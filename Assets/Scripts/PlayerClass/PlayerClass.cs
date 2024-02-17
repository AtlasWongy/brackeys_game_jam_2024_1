using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerClass : MonoBehaviour
{
    int wits = 5;
    int guts = 6;
    int heart = 5;
    int good = 0;
    int evil = 0;
    // Start is called before the first frame update
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

    bool DieRoll(){
        //apply ecounter stat modifier first
        int highestStat = GetHighestStat();

        int dieRoll = UnityEngine.Random.Range(1,11);

        return highestStat > dieRoll;
    }

    bool IsGoodHigherThanEvil(){
        return good > evil;
    }
}
