using System;
using System.Collections;
using System.Collections.Generic;
using Options;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Static instance of GameManager which allows it to be accessed by any other script.
    public static GameManager Instance { get; private set; }

    

    private int? _PersonalityDemand = null;
    public int? PersonalityDemand{
        get{return _PersonalityDemand;}
        set{ if (_PersonalityDemand==null) _PersonalityDemand = value;}
    }

    private int? _AttributeDemand = null;
    public int? AttributeDemand{
        get{return _AttributeDemand;}
        set{ if (_AttributeDemand==null) _AttributeDemand = value;}
    }

    private int? _AchievementDemand = null;
    public int? AchievementDemand{
        get{return _AchievementDemand;}
        set{ if (_AchievementDemand==null) _AchievementDemand = value;}
    }

    //TRACKERS FOR GAME DATA

    private int CombatSuccesses;
    private int CombatDefeats;
    private int EventSuccesses;
    private int EventDefeats;

    private int DemandsMet = 0;

    private int DoorCounter = 0;


    private void Awake()
    {
        // Singleton pattern implementation.
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
            return; // Ensures the rest of the Awake method doesn't execute for the duplicate GameManager
        }

        // Don't destroy GameManager when loading new scenes.
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        //UpdateUIStatText();
        PrincessStartGenerate();
    }

    public void HandleEventOutcome(OptionEvent optionEvent)
    {
        // Modify player stats based on event outcome
        Debug.LogFormat("Option Selected: {0}. Change in stats: {1} health, {2} wits, {3} guts, {4} heart, {5} good, {6} evil. You gained: {7} gold and {8} items.", optionEvent.optionName, optionEvent.stats.Health, optionEvent.stats.Wits, optionEvent.stats.Guts, optionEvent.stats.Heart, optionEvent.stats.Good, optionEvent.stats.Evil, optionEvent.rewardsObtained.Item2, optionEvent.rewardsObtained.Item1);

        PlayerClass.PlayerInstance.AdjustStats(optionEvent.stats.Health, optionEvent.stats.Wits, optionEvent.stats.Guts, optionEvent.stats.Heart, optionEvent.stats.Good, optionEvent.stats.Evil);

        // Update UI stat text
        UpdateUIStatText();

        DoorCounter +=1;
        if(DoorCounter==1){
            PrincessEndCheck();
        }

        // Check win condition
        //if (CheckWinCondition())
        //{
        //    Debug.Log("You Won! Game over.");
        //}
    }

    public void PrincessStartGenerate(){
        PersonalityDemand = UnityEngine.Random.Range(1,3);
        AttributeDemand = UnityEngine.Random.Range(1,3);
        AchievementDemand = UnityEngine.Random.Range(1,3);

        //THIS SHOULD BE 11, 3 NOW FOR TESTING

        Debug.LogFormat("{0},{1},{2} are the demands",PersonalityDemand,AttributeDemand,AchievementDemand);

      
    }

    public void PrincessEndCheck(){
        DemandsMet = 0;
        //need 2 out of 3 right to win the game

        switch(PersonalityDemand){
            case 1:
                //CHECK IF HERO IS BRAVE
                if(PlayerClass.PlayerInstance.IsPersonalityBrave()){
                    DemandsMet += 1;
                }
                break;
            case 2:
                //CHECK IF HERO IS COWARDLY
                //etc etc.
                if(PlayerClass.PlayerInstance.IsPersonalityCowardly()){
                    DemandsMet += 1;
                }
                break;
        }
        
        switch(AchievementDemand){
            case 1:
                //DONT KILL TOO MANY THINGS
                break;
            case 2:
                //DONT DIE TOO MANY TIMES
                // etc. etc
                break;
        }

        Debug.Log(DemandsMet.ToString());
    }

    // Method to update UI stat text
    public void UpdateUIStatText()
    {
        Debug.LogFormat("Current Stats: {0} health, {1} wits, {2} guts, {3} heart, {4} good, {5} evil.", PlayerClass.PlayerInstance.GetStats().Health, PlayerClass.PlayerInstance.GetStats().Wits, PlayerClass.PlayerInstance.GetStats().Guts, PlayerClass.PlayerInstance.GetStats().Heart, PlayerClass.PlayerInstance.GetStats().Good, PlayerClass.PlayerInstance.GetStats().Evil);
        // Call UIManager method to update UI stat text
        UIStatsManager.UIStatsInstance.updateText("HP", PlayerClass.PlayerInstance.GetStats().Health.ToString());
        UIStatsManager.UIStatsInstance.updateText("Brain", PlayerClass.PlayerInstance.GetStats().Wits.ToString());
        UIStatsManager.UIStatsInstance.updateText("Guts", PlayerClass.PlayerInstance.GetStats().Guts.ToString());
        UIStatsManager.UIStatsInstance.updateText("Heart", PlayerClass.PlayerInstance.GetStats().Heart.ToString());

    }


    // Method to check the win condition
    private bool CheckWinCondition()
    {
        return PlayerClass.PlayerInstance.GetStats().Good > PlayerClass.PlayerInstance.GetStats().Evil;
    }
    
    public bool ResolvePlayerRoll(OptionEvent optionEvent){
        return(PlayerClass.PlayerInstance.DieRoll(optionEvent));
        //return true;
    }
}