using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Options;

public class PlayerClass : MonoBehaviour
{
    private int wits = 5;
    private int guts = 6;
    private int heart = 5;
    private int good = 0;
    private int evil = 0;
    // Start is called before the first frame update
    public static PlayerClass PlayerInstance;

    void Awake() => PlayerInstance = this;
    void Start()
    {
        Debug.LogFormat(DieRoll().ToString());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    int GetHighestStat(){
        return Math.Max(Math.Max(wits,guts),heart);
    }

    int GetHighestStat(OptionEvent optionEvent){
        return Math.Max(Math.Max(wits+optionEvent.wits,guts+optionEvent.guts),heart+optionEvent.guts);
    }

    public bool DieRoll(){
        //apply ecounter stat modifier first
        int highestStat = GetHighestStat();

        int dieRoll = UnityEngine.Random.Range(1,11);

        return highestStat > dieRoll;
    }

    public bool DieRoll(OptionEvent optionEvent){
        int highestStat = GetHighestStat(optionEvent);

        Debug.LogFormat("Your highest stat is {0}",highestStat);

        int dieRoll = UnityEngine.Random.Range(1,11);

        Debug.LogFormat("Enemy rolled a {0}",dieRoll);

        return highestStat > dieRoll;
    }

    bool IsGoodHigherThanEvil(){
        return good > evil;
    }
}
