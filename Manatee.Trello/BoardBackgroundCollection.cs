using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Manatee.Trello.Internal.DataAccess;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;

namespace Manatee.Trello
{
	/// <summary>
	/// A collection of board backgrounds.
	/// </summary>
	public class BoardBackgroundCollection : ReadOnlyBoardBackgroundCollection, IBoardBackgroundCollection
	{
		internal BoardBackgroundCollection(Func<string> getOwnerId, TrelloAuthorization auth)
			: base(getOwnerId, auth)
		{
		}


                /// <summary>
                /// Adds a custom board background.
                /// </summary>
                /// <param name="data">The byte data of the file to attach.</param>
                /// <param name="ct">(Optional) A cancellation token for async processing.</param>
                /// <returns>The newly created <see cref="IBoardBackground"/>.</returns>
		public async Task<IBoardBackground> Add(byte[] data, CancellationToken ct = default)
                {
                    var parameters = new Dictionary<string, object> { { RestFile.ParameterKey, new RestFile { ContentBytes = data } } };
                    var endpoint = EndpointFactory.Build(EntityRequestType.Member_Write_AddBoardBackground, new Dictionary<string, object> { { "_id", OwnerId } });
                    var newData = await JsonRepository.Execute<IJsonBoardBackground>(Auth, endpoint, ct, parameters);

                    return new BoardBackground(OwnerId, newData, Auth);
                }

                /// <summary>
                /// Adds a custom board background.
                /// </summary>
                /// <param name="filePath">The path of the file to attach.</param>
                /// <param name="ct">(Optional) A cancellation token for async processing.</param>
                /// <returns>The newly created <see cref="IBoardBackground"/>.</returns>
                public async Task<IBoardBackground> Add(string filePath, CancellationToken ct = default)
		{
                    if (!File.Exists(filePath)) throw new Exception(filePath + " Invalid file path");

                        var parameters = new Dictionary<string, object> {{RestFile.ParameterKey, new RestFile {FilePath = filePath , FileName = "BoardBackground"} }};
                        var endpoint = EndpointFactory.Build(EntityRequestType.Member_Write_AddBoardBackground, new Dictionary<string, object> {{"_id", OwnerId}});
                        var newData = await JsonRepository.Execute<IJsonBoardBackground>(Auth, endpoint, ct, parameters);

                        return new BoardBackground(OwnerId, newData, Auth);
		}
    }
}