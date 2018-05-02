using System;
using System.Collections.Generic;
using System.Linq;
using Manatee.Json;

namespace Manatee.Trello.CardRepeater
{
	public class CardRepitition
	{
		internal static readonly ITrelloFactory Factory = new TrelloFactory();

		private readonly TrelloAuthorization _auth;

		public string Id { get; set; }
		public ICard SourceCard { get; set; }
		public IList List { get; set; }
		public IBoard Board { get; set; }
		public IMember Member { get; set; }
		public Position Position { get; set; }
		public int? Interval { get; set; }
		public RepititionPeriod Period { get; set; }
		public string TimeZone { get; set; }
		public Time Time { get; set; }
		public List<Date> Dates { get; set; }
		public List<DayOfWeek> Days { get; set; }

		internal CardRepitition(string data, TrelloAuthorization auth)
		{
			var json = JsonValue.Parse(data);
			if (json.Type != JsonValueType.Object) return;

			_auth = auth ?? TrelloAuthorization.Default;

			FromJson(json);

			Console.WriteLine(json.GetIndentedString());
		}

		private void FromJson(JsonValue json)
		{
			var obj = json.Object;
			Id = obj.TryGetString("id");
			var cardId = obj.TryGetString("idCard");
			if (!string.IsNullOrEmpty(cardId))
				SourceCard = Factory.Card(cardId, _auth);
			var listId = obj.TryGetString("idList");
			if (!string.IsNullOrEmpty(listId))
				List = Factory.List(listId, _auth);
			var boardId = obj.TryGetString("idBoard");
			if (!string.IsNullOrEmpty(boardId))
				Board = Factory.Board(boardId, _auth);
			var memberId = obj.TryGetString("idMember");
			if (!string.IsNullOrEmpty(memberId))
				Member = Factory.Member(memberId, _auth);
			var pos = obj.TryGetString("pos");
			if (pos?.ToLower() == "top")
				Position = Position.Top;
			else if (pos?.ToLower() == "bottom")
				Position = Position.Bottom;
			Interval = (int?) obj.TryGetNumber("interval");
			var period = obj.TryGetString("period");
			if (period?.ToLower() == "weekly")
				Period = RepititionPeriod.Weekly;
			else if (period?.ToLower() == "monthly")
				Period = RepititionPeriod.Monthly;
			else if (period?.ToLower() == "yearly")
				Period = RepititionPeriod.Yearly;
			TimeZone = obj.TryGetString("tz");
			var weekDays = obj.TryGetArray("weekdays");
			Days = weekDays?.OfType(JsonValueType.Number)
			               .Select(jv => (DayOfWeek) jv.Number)
			               .ToList();
			var time = obj.TryGetObject("time");
			if (time != null)
			{
				var hour = time.TryGetNumber("h");
				var minute = time.TryGetNumber("d");
				if (hour.HasValue && minute.HasValue)
					Time = new Time((int) hour, (int) minute);
			}
			var dates = obj.TryGetArray("dates");
			Dates = dates?.OfType(JsonValueType.Object)
			             .Select(jv =>
				             {
					             var month = jv.Object.TryGetNumber("m");
					             var day = jv.Object.TryGetNumber("d");
					             if (month.HasValue && day.HasValue)
						             return new Date((MonthOfYear) (month + 1), (int) day);
					             return null;
				             })
			             .Where(x => x != null)
			             .ToList();
		}
	}
}