using UnityEngine;
using UnityEngine.UI;
using AhmKam;

public class DemoText : MonoBehaviour
{
    public Text uiText;

    void Start()
    {
        // Subscribe
        SimpleMessageBroker.Subscribe<FooArgs>("foo_id", FooWithMessage);
    }

    // Function
    public void FooWithMessage(FooArgs arg) => Debug.Log(arg.value);

    private void OnDestroy()
    {
        // Unsubscribe
        SimpleMessageBroker.Unsubscribe<FooArgs>("foo_id", FooWithMessage);
    }

}

public class FooArgs
{
    public string value;
}
