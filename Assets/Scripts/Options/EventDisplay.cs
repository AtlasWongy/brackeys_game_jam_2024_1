using Options;
using Player;
using Singletons;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EventDisplay : MonoBehaviour
{
    public OptionEvent optionEvent;
    public string optionEventFolder;

    public Button eventButton;
    private TextMeshProUGUI _eventDesc;
    private bool _playerWins;
    private OptionEvent[] optionEventList;

    private void Start()
    {
        optionEventList = Resources.LoadAll<OptionEvent>(optionEventFolder);
        LoadEvent();

        // Subscribe to the button's onClick event
        eventButton.onClick.AddListener(OnClick);
    }

    private void LoadEvent()
    {
        eventButton.GetComponent<EventDisplay>().optionEvent = getOptionEvent();

        _eventDesc = eventButton.GetComponentInChildren<TextMeshProUGUI>();

        _eventDesc.text = optionEvent.description;
    }

    private void OnClick()
    {
        //optionEvent.InvokeEvent();
        bool encounterSuccess = GameManager.Instance.ResolvePlayerRoll(optionEvent);
        if (encounterSuccess)
        {
            optionEvent.EventSuccess();
            Debug.LogFormat("Wow! you won!");
            // _playerWins = true;
        }
        else
        {
            optionEvent.EventFailure();
            Debug.LogFormat("Oh no! You lost!");
            // _playerWins = false;
        }

        GameManager.Instance.HandleEventOutcome(optionEvent);
    }

    private OptionEvent getOptionEvent()
    {
        Debug.Log(optionEventList.Length);
        if (optionEventList.Length > 0)
        {
            while (true)
            {
                int randomIndex = Random.Range(0, optionEventList.Length);

                OptionEvent assignedOptionEvent = optionEventList[randomIndex];
                Debug.Log(GameManager.Instance.TrackEventOptions(null));
                if (!GameManager.Instance.TrackEventOptions(null).Contains(assignedOptionEvent.optionName))
                {
                    GameManager.Instance.TrackEventOptions(assignedOptionEvent.optionName);
                    return assignedOptionEvent;
                }
                else
                {
                    continue;
                };
            }
        } 
        else
        {
            return optionEvent;

        }
    }
}
