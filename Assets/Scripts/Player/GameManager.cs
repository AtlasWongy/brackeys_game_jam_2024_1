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

    private PlayerClass player;

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

        // Initialize player object
        player = FindObjectOfType<PlayerClass>(); // Assuming PlayerClass is attached to a GameObject in the scene
        if (player == null)
        {
            Debug.LogError("PlayerClass not found in the scene.");
        }
    }

    private void Start()
    {
        if (player != null)
        {
            player.InitialStats(5, 6, 5, 0, 0);
        }
    }

    // Method to update player stats
    public void UpdatePlayerStats(Stats newStats)
    {
        
    }

    public void HandleEventOutcome(OptionEvent eventOption)
    {
        // Modify player stats based on event outcome
        player.AdjustStats(eventOption.wits, eventOption.guts, eventOption.heart, eventOption.good, eventOption.evil);

        // Update UI stat text
        UpdateUIStatText(eventOption);

        // Check win condition
        if (CheckWinCondition())
        {
            // Handle win condition
        }
    }

    // Method to update UI stat text
    public void UpdateUIStatText(OptionEvent eventOption)
    {
        Debug.LogFormat("{0}, {1}, {2}, {3}, {4}", eventOption.wits, eventOption.guts, eventOption.heart, eventOption.good, eventOption.evil);
        Debug.LogFormat("{0}, {1}, {2}, {3}, {4}", player.GetStats().Wits, player.GetStats().Guts, player.GetStats().Heart, player.GetStats().Good, player.GetStats().Evil);
        // Call UIManager method to update UI stat text
    }


    // Method to check the win condition
    private bool CheckWinCondition()
    {

        return player.GetStats().Good > player.GetStats().Evil;
    }
}