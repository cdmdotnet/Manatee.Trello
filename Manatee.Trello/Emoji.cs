using System.Collections.Generic;

namespace Manatee.Trello
{
	public class Emoji
	{
		// functions as ID
		public string Unified { get; }
		public string Native { get; }
		public string Name { get; }
		public string ShortName { get; }
		public IEnumerable<string> ShortNames { get; }
		public string Text { get; }
		public IEnumerable<string> Texts { get; }
		public string Category { get; }
		public int SheetX { get; }
		public int SheetY { get; }
		public string Tts { get; }
		public IEnumerable<string> Keywords { get; }

		internal Emoji(string unified, string native, string name, string shortName,
		               IEnumerable<string> shortNames, string text, IEnumerable<string> texts,
		               string category, int sheetX, int sheetY, string tts, IEnumerable<string> keywords)
		{
			Unified = unified;
			Native = native;
			Name = name;
			ShortName = shortName;
			ShortNames = shortNames;
			Text = text;
			Texts = texts;
			Category = category;
			SheetX = sheetX;
			SheetY = sheetY;
			Tts = tts;
			Keywords = keywords;
		}
	}
}