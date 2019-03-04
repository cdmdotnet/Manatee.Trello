using System.Collections.Generic;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents an emoji that can be used as a reaction to a comment.
	/// </summary>
	public class Emoji
	{
		/// <summary>
		/// Gets the Unicode code point.
		/// </summary>
		public string Unified { get; }
		/// <summary>
		/// Gets the corresponding Unicode character.
		/// </summary>
		public string Native { get; }
		/// <summary>
		/// Gets the official name.
		/// </summary>
		public string Name { get; }
		/// <summary>
		/// Gets the most common short name typically used to identify the emoji within text.
		/// </summary>
		public string ShortName { get; }
		/// <summary>
		/// Gets the full list of short names.
		/// </summary>
		public IEnumerable<string> ShortNames { get; }
		/// <summary>
		/// Gets the most common text-based emoticon equivalent.
		/// </summary>
		public string Text { get; }
		/// <summary>
		/// Gets the full list of emoticons.
		/// </summary>
		public IEnumerable<string> Texts { get; }
		/// <summary>
		/// Gets the category.
		/// </summary>
		public string Category { get; }
		/// <summary>
		/// Gets the X-coordinate on the sprite sheet.
		/// </summary>
		/// <remarks>
		/// Sprite sheet: https://d2k1ftgv7pobq7.cloudfront.net/images/emoji-spritesheets/sheet-twitter-64.png
		/// </remarks>
		public int SheetX { get; }
		/// <summary>
		/// Gets the Y-coordinate on the sprite sheet.
		/// </summary>
		/// <remarks>
		/// Sprite sheet: https://d2k1ftgv7pobq7.cloudfront.net/images/emoji-spritesheets/sheet-twitter-64.png
		/// </remarks>
		public int SheetY { get; }
		/// <summary>
		/// Gets the <see cref="SkinVariationType"/>.
		/// </summary>
		public SkinVariationType SkinVariationType { get; }
		/// <summary>
		/// Gets the Text-To-Speech conversion.
		/// </summary>
		public string TextToSpeech { get; }
		/// <summary>
		/// Gets the list of associated keywords.
		/// </summary>
		public IEnumerable<string> Keywords { get; }

		internal Emoji(string unified, string native, string name, string shortName,
		               IEnumerable<string> shortNames, string text, IEnumerable<string> texts,
		               string category, int sheetX, int sheetY, SkinVariationType skinVariationType,
		               string tts, IEnumerable<string> keywords)
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
			SkinVariationType = skinVariationType;
			TextToSpeech = tts;
			Keywords = keywords;
		}
	}
}