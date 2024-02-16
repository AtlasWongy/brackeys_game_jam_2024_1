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

//     public Button eventButton;
    public GameObject eventDescObj;
    private TextMeshProUGUI _eventDesc;

    private void Start()
    {
        _eventDesc = eventDescObj.GetComponent<TextMeshProUGUI>();

        _eventDesc.text = optionEvent.description;

    

        // Subscribe to the button's onClick event
        eventButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        optionEvent.InvokeEvent();
    }
}
