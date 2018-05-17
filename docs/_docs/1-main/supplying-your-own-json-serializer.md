---
title: Supplying your own JSON serializer
category:
order: 5
---

You can instruct the library to use a custom JSON serializer.  The default serializer uses [Manatee.Json](https://github.com/gregsdennis/Manatee.Json).  This is my own JSON serializer, and it's the best... because it's mine.

> **NOTE** Once upon a time, I had implemented a serializer based on Newtonsoft's Json.Net.  But due to several complexities of the Trello API contract, I didn't feel I was enough of a Json.Net wizard to tackle these, so I dropped it.

> **ANOTHER NOTE** If you consider yourself strong in the ways of Newtonsoft's Json.Net, please feel free to tackle this library and publish it yourself.

If you prefer to use a different JSON serializer, the method is similar to that of using your own REST provider.  In the `Manatee.Trello.Json` namespace, you will find a bunch of interfaces, all of which must be implemented.  Two will be wrappers for the serializer itself, one will be a factory responsible for creating instances of all of the DTO interfaces, while the others will be concrete implementations for the various object types which Manatee.Trello requests through the REST provider.

- `ISerializer` / `IDeserializer` - A distinction between serializer and deserializer functionality has been made for ultimate compatiblity.  In most cases, they will be the same object, however exposing this functionality in this way allows Manatee.Trello to support the case that your preferred serializer uses separate objects or that you prefer different libraries to provide this functionality.  (And for you software design nuts out there, it support Interface Segregation: the **I** in SOLID.)
- `IJsonFactory` - All of the objects in Manatee.Trello use the JSON objects for data contexts.  This factory provides a mechanism for the internals to create instances of those JSON objects.
- <code>I<i>PickAnEntity</i></code> - There are quite a few entity interfaces for which you will need to provide functionality.  Furthermore, the serializer and deserializer will be called using these interfaces, so you'll need to ensure that your implementation of `IDeserializer` is configured properly to create instances of your entity implementations when the entity interface is supplied as the type.  (For example, if `IJsonCard` is requested by Manatee.Trello, your deserializer will need to know to create an instance of your `MyJsonCard` class which implements `IJsonCard`.)

Once all of the interfaces have been implemented, you can configure Manatee.Trello to use your serializer by setting the `Serializer` and `Deserializer` static properties on the `TrelloServiceConfiguration` static class.

```csharp
public class MyJsonSerializerWrapper : ISerializer, IDeserializer
{
    ...
}
public class MyJsonFactory : IJsonFactory
{
    ...
}

public static void Main()
{
    ...
    var serializer = new MyJsonSerializerWrapper();
    TrelloConfiguration.Serializer = serializer;
    TrelloConfiguration.Deserializer = serializer;
    TrelloConfiguration.JsonFactory = new MyJsonFactory();
    ...
}
```

To aid in deciding which properties should and should not be serialized, three non-functional attributes have been created and placed on all of the JSON interface properties:

- `JsonDeserializeAttribute` - Indicates that the property should be deserialized if a value is present.
- `JsonSerializeAttribute` - Indicates that a property should be serialized if a value is present.  Also exposes an `IsRequired` boolean property to indicate whether Trello requires this property.
- `JsonSpecialSerializationAttribute` - Indicates that Trello expects special serialization for a given property.  Refer to the [Trello API documentation](https://developers.trello.com/v1.0/reference#introduction) for more information regarding these properties.

Lastly, many of the DTO interaces implement `IAcceptId`.  These DTOs may be received as mere the entity's ID string instead of the entire body.  When a string is received, it must be deserialized as the full object with only the `Id` property populated.  When an object is received, deserialize all applicable properties and set the `ValidForMerge` property to `true`.  In this way, Manatee.Trello will know that this DTO can be used as a new data source for that entity.

That's it.  Manatee.Trello will now use your JSON serializer through the wrappers you have provided.
