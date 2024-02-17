using System;
using System.Collections;
using System.Collections.Generic;
using Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EventDisplay : MonoBehaviour
{
    public OptionEvent optionEvent;

    public Button eventButton;
    private TextMeshProUGUI _eventDesc;

    private void Start()
    {
        _eventDesc = eventButton.GetComponentInChildren<TextMeshProUGUI>();

        _eventDesc.text = optionEvent.description;

    

        // Subscribe to the button's onClick event
        eventButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        //optionEvent.InvokeEvent();
        bool encounterSuccess = GameManager.Instance.ResolvePlayerRoll(optionEvent);
        if (encounterSuccess){
            optionEvent.EventSuccess();
            Debug.LogFormat("Wow! you won!");
        }
        else{
            optionEvent.EventFailure();
            Debug.LogFormat("Oh no! You lost!");
        }
        
        GameManager.Instance.HandleEventOutcome(optionEvent);
    }
}
