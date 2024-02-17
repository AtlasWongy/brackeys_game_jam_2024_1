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
        UpdateUIStatText();
    }

    public void HandleEventOutcome(OptionEvent optionEvent)
    {
        // Modify player stats based on event outcome
        Debug.LogFormat("Option Selected: {0}. Change in stats: {1} health, {2} wits, {3} guts, {4} heart, {5} good, {6} evil. You gained: {7} gold and {8} items.", optionEvent.optionName, optionEvent.stats.Health, optionEvent.stats.Wits, optionEvent.stats.Guts, optionEvent.stats.Heart, optionEvent.stats.Good, optionEvent.stats.Evil, optionEvent.rewardsObtained.Item2, optionEvent.rewardsObtained.Item1);

        PlayerClass.PlayerInstance.AdjustStats(optionEvent.stats.Health, optionEvent.stats.Wits, optionEvent.stats.Guts, optionEvent.stats.Heart, optionEvent.stats.Good, optionEvent.stats.Evil);

        // Update UI stat text
        UpdateUIStatText();

        // Check win condition
        if (CheckWinCondition())
        {
            Debug.Log("You Won! Game over.");
        }
    }

    // Method to update UI stat text
    public void UpdateUIStatText()
    {
        Debug.LogFormat("Current Stats: {0} health, {1} wits, {2} guts, {3} heart, {4} good, {5} evil.", PlayerClass.PlayerInstance.GetStats().Health, PlayerClass.PlayerInstance.GetStats().Wits, PlayerClass.PlayerInstance.GetStats().Guts, PlayerClass.PlayerInstance.GetStats().Heart, PlayerClass.PlayerInstance.GetStats().Good, PlayerClass.PlayerInstance.GetStats().Evil);
        // Call UIManager method to update UI stat text
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