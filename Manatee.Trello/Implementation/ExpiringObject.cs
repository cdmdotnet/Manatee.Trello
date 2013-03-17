using System;

namespace Manatee.Trello.Implementation
{
	public abstract class ExpiringObject
	{
		private DateTime _expires;
		private TrelloService _svc;

		internal TrelloService Svc
		{
			get { return _svc; }
			set
			{
				if (_svc == value) return;
				_svc = value;
				PropigateSerivce();
				MarkForUpdate();
			}
		}

		public ExpiringObject()
		{
			MarkForUpdate();
		}
		internal ExpiringObject(TrelloService svc)
		{
			Svc = svc;
			MarkForUpdate();
		}

		protected void VerifyNotExpired()
		{
			if ((Svc == null) || (Svc.Api == null) || !Options.AutoRefresh || (DateTime.Now < _expires)) return;
			Refresh();
			_expires = DateTime.Now + Options.ItemDuration;
		}

		private void MarkForUpdate()
		{
			_expires = DateTime.Now - TimeSpan.FromSeconds(1);
		}

		protected abstract void Refresh();
		protected abstract void PropigateSerivce();
	}
}
