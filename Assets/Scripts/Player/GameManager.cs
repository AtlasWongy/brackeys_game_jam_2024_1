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

    }


    public void HandleEventOutcome(OptionEvent optionEvent)
    {
        // Modify player stats based on event outcome
        Debug.LogFormat("{0}, {1}, {2}, {3}, {4}, {5}", optionEvent.stats.Health, optionEvent.stats.Wits, optionEvent.stats.Guts, optionEvent.stats.Heart, optionEvent.stats.Good, optionEvent.stats.Evil);

        PlayerClass.Instance.AdjustStats(optionEvent.stats.Health, optionEvent.stats.Wits, optionEvent.stats.Guts, optionEvent.stats.Heart, optionEvent.stats.Good, optionEvent.stats.Evil);

        // Update UI stat text
        UpdateUIStatText();

        // Check win condition
        if (CheckWinCondition())
        {
            Debug.Log("You Won!");
        }
    }

    // Method to update UI stat text
    public void UpdateUIStatText()
    {
        Debug.LogFormat("{0}, {1}, {2}, {3}, {4}, {5}", PlayerClass.Instance.GetStats().Health, PlayerClass.Instance.GetStats().Wits, PlayerClass.Instance.GetStats().Guts, PlayerClass.Instance.GetStats().Heart, PlayerClass.Instance.GetStats().Good, PlayerClass.Instance.GetStats().Evil);
        // Call UIManager method to update UI stat text
    }


    // Method to check the win condition
    private bool CheckWinCondition()
    {

        return PlayerClass.Instance.GetStats().Good > PlayerClass.Instance.GetStats().Evil;
    }
}