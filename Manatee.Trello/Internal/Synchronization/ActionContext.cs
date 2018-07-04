using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class ActionContext : SynchronizationContext<IJsonAction>
	{
		private static readonly Dictionary<string, object> Parameters;
		private static readonly Action.Fields MemberFields;

		public static Dictionary<string, object> CurrentParameters
		{
			get
			{
				lock (Parameters)
				{
					if (!Parameters.Any())
						GenerateParameters();

					return new Dictionary<string, object>(Parameters);
				}
			}
		}

		private bool _deleted;

		public ActionDataContext ActionDataContext { get; }
		public virtual bool HasValidId => IdRule.Instance.Validate(Data.Id, null) == null;

		static ActionContext()
		{
			Parameters = new Dictionary<string, object>();
			MemberFields = Action.Fields.Data |
			               Action.Fields.Date |
						   Action.Fields.Type;
			Properties = new Dictionary<string, Property<IJsonAction>>
				{
					{
						nameof(Action.Creator),
						new Property<IJsonAction, Member>((d, a) => d.MemberCreator.GetFromCache<Member, IJsonMember>(a),
						                                  (d, o) => { if (o != null) d.MemberCreator = o.Json; })
					},
					{
						nameof(Action.Date),
						new Property<IJsonAction, DateTime?>((d, a) => d.Date, (d, o) => d.Date = o)
					},
					{
						nameof(Action.Id),
						new Property<IJsonAction, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						"Text",
						new Property<IJsonAction, string>((d, a) => d.Data.Text, (d, o) => d.Text = o)
					},
					{
						nameof(Action.Type),
						new Property<IJsonAction, ActionType?>((d, a) => d.Type, (d, o) => d.Type = o)
					},
				};
		}
		public ActionContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
			ActionDataContext = new ActionDataContext(Auth);
			ActionDataContext.SubmitRequested += ct => HandleSubmitRequested("Text", ct);
			Data.Data = ActionDataContext.Data;
		}

		public static void UpdateParameters()
		{
			lock (Parameters)
			{
				Parameters.Clear();
			}
		}

		private static void GenerateParameters()
		{
			lock (Parameters)
			{
				Parameters.Clear();
				var flags = Enum.GetValues(typeof(Action.Fields)).Cast<Action.Fields>().ToList();
				var availableFields = (Action.Fields)flags.Cast<int>().Sum();

				var memberFields = availableFields & MemberFields & Action.DownloadedFields;
				Parameters["fields"] = memberFields.GetDescription();

				var parameterFields = availableFields & Action.DownloadedFields & (~MemberFields);
				if (parameterFields.HasFlag(Action.Fields.Creator))
				{
					Parameters["memberCreator"] = "true";
					Parameters["memberCreator_fields"] = MemberContext.CurrentParameters["fields"];
				}
			}
		}

		public async Task Delete(CancellationToken ct)
		{
			if (_deleted) return;

			var endpoint = EndpointFactory.Build(EntityRequestType.Action_Write_Delete,
			                                     new Dictionary<string, object> {{"_id", Data.Id}});
			await JsonRepository.Execute(Auth, endpoint, ct);

			_deleted = true;
			RaiseDeleted();
		}

		public override Endpoint GetRefreshEndpoint()
		{
			return EndpointFactory.Build(EntityRequestType.Action_Read_Refresh,
			                             new Dictionary<string, object> {{"_id", Data.Id}});
		}

		protected override async Task<IJsonAction> GetData(CancellationToken ct)
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.Action_Read_Refresh,
				                                     new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = await JsonRepository.Execute<IJsonAction>(Auth, endpoint, ct, CurrentParameters);
				MarkInitialized();

				return newData;
			}
			catch (TrelloInteractionException e)
			{
				if (!e.IsNotFoundError() || !IsInitialized) throw;
				_deleted = true;
				return Data;
			}
		}
		protected override async Task SubmitData(IJsonAction json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Action_Write_Update,
			                                     new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);

			Merge(newData);
			Data.Data = ActionDataContext.Data;
		}
		protected override void ApplyDependentChanges(IJsonAction json)
		{
			Data.Data = ActionDataContext.Data;
			if (ActionDataContext.HasChanges)
			{
				json.Data = ActionDataContext.GetChanges();
				ActionDataContext.ClearChanges();
			}
		}
		protected override IEnumerable<string> MergeDependencies(IJsonAction json, bool overwrite)
		{
			return ActionDataContext.Merge(json.Data, overwrite);
		}
		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}