using System.Collections.Generic;
using Manatee.Trello.Json;

namespace Manatee.Trello.Internal.Synchronization
{
	internal class BoardBackgroundContext : LinkedSynchronizationContext<IJsonBoardBackground>
	{
		// todo: do I need to move scaled images here?

		static BoardBackgroundContext()
		{
			Properties = new Dictionary<string, Property<IJsonBoardBackground>>
				{
					{
						nameof(BoardBackground.Id),
						new Property<IJsonBoardBackground, string>((d, a) => d.Id, (d, o) => d.Id = o)
					},
					{
						nameof(BoardBackground.Color),
						new Property<IJsonBoardBackground, WebColor>(
							(d, a) => d.Color.IsNullOrWhiteSpace() ? null : new WebColor(d.Color),
							(d, o) => d.Color = o?.ToString())
					},
					{
						nameof(BoardBackground.BottomColor),
						new Property<IJsonBoardBackground, WebColor>(
							(d, a) => d.BottomColor.IsNullOrWhiteSpace() ? null : new WebColor(d.BottomColor),
							(d, o) => d.BottomColor = o?.ToString())
					},
					{
						nameof(BoardBackground.TopColor),
						new Property<IJsonBoardBackground, WebColor>(
							(d, a) => d.TopColor.IsNullOrWhiteSpace() ? null : new WebColor(d.TopColor),
							(d, o) => d.TopColor = o?.ToString())
					},
					{
						nameof(BoardBackground.Image),
						new Property<IJsonBoardBackground, string>((d, a) => d.Image, (d, o) => d.Image = o)
					},
					{
						nameof(BoardBackground.IsTiled),
						new Property<IJsonBoardBackground, bool?>((d, a) => d.Tile, (d, o) => d.Tile = o)
					},
					{
						nameof(BoardBackground.Brightness),
						new Property<IJsonBoardBackground, BoardBackgroundBrightness?>((d, a) => d.Brightness, (d, o) => d.Brightness = o)
					},
				};
		}
		public BoardBackgroundContext(TrelloAuthorization auth)
			: base(auth) {}
	}
}