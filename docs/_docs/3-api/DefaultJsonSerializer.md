# DefaultJsonSerializer

Wrapper class for the Manatee.Json.Serializer for use with RestSharp.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- Object
- DefaultJsonSerializer

## Properties

### static [DefaultJsonSerializer](DefaultJsonSerializer#defaultjsonserializer) Instance { get; }

Provides a singleton instance.

## Methods

### T Deserialize&lt;T&gt;(string content)

Attempts to deserialize a RESTful response to the indicated type.

**Type Parameter:** T (no constraints)

The type of object expected.

**Parameter:** content

A string which contains the JSON to deserialize.

**Returns:** The requested object, if JSON is valid; null otherwise.

### string Serialize(Object obj)

Serializes an object to JSON.

**Parameter:** obj

The object to serialize.

**Returns:** An equivalent JSON string.

