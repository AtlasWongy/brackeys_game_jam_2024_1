using System;
using System.Collections;
using System.Collections.Generic;
using Options;
using TMPro;
using UnityEngine;

public class EventDisplay : MonoBehaviour
{
    public OptionEvent optionEvent;
    public GameObject eventDescObj;
    private TextMeshProUGUI _eventDesc;

    private void Start()
    {
        _eventDesc = eventDescObj.GetComponent<TextMeshProUGUI>();
        _eventDesc.text = optionEvent.description;
    }
}
