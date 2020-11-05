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
