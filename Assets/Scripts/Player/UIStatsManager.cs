using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class UIStatsManager : MonoBehaviour
{
    public GameObject Stats;
     public static UIStatsManager UIStatsInstance;
    // Start is called before the first frame update


    void Awake() => UIStatsInstance = this;
    // Start is called before the first frame update
    void Start()
    {
        updateText("Heart","Heart: Infinity");
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateText(string StatName, string newText){
        GameObject.Find(StatName).GetComponentInChildren<TextMeshProUGUI>().text = newText;
    }

    
}
