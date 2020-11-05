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
