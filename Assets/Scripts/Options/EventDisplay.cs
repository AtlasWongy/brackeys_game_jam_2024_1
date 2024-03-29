using Options;
using Player;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventDisplay : MonoBehaviour
{
    //public OptionEvent optionEvent;
    public string optionEventFolder;

    public Button eventButton;
    public TextMeshProUGUI _eventDesc;

    public OptionEvent optionEvent;

    private void Start()
    {
        GameManager.Instance.LoadEvent(eventButton, optionEvent);
        // Subscribe to the button's onClick event
        eventButton.onClick.AddListener(OnClick);
    }


    private void OnClick()
    {
        //optionEvent.InvokeEvent();
        bool encounterSuccess = GameManager.Instance.ResolvePlayerRoll(optionEvent);
        if (encounterSuccess)
        {
            optionEvent.EventSuccess();
            // _playerWins = true;
        }
        else
        {
            optionEvent.EventFailure();
            // _playerWins = false;
        }

        GameManager.Instance.HandleEventOutcome(optionEvent);
    }

}
