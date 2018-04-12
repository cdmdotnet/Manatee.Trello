using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal;
using Manatee.Trello.Internal.Synchronization;
using Manatee.Trello.Internal.Validation;
using Manatee.Trello.Json;

namespace Manatee.Trello
{
	/// <summary>
	/// Represents a webhook.
	/// </summary>
	/// <typeparam name="T">The type of object to which the webhook is attached.</typeparam>
	public class Webhook<T> : IWebhook<T>, IMergeJson<IJsonWebhook>
		where T : class, ICanWebhook
	{
		private readonly Field<string> _callBackUrl;
		private readonly Field<string> _description;
		private readonly Field<bool?> _isActive;
		private readonly Field<T> _target;
		private readonly WebhookContext<T> _context;
		private DateTime? _creation;

		/// <summary>
		/// Gets or sets a callback URL for the webhook.
		/// </summary>
		public string CallBackUrl
		{
			get { return _callBackUrl.Value; }
			set { _callBackUrl.Value = value; }
		}
		/// <summary>
		/// Gets the creation date of the webhook.
		/// </summary>
		public DateTime CreationDate
		{
			get
			{
				if (_creation == null)
					_creation = Id.ExtractCreationDate();
				return _creation.Value;
			}
		}
		/// <summary>
		/// Gets or sets a description for the webhook.
		/// </summary>
		public string Description
		{
			get { return _description.Value; }
			set { _description.Value = value; }
		}
		/// <summary>
		/// Gets the webhook's ID.
		/// </summary>
		public string Id { get; private set; }
		/// <summary>
		/// Gets or sets whether the webhook is active.
		/// </summary>
		public bool? IsActive
		{
			get { return _isActive.Value; }
			set { _isActive.Value = value; }
		}
		/// <summary>
		/// Gets or sets the webhook's target.
		/// </summary>
		public T Target
		{
			get { return _target.Value; }
			set { _target.Value = value; }
		}

		/// <summary>
		/// Raised when data on the webhook is updated.
		/// </summary>
		public event Action<IWebhook<T>, IEnumerable<string>> Updated;

		/// <summary>
		/// Creates a new instance of the <see cref="Webhook{T}"/> object for a webhook which has already been registered with Trello.
		/// </summary>
		/// <param name="id">The id.</param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided, <see cref="TrelloAuthorization.Default"/> will be used.</param>
		public Webhook(string id, TrelloAuthorization auth = null)
		{
			Id = id;
			_context = new WebhookContext<T>(Id, auth);
			_context.Synchronized += Synchronized;

			_callBackUrl = new Field<string>(_context, nameof(CallBackUrl));
			_callBackUrl.AddRule(UriRule.Instance);
			_description = new Field<string>(_context, nameof(Description));
			_isActive = new Field<bool?>(_context, nameof(IsActive));
			_isActive.AddRule(NullableHasValueRule<bool>.Instance);
			_target = new Field<T>(_context, nameof(Target));
			_target.AddRule(NotNullRule<T>.Instance);

			TrelloConfiguration.Cache.Add(this);
		}

		private Webhook(string id, WebhookContext<T> context)
		{
			Id = id;
			_context.Synchronized += Synchronized;

			_callBackUrl = new Field<string>(_context, nameof(CallBackUrl));
			_callBackUrl.AddRule(UriRule.Instance);
			_description = new Field<string>(_context, nameof(Description));
			_isActive = new Field<bool?>(_context, nameof(IsActive));
			_isActive.AddRule(NullableHasValueRule<bool>.Instance);
			_target = new Field<T>(_context, nameof(Target));
			_target.AddRule(NotNullRule<T>.Instance);

			TrelloConfiguration.Cache.Add(this);
		}

		/// <summary>
		/// Creates a new instance of the <see cref="Webhook{T}"/> object and registers a webhook with Trello.
		/// </summary>
		/// <param name="target"></param>
		/// <param name="description"></param>
		/// <param name="callBackUrl"></param>
		/// <param name="auth">(Optional) Custom authorization parameters. When not provided, <see cref="TrelloAuthorization.Default"/> will be used.</param>
		public static async Task<Webhook<T>> Create(T target, string callBackUrl, string description = null,
		                                            TrelloAuthorization auth = null, CancellationToken ct = default(CancellationToken))
		{
			var context = new WebhookContext<T>(auth);
			var id = await context.Create(target, description, callBackUrl, ct);
			return new Webhook<T>(id, context);
		}

		/// <summary>
		/// Permanently deletes the webhook from Trello.
		/// </summary>
		/// <remarks>
		/// This instance will remain in memory and all properties will remain accessible.
		/// </remarks>
		public async Task Delete(CancellationToken ct = default(CancellationToken))
		{
			await _context.Delete(ct);
			if (TrelloConfiguration.RemoveDeletedItemsFromCache)
				TrelloConfiguration.Cache.Remove(this);
		}
		/// <summary>
		/// Marks the webhook to be refreshed the next time data is accessed.
		/// </summary>
		public async Task Refresh(CancellationToken ct = default(CancellationToken))
		{
			await _context.Synchronize(ct);
		}

		void IMergeJson<IJsonWebhook>.Merge(IJsonWebhook json)
		{
			_context.Merge(json);
		}

		private void Synchronized(IEnumerable<string> properties)
		{
			Id = _context.Data.Id;
			var handler = Updated;
			handler?.Invoke(this, properties);
		}
	}
}