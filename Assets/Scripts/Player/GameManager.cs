using System;
using System.Collections;
using System.Collections.Generic;
using Options;
using Player;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerPrefab;

    public Button[] buttons;
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

    private void Update()
    {
        if (FindObjectOfType<PlayerMovement>() == null)
        {
            LoadPlayer();
        }
    }

    public void LoadPlayer()
    {
        Player.PlayerMovement player = Instantiate(playerPrefab, new Vector3(-3.5f, 0.6f), Quaternion.identity);
    }

    private void LoadDoor()
    {
        
    }

    public void HandleEventOutcome(OptionEvent optionEvent)
    {
        DisableButtons();
        
        // Modify player stats based on event outcome
        Debug.LogFormat("Option Selected: {0}. Change in stats: {1} health, {2} wits, {3} guts, {4} heart, {5} good, {6} evil. You gained: {7} gold and {8} items.", optionEvent.optionName, optionEvent.stats.Health, optionEvent.stats.Wits, optionEvent.stats.Guts, optionEvent.stats.Heart, optionEvent.stats.Good, optionEvent.stats.Evil, optionEvent.rewardsObtained.Item2, optionEvent.rewardsObtained.Item1);

        Player.PlayerClass.PlayerInstance.AdjustStats(optionEvent.stats.Health, optionEvent.stats.Wits, optionEvent.stats.Guts, optionEvent.stats.Heart, optionEvent.stats.Good, optionEvent.stats.Evil);

        // Update UI stat text
        // UpdateUIStatText();

        // Check win condition
        // if (CheckWinCondition())
        // {
        //     Debug.Log("You Won! Game over.");
        // }
    }

    // Method to update UI stat text
    public void UpdateUIStatText()
    {
        Debug.LogFormat("Current Stats: {0} health, {1} wits, {2} guts, {3} heart, {4} good, {5} evil.", Player.PlayerClass.PlayerInstance.GetStats().Health, Player.PlayerClass.PlayerInstance.GetStats().Wits, Player.PlayerClass.PlayerInstance.GetStats().Guts, Player.PlayerClass.PlayerInstance.GetStats().Heart, Player.PlayerClass.PlayerInstance.GetStats().Good, Player.PlayerClass.PlayerInstance.GetStats().Evil);
        // Call UIManager method to update UI stat text
        UIStatsManager.UIStatsInstance.updateText("HP", Player.PlayerClass.PlayerInstance.GetStats().Health.ToString());
        UIStatsManager.UIStatsInstance.updateText("Brain", Player.PlayerClass.PlayerInstance.GetStats().Wits.ToString());
        UIStatsManager.UIStatsInstance.updateText("Guts", Player.PlayerClass.PlayerInstance.GetStats().Guts.ToString());
        UIStatsManager.UIStatsInstance.updateText("Heart", Player.PlayerClass.PlayerInstance.GetStats().Heart.ToString());

    }
    
    // Method to check the win condition
    private bool CheckWinCondition()
    {
        return Player.PlayerClass.PlayerInstance.GetStats().Good > Player.PlayerClass.PlayerInstance.GetStats().Evil;
    }
    
    public bool ResolvePlayerRoll(OptionEvent optionEvent){
        return(Player.PlayerClass.PlayerInstance.DieRoll(optionEvent));
        //return true;
    }

    private void DisableButtons()
    {
        foreach (var button in buttons)
        {
            DontDestroyOnLoad(button);
            button.interactable = false;
        }
    }

    public void EnableButtons()
    {
        foreach (var button in buttons)
        {
            button.interactable = true;
        }
    }
    
}