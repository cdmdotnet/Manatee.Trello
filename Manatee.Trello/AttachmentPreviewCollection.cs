/***************************************************************************************

	Copyright 2014 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		AttachmentPreviewCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		ReadOnlyAttachmentPreviewCollection
	Purpose:		Collection objects for attachment previews.

***************************************************************************************/
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.Synchronization;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of attachment previews.
	/// </summary>
	public class ReadOnlyAttachmentPreviewCollection : ReadOnlyCollection<AttachmentPreview>
	{
		private readonly AttachmentContext _context;

		internal ReadOnlyAttachmentPreviewCollection(AttachmentContext context)
			: base(context.Data.Id)
		{
			_context = context;
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected override sealed void Update()
		{
			_context.Synchronize();
			if (_context.Data.Previews == null) return;
			Items.Clear();
			foreach (var jsonPreview in _context.Data.Previews)
			{
				var preview = jsonPreview.GetFromCache<AttachmentPreview>();
				Items.Add(preview);
			}
		}
	}
}