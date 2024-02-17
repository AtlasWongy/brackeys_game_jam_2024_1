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

    private void Awake()
    {
        // Singleton pattern implementation.
        if (UIStatsInstance == null)
        {
            UIStatsInstance = this;
        }
        else if (UIStatsInstance != this)
        {
            Destroy(gameObject);
            return; // Ensures the rest of the Awake method doesn't execute for the duplicate GameManager
        }

        // Don't destroy UIStats when loading new scenes.
        // DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.UpdateUIStatText();
        //updateText("Heart", "Heart: Infinity");
        //updateText("HP", PlayerClass.PlayerInstance.GetStats().Health.ToString());
        //updateText("Brain", PlayerClass.PlayerInstance.GetStats().Wits.ToString());
        //updateText("Guts", PlayerClass.PlayerInstance.GetStats().Guts.ToString());
        //updateText("Heart", PlayerClass.PlayerInstance.GetStats().Heart.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        // GameManager.Instance.UpdateUIStatText();
    }

    public void updateText(string StatName, string newText){
        GameObject.Find(StatName).GetComponentInChildren<TextMeshProUGUI>().text = newText;
    }

    
}
