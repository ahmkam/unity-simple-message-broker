# Simple Message Broker

A simple message broker for C# and Unity inspired by UniRx message broker

### Usage
Copy the Plugins/SimpleMessageBroker folder into your project or download the latest [unity package](https://github.com/ahmkam/unity-simple-message-broker/releases/download/2.0/UnitySimpleMessageBroker-v2.0.unitypackage) from releases

<br />

Sending / receiving empty message
```csharp
// Your function
public void Foo() => Debug.Log("Foo");

// Subscribe message
SimpleMessageBroker.Subscribe("foo_id", Foo);

// Publish message
SimpleMessageBroker.Publish("foo_id");

// Unsubscribe message
SimpleMessageBroker.Unsubscribe("foo_id", Foo);
```

Sending / receiving with a message class. An ID can be given if you want to filter messages of the same class

```csharp
// Message class
public class FooArgs
{
    public string value;
}

// Your function
public void FooWithMessage(FooArgs arg) => Debug.Log(arg.value);

// Subscribe message
SimpleMessageBroker.Subscribe<FooArgs>("foo_id", FooWithMessage);

// Publish
SimpleMessageBroker.Publish<FooArgs>("foo_id", new FooArgs() { value = "hello world" });

// Unsubscribe
SimpleMessageBroker.Unsubscribe<FooArgs>("foo_id", FooWithMessage);
```

-------------
### Todo
- ~~Filtering messages belonging to the same message class~~
- Utility methods
- Error checking