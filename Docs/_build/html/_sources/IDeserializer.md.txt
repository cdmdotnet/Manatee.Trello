# IDeserializer

Defines methods required by the IRestClient to deserialize a response from JSON to an object.

**Assembly:** Manatee.Trello.dll

**Namespace:** Manatee.Trello.Json

**Inheritance hierarchy:**

- IDeserializer

## Methods

### T Deserialize&lt;T&gt;(string content)

Attempts to deserialize a RESTful response to the indicated type.

**Type Parameter:** T (no constraints)

The type of object expected.

**Parameter:** content

A string which contains the JSON to deserialize.

**Returns:** The requested object, if JSON is valid; null otherwise.

