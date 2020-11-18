using System;
using UnityEngine;
using AhmKam;

public class DemoButton : MonoBehaviour
{
    public void OnButtonClicked()
    {
        // Publish
        SimpleMessageBroker.Publish<FooArgs>("foo_id", new FooArgs() { value = "hello world" });
    }
}

