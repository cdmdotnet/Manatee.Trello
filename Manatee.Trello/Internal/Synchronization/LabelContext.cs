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
	internal class LabelContext : DeletableSynchronizationContext<IJsonLabel>
	{
		private static readonly Dictionary<string, object> Parameters;
		private static readonly Label.Fields MemberFields;

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

		static LabelContext()
		{
			Parameters = new Dictionary<string, object>();
			MemberFields = Label.Fields.Color |
			               Label.Fields.Name;
			Properties = new Dictionary<string, Property<IJsonLabel>>
				{
					{
						nameof(Label.Board),
						new Property<IJsonLabel, Board>((d, a) => d.Board?.GetFromCache<Board, IJsonBoard>(a),
						                                (d, o) => d.Board = o?.Json)
					},
					{
						nameof(Label.Color),
						new Property<IJsonLabel, LabelColor?>((d, a) => d.Color, (d, o) => d.Color = o)
					},
					{
						nameof(Label.Id),
						new Property<IJsonLabel, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(Label.Name),
						new Property<IJsonLabel, string>((d, a) => d.Name, (d, o) => d.Name = o)
					},
					{
						nameof(IJsonLabel.ValidForMerge),
						new Property<IJsonLabel, bool>((d, a) => d.ValidForMerge, (d, o) => d.ValidForMerge = o, true)
					},
				};
		}
		public LabelContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
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
				var flags = Enum.GetValues(typeof(Label.Fields)).Cast<Label.Fields>().ToList();
				var availableFields = (Label.Fields)flags.Cast<int>().Sum();

				var memberFields = availableFields & MemberFields & Label.DownloadedFields;
				Parameters["fields"] = memberFields.GetDescription();

				var parameterFields = availableFields & Label.DownloadedFields & (~MemberFields);
				if (parameterFields.HasFlag(Label.Fields.Board))
				{
					Parameters["board"] = "true";
					Parameters["board_fields"] = BoardContext.CurrentParameters["fields"];
				}
			}
		}

		public override Endpoint GetRefreshEndpoint()
		{
			return EndpointFactory.Build(EntityRequestType.Label_Read_Refresh,
			                             new Dictionary<string, object> {{"_id", Data.Id}});
		}

		protected override Dictionary<string, object> GetParameters()
		{
			return CurrentParameters;
		}

		protected override Endpoint GetDeleteEndpoint()
		{
			return EndpointFactory.Build(EntityRequestType.Label_Write_Delete,
			                             new Dictionary<string, object> {{"_id", Data.Id}});
		}

		protected override async Task SubmitData(IJsonLabel json, CancellationToken ct)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Label_Write_Update, new Dictionary<string, object> {{"_id", Data.Id}});
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);
			Merge(newData);
		}
	}
}