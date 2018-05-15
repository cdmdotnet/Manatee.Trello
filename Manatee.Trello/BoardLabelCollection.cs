﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of labels for boards.
	/// </summary>
	public class BoardLabelCollection : ReadOnlyCollection<ILabel>, IBoardLabelCollection
	{
		internal BoardLabelCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth) {}

		/// <summary>
		/// Adds a label to the collection.
		/// </summary>
		/// <param name="name">The name of the label.</param>
		/// <param name="color">The color of the label to add.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		/// <returns>The <see cref="ILabel"/> generated by Trello.</returns>
		public async Task<ILabel> Add(string name, LabelColor? color, CancellationToken ct = default(CancellationToken))
		{
			var json = TrelloConfiguration.JsonFactory.Create<IJsonLabel>();
			json.Name = name ?? string.Empty;
			json.Color = color;
			json.ForceNullColor = !color.HasValue;
			json.Board = TrelloConfiguration.JsonFactory.Create<IJsonBoard>();
			json.Board.Id = OwnerId;

			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Write_AddLabel);
			var newData = await JsonRepository.Execute(Auth, endpoint, json, ct);

			return new Label(newData, Auth);
		}

		/// <summary>
		/// Adds a filter to the collection.
		/// </summary>
		/// <param name="labelColor">The filter value.</param>
		public void Filter(LabelColor labelColor)
		{
			AdditionalParameters["filter"] = labelColor.GetDescription();
		}

		/// <summary>
		/// Manually updates the collection's data.
		/// </summary>
		/// <param name="force">Indicates that the refresh should ignore the value in <see cref="TrelloConfiguration.RefreshThrottle"/> and make the call to the API.</param>
		/// <param name="ct">(Optional) A cancellation token for async processing.</param>
		public sealed override async Task Refresh(bool force = false, CancellationToken ct = default(CancellationToken))
		{
			IncorporateLimit();

			var endpoint = EndpointFactory.Build(EntityRequestType.Board_Read_Labels, new Dictionary<string, object> {{"_id", OwnerId}});
			var newData = await JsonRepository.Execute<List<IJsonLabel>>(Auth, endpoint, ct, AdditionalParameters);

			Items.Clear();
			Items.AddRange(newData.Select(jb =>
				{
					var board = jb.GetFromCache<Label, IJsonLabel>(Auth);
					board.Json = jb;
					return board;
				}));
		}
	}
}