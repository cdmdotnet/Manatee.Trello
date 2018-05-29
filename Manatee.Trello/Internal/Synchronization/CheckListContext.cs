using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class CheckListContext : SynchronizationContext<IJsonCheckList>
	{
		private static readonly Dictionary<string, object> Parameters;
		private static readonly CheckList.Fields MemberFields;

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

		public CheckItemCollection CheckItems { get; }

		static CheckListContext()
		{
			Parameters = new Dictionary<string, object>();
			MemberFields = CheckList.Fields.Name |
						   CheckList.Fields.Card |
						   CheckList.Fields.Board |
						   CheckList.Fields.Position;
			Properties = new Dictionary<string, Property<IJsonCheckList>>
				{
					{
						nameof(CheckList.Board),
						new Property<IJsonCheckList, Board>((d, a) => d.Board.GetFromCache<Board, IJsonBoard>(a),
						                                    (d, o) => d.Board = o?.Json)
					},
					{
						nameof(CheckList.Card),
						new Property<IJsonCheckList, Card>((d, a) => d.Card.GetFromCache<Card, IJsonCard>(a),
						                                   (d, o) => d.Card = o?.Json)
					},
					{
						nameof(CheckList.Id),
						new Property<IJsonCheckList, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(CheckList.Name),
						new Property<IJsonCheckList, string>((d, a) => d.Name, (d, o) => d.Name = o)
					},
					{
						nameof(CheckList.Position),
						new Property<IJsonCheckList, Position>((d, a) => Position.GetPosition(d.Pos),
						                                       (d, o) => d.Pos = Position.GetJson(o))
					},
				};
		}
		public CheckListContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;

			CheckItems = new CheckItemCollection(this, auth);
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
				var flags = Enum.GetValues(typeof(CheckList.Fields)).Cast<CheckList.Fields>().ToList();
				var availableFields = (CheckList.Fields)flags.Cast<int>().Sum();

				var memberFields = availableFields & MemberFields & CheckList.DownloadedFields;
				Parameters["fields"] = memberFields.GetDescription();

				var parameterFields = availableFields & CheckList.DownloadedFields & (~MemberFields);
				if (parameterFields.HasFlag(CheckList.Fields.CheckItems))
				{
					Parameters["checkItems"] = "all";
					Parameters["checkItem_fields"] = CheckItemContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(CheckList.Fields.Board))
				{
					Parameters["board"] = "true";
					Parameters["board_fields"] = BoardContext.CurrentParameters["fields"];
				}
				if (parameterFields.HasFlag(CheckList.Fields.Card))
				{
					Parameters["card"] = "true";
					Parameters["card_fields"] = CardContext.CurrentParameters["fields"];
				}
			}
		}

		public async Task Delete(CancellationToken ct)
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.CheckList_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			await JsonRepository.Execute(Auth, endpoint, ct);

			_deleted = true;
		}

		protected override async Task<IJsonCheckList> GetData(CancellationToken ct)
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.CheckList_Read_Refresh, new Dictionary<string, object> {{"_id", Data.Id}});
				var newData = await JsonRepository.Execute<IJsonCheckList>(Auth, endpoint, ct, CurrentParameters);

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
		protected override async Task SubmitData(IJsonCheckList json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.CheckList_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);
			Merge(newData);
		}

		protected override IEnumerable<string> MergeDependencies(IJsonCheckList json, bool overwrite)
		{
			var properties = new List<string>();

			if (json.CheckItems != null)
			{
				CheckItems.Update(json.CheckItems.Select(a => a.TryGetFromCache<CheckItem>() ?? new CheckItem(a, Data.Id, Auth)));
				properties.Add(nameof(CheckList.CheckItems));
			}

			return properties;
		}

		protected override bool CanUpdate()
		{
			return !_deleted;
		}
	}
}