/***************************************************************************************

	Copyright 2015 Greg Dennis

	   Licensed under the Apache License, Version 2.0 (the "License");
	   you may not use this file except in compliance with the License.
	   You may obtain a copy of the License at

		 http://www.apache.org/licenses/LICENSE-2.0

	   Unless required by applicable law or agreed to in writing, software
	   distributed under the License is distributed on an "AS IS" BASIS,
	   WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
	   See the License for the specific language governing permissions and
	   limitations under the License.
 
	File Name:		ReadOnlyBoardBackgroundScalesCollection.cs
	Namespace:		Manatee.Trello
	Class Name:		ReadOnlyBoardBackgroundScalesCollection
	Purpose:		A read-only collection of scaled versions of board backgrounds.

***************************************************************************************/
using Manatee.Trello.Internal.Caching;
using Manatee.Trello.Internal.Synchronization;

namespace Manatee.Trello
{
	/// <summary>
	/// A read-only collection of scaled versions of board backgrounds.
	/// </summary>
	public class ReadOnlyBoardBackgroundScalesCollection : ReadOnlyCollection<ImagePreview>
	{
		private readonly BoardBackgroundContext _context;

		internal ReadOnlyBoardBackgroundScalesCollection(BoardBackgroundContext context, TrelloAuthorization auth)
			: base(() => context.Data.Id, auth)
		{
			_context = context;
		}

		/// <summary>
		/// Implement to provide data to the collection.
		/// </summary>
		protected sealed override void Update()
		{
			_context.Synchronize();
			if (_context.Data.ImageScaled == null) return;
			Items.Clear();
			foreach (var jsonPreview in _context.Data.ImageScaled)
			{
				var preview = jsonPreview.GetFromCache<ImagePreview>(Auth);
				Items.Add(preview);
			}
		}
	}
}