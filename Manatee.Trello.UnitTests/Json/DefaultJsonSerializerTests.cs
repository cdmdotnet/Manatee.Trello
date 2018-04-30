using System;
using System.Collections;
using System.Linq;
using FluentAssertions;
using Manatee.Trello.Json;
using Manatee.Trello.Rest;
using Moq;
using NUnit.Framework;

namespace Manatee.Trello.UnitTests.Json
{
	[TestFixture]
	public class DefaultJsonSerializerTests
	{
		public static IEnumerable JsonTypes
		{
			get
			{
				var nonEntityTypes = new[]
					{
						typeof(IAcceptId),
						typeof(ISerializer),
						typeof(IDeserializer),
						typeof(IJsonCacheable),
						typeof(IJsonFactory)
					};
				var jsonTypes = typeof(DefaultJsonSerializer)
				                .Assembly
				                .DefinedTypes
				                .Where(t => t.Namespace == typeof(DefaultJsonSerializer).Namespace &&
				                            t.IsInterface)
				                .Except(nonEntityTypes);

				return jsonTypes.Select(t => new TestCaseData(t));
			}
		}

		[Test]
		[TestCaseSource(nameof(JsonTypes))]
		public void DefaultSerializerGeneratesAllRequiredJsonTypes_String(Type jsonType)
		{
			var deserializeMethod = typeof(DefaultJsonSerializer)
			                        .GetMethods()
			                        .First(m => m.Name == nameof(DefaultJsonSerializer.Deserialize) &&
			                                    m.GetParameters().First().ParameterType == typeof(string))
			                        .MakeGenericMethod(jsonType);
			var model = deserializeMethod.Invoke(DefaultJsonSerializer.Instance, new object[] {"{}"});

			model.Should().NotBeNull();
			model.Should().BeAssignableTo(jsonType);
		}

		[Test]
		[TestCaseSource(nameof(JsonTypes))]
		public void DefaultSerializerGeneratesAllRequiredJsonTypes_Response(Type jsonType)
		{
			var response = CreateResponse(jsonType);

			var deserializeMethod = typeof(DefaultJsonSerializer)
			                        .GetMethods()
			                        .First(m => m.Name == nameof(DefaultJsonSerializer.Deserialize) &&
			                                    m.GetParameters().First().ParameterType != typeof(string))
			                        .MakeGenericMethod(jsonType);
			var model = deserializeMethod.Invoke(DefaultJsonSerializer.Instance, new[] {response});

			model.Should().NotBeNull();
			model.Should().BeAssignableTo(jsonType);
		}

		private static object CreateResponse(Type contentType)
		{
			var responseType = typeof(IRestResponse<>)
				.MakeGenericType(contentType);
			var method = typeof(Mock)
			             .GetMethods()
			             .First(m => m.Name == nameof(Mock.Of) &&
			                         !m.GetParameters().Any())
			             .MakeGenericMethod(responseType);

			var response = (IRestResponse) method.Invoke(null, new object[] { });

			var mock = Mock.Get(response);

			mock.SetupGet(r => r.Content)
			    .Returns("{}");

			return response;
		}
	}
}
