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
    
    private TextMeshProUGUI[] _event;

    private void Start()
    {
        _event = eventButton.GetComponentsInChildren<TextMeshProUGUI>();

        _event[0].text = optionEvent.optionName;
        _event[1].text = optionEvent.description;

        // Subscribe to the button's onClick event
        eventButton.onClick.AddListener(OnClick);
    }

    private void OnClick()
    {
        optionEvent.InvokeEvent();
    }
}
