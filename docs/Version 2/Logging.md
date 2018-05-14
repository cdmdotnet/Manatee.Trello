All logging is provided by the `ILog` implementation stored in the Log property of the `TrelloConfiguration` class.  By default, logging is provided by an internal implementation which forwards all logging messages to the Debug window.

The `ILog` interface exposes the following methods:

- Debug - Logs a debug message.
- Info - Logs an informational message.
- Error - Logs an exception with an option to throw the exception once logged.

This interface must be implemented to use the logging solution of your preference.  If, at run-time, this logging implementation is removed from the `TrelloConfiguration` class (by setting the Log property to null), it will reset to the default implementation.
