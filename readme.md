# Simple Message Broker

A simple message broker for C# and Unity inspired by UniRx message broker

### Usage
Copy the Plugins/SimpleMessageBroker folder into your project or download the [unity package](https://github.com/ahmkam/unity-simple-message-broker/releases/download/1.0/UnitySimpleMessageBroker-v1.0.unitypackage) from release


Define a message class
```csharp
public class PersonData
{
    public string name;
    public int age;

    public override string ToString() => $"Name:{name}, Age:{age}";
}
```

Subscribe to listen the message
``` csharp
using UnityEngine;
using UnityEngine.UI;
using AhmKam;

public class DemoText : MonoBehaviour
{
    public Text uiText;

    void Start() => SimpleMessageBroker.Subscribe<PersonData>(UpdateText);

    private void UpdateText(PersonData person) => uiText.text = person.ToString();

    private void OnDestroy() => SimpleMessageBroker.Unsubscribe<PersonData>(UpdateText);
}

```

Publish the message
``` csharp
using UnityEngine;
using AhmKam;

public class DemoButton : MonoBehaviour
{
    public void OnButtonClicked()
    {
        SimpleMessageBroker.Publish<PersonData>(new PersonData()
        {
            name = $"User {UnityEngine.Random.Range(1, 1000)}",
            age = UnityEngine.Random.Range(1, 100)
        });
    }
}
```
-------------
### Note!!

Make sure to unsubscribe the message!

-------------
### Todo
- Filtering messages belonging to the same message class
- Utility methods
- Error checking