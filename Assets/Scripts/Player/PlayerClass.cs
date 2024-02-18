using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Options;

namespace Player
{
   public class PlayerClass : MonoBehaviour
   {
       public static PlayerClass PlayerInstance;
   
       private Stats playerStats;
       private int _gold;
   
       private void Awake()
       {
           // Singleton pattern implementation.
           if (PlayerInstance == null)
           {
               PlayerInstance = this;
           }
           else if (PlayerInstance != this)
           {
               Destroy(gameObject);
               return; // Ensures the rest of the Awake method doesn't execute for the duplicate GameManager
           }
   
           // Don't destroy player when loading new scenes.
           DontDestroyOnLoad(gameObject);
       }
   
       void Start()
       {
           InitialStats(10, 5, 6, 5, 0, 0);
           _gold = 0;
       }
   
       // Update is called once per frame
       void Update()
       {
           
       }
   
       public void InitialStats(int health, int wits, int guts, int heart, int good, int evil)
       {
           playerStats = new Stats(health, wits, guts, heart, good, evil);
       }
   
       public void AdjustStats(int healthChange, int witsChange, int gutsChange, int heartChange, int goodChange, int evilChange)
       {
           playerStats.AdjustStats(healthChange, witsChange, gutsChange, heartChange, goodChange, evilChange);
       }

       public void AdjustGold(int goldChanged)
       {
           _gold = +goldChanged;
       }
   
   
       // Method to get player stats
       public Stats GetStats()
       {
           return playerStats;
       }

       public int GetGold()
       {
           return _gold;
       }
   
       int GetHighestStat(OptionEvent optionEvent){
           return Math.Max(Math.Max(playerStats.Wits + optionEvent.stats.Wits, playerStats.Guts+optionEvent.stats.Guts), playerStats.Heart +optionEvent.stats.Guts);
       }
   
   
       public bool DieRoll(OptionEvent optionEvent){
           int highestStat = GetHighestStat(optionEvent);
   
           Debug.LogFormat("Your highest stat is {0}",highestStat);
   
           int dieRoll = UnityEngine.Random.Range(1,11);
   
           Debug.LogFormat("Enemy rolled a {0}",dieRoll);
   
           return highestStat > dieRoll;
       }
   
       bool IsGoodHigherThanEvil(){
           return playerStats.Good > playerStats.Evil;
       }
   } 
}


    public bool IsGoodHigherThanEvil(){
        return playerStats.Good > playerStats.Evil;
    }

    public bool IsPersonalityBrave(){
        return playerStats.Guts > 7;
    }

    public bool IsPersonalityCowardly(){
        return playerStats.Guts <= 3;
    }

    public bool IsPersonalityCunning(){
        return playerStats.Wits > 7;
    }

    public bool IsPersonalityDull(){
        return playerStats.Wits <= 3;
    }

    public bool IsPersonalityNoble(){
        return playerStats.Heart > 7;
    }

    public bool IsPersonalitySelfish(){
        return playerStats.Heart <= 3;
    }
}
