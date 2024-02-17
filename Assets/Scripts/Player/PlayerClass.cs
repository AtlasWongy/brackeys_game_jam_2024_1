using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerClass : MonoBehaviour
{
    public static PlayerClass Instance;

    private Stats playerStats;

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

        // Don't destroy player when loading new scenes.
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        InitialStats(10, 5, 6, 5, 0, 0);
        Debug.LogFormat(DieRoll().ToString());
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

    int GetHighestStat(){
        return Math.Max(Math.Max(playerStats.Wits, playerStats.Guts), playerStats.Heart);
    }

    // Method to get player stats
    public Stats GetStats()
    {
        return playerStats;
    }

    bool DieRoll(){
        //apply ecounter stat modifier first
        int highestStat = GetHighestStat();

        int dieRoll = UnityEngine.Random.Range(1,11);

        return highestStat > dieRoll;
    }

    bool IsGoodHigherThanEvil(){
        return playerStats.Good > playerStats.Evil;
    }
}
