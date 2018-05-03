using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Manatee.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello.CardRepeater
{
	public class CardRepetition : ICacheable
	{
		internal static readonly ITrelloFactory Factory = new TrelloFactory();

		private readonly TrelloAuthorization _auth;

		public string Id { get; private set; }
		public ICard SourceCard { get; private set; }
		public IList List { get; private set; }
		public IBoard Board { get; private set; }
		public IMember Member { get; private set; }
		public Position Position { get; private set; }
		public int? Interval { get; private set; }
		public RepetitionPeriod Period { get; private set; }
		public string TimeZone { get; private set; }
		public Time Time { get; private set; }
		public List<Date> Dates { get; private set; }
		public List<DayOfWeek> Days { get; private set; }

		internal CardRepetition(JsonObject data, TrelloAuthorization auth)
		{
			_auth = auth ?? TrelloAuthorization.Default;

			Update(data);

			TrelloConfiguration.Cache.Add(this);
		}

		public async Task ChangeToWeekly(int hour, int minute, int interval, IEnumerable<DayOfWeek> days, IList list = null, Position position = null)
		{
			if (hour < 0 || hour >= 24)
				throw new ArgumentException(nameof(hour));
			if (minute < 0 || minute >= 60)
				throw new ArgumentException(nameof(minute));
			if (interval < 0 || interval > 30)
				throw new ArgumentException(nameof(interval));
			var daysList = days?.ToList();
			if (daysList == null || !daysList.Any())
				throw new ArgumentException(nameof(days));
			if (position != null && position != Position.Bottom && position != Position.Top)
				throw new ArgumentException(nameof(position));

			Period = RepetitionPeriod.Weekly;
			Time = new Time(hour, minute);
			Interval = interval;
			Days = daysList;

			if (list != null)
				List = list;
			if (position != null)
				Position = position;

			await _Submit();
		}

		public async Task ChangeToMonthly(int date)
		{

		}

		public async Task ChangeToYearly(int date)
		{

		}

		public async Task Delete()
		{

		}

		internal void Update(JsonObject data)
		{
			_FromJson(data);
		}

		private void _FromJson(JsonObject data)
		{
			Id = data.TryGetString("id");
			var cardId = data.TryGetString("idCard");
			if (!string.IsNullOrEmpty(cardId))
				SourceCard = Factory.Card(cardId, _auth);
			var listId = data.TryGetString("idTargetList");
			if (!string.IsNullOrEmpty(listId))
				List = Factory.List(listId, _auth);
			var boardId = data.TryGetString("idTargetBoard");
			if (!string.IsNullOrEmpty(boardId))
				Board = Factory.Board(boardId, _auth);
			var memberId = data.TryGetString("idMember");
			if (!string.IsNullOrEmpty(memberId))
				Member = Factory.Member(memberId, _auth);
			var pos = data.TryGetString("pos");
			if (pos?.ToLower() == "top")
				Position = Position.Top;
			else if (pos?.ToLower() == "bottom")
				Position = Position.Bottom;
			Interval = (int?) data.TryGetNumber("interval");
			var period = data.TryGetString("period");
			if (period != null)
				Period = TrelloConfiguration.Deserializer.Deserialize<RepetitionPeriod>($"\"{period}\"");
			TimeZone = data.TryGetString("tz");
			var weekDays = data.TryGetArray("weekdays");
			Days = weekDays?.OfType(JsonValueType.Number)
			               .Select(jv => (DayOfWeek) jv.Number)
			               .ToList();
			var time = data.TryGetObject("time");
			if (time != null)
			{
				var hour = time.TryGetNumber("h");
				var minute = time.TryGetNumber("m");
				if (hour.HasValue && minute.HasValue)
					Time = new Time((int) hour, (int) minute);
			}
			var dates = data.TryGetArray("dates");
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

		private async Task _Submit()
		{
			await TrelloProcessor.CustomRequest(RestMethod.Put, $"board/{Board.Id}/pluginData/{Id}", this, _auth);
		}

		internal string ToJson()
		{
			var json = new JsonObject();
			switch (Period)
			{
				case RepetitionPeriod.Weekly:
					json["time"] = new JsonObject
						{
							["h"] = Time.Hour,
							["m"] = Time.Minute
						};
					json["interval"] = Interval;
					json["days"] = Days.Select(d => (double) d).ToJson();
					json["idTargetList"] = List.Id;
					json["pos"] = Position == Position.Top ? "top" : "bottom";
					break;
				case RepetitionPeriod.Monthly:
					break;
				case RepetitionPeriod.Yearly:
					break;
				default:
					throw new ArgumentOutOfRangeException();
			}

			return json.ToString();
		}
	}
}