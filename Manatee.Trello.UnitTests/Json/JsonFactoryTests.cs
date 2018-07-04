using System;
using System.Collections;
using System.Linq;
using FluentAssertions;
using Manatee.Trello.Json;
using NUnit.Framework;

namespace Manatee.Trello.UnitTests.Json
{
	[TestFixture]
	public class JsonFactoryTests
	{
		[SetUp]
		public void Setup()
		{
			TrelloAuthorization.Default.AppKey = "test";
		}

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
				var jsonTypes = typeof(DefaultJsonFactory).Assembly
				                                          .DefinedTypes
				                                          .Where(t => t.Namespace == typeof(DefaultJsonFactory).Namespace &&
				                                                      t.IsInterface)
				                                          .Except(nonEntityTypes);

				return jsonTypes.Select(t => new TestCaseData(t));
			}
		}

		[Test]
		[TestCaseSource(nameof(JsonTypes))]
		public void DefaultFactoryGeneratesAllRequiredJsonTypes(Type jsonType)
		{
			var createMethod = typeof(DefaultJsonFactory).GetMethod(nameof(DefaultJsonFactory.Create))
			                                             .MakeGenericMethod(jsonType);
			var model = createMethod.Invoke(DefaultJsonFactory.Instance, new object[] { });

			model.Should().NotBeNull();
			model.Should().BeAssignableTo(jsonType);
		}
	}
}
