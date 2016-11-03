﻿using System.Collections.Generic;
using Manatee.Trello.Exceptions;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class LabelContext : SynchronizationContext<IJsonLabel>
	{
		private bool _deleted;

		static LabelContext()
		{
			_properties = new Dictionary<string, Property<IJsonLabel>>
				{
					{
						"Board", new Property<IJsonLabel, Board>((d, a) => d.Board?.GetFromCache<Board>(a),
																 (d, o) => d.Board = o?.Json)
					},
					{"Color", new Property<IJsonLabel, LabelColor?>((d, a) => d.Color, (d, o) => d.Color = o)},
					{"Id", new Property<IJsonLabel, string>((d, a) => d.Id, (d, o) => d.Id = o)},
					{"Name", new Property<IJsonLabel, string>((d, a) => d.Name, (d, o) => d.Name = o)},
					{"Uses", new Property<IJsonLabel, int?>((d, a) => d.Uses, (d, o) => d.Uses = o)},
				};
		}
		public LabelContext(string id, TrelloAuthorization auth)
			: base(auth)
		{
			Data.Id = id;
		}

		public void Delete()
		{
			if (_deleted) return;
			CancelUpdate();

			var endpoint = EndpointFactory.Build(EntityRequestType.Label_Write_Delete, new Dictionary<string, object> {{"_id", Data.Id}});
			JsonRepository.Execute(Auth, endpoint);

			_deleted = true;
		}

		protected override IJsonLabel GetData()
		{
			try
			{
				var endpoint = EndpointFactory.Build(EntityRequestType.Label_Read_Refresh, new Dictionary<string, object> {{"_boardId", Data.Board.Id}, {"_id", Data.Id}});
				var newData = JsonRepository.Execute<IJsonLabel>(Auth, endpoint);
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
		protected override void SubmitData(IJsonLabel json)
		{
			var endpoint = EndpointFactory.Build(EntityRequestType.Label_Write_Update, new Dictionary<string, object> {{"_boardId", Data.Board.Id}, {"_id", Data.Id}});
			var newData = JsonRepository.Execute(Auth, endpoint, json);
			Merge(newData);
		}
	}
}