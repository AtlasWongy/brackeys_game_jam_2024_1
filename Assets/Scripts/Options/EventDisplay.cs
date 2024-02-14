using System;
using System.Collections;
using System.Collections.Generic;
using Options;
using TMPro;
using UnityEngine;

public class EventDisplay : MonoBehaviour
{
    public OptionEvent optionEvent;

    public GameObject eventNameObj;
    public GameObject eventDescObj;
    
    private TextMeshProUGUI _eventName;
    private TextMeshProUGUI _eventDesc;

    private void Start()
    {
        _eventName = eventNameObj.GetComponent<TextMeshProUGUI>();
        _eventDesc = eventDescObj.GetComponent<TextMeshProUGUI>();

        _eventName.text = optionEvent.optionName;
        _eventDesc.text = optionEvent.description;
    }
}
