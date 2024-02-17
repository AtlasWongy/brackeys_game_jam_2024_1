using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable, Inspectable]
public class Stats
{
    [Inspectable]
    public int Health;

    [Inspectable]
    public int Wits;

    [Inspectable]
    public int Guts;

    [Inspectable]
    public int Heart;

    [Inspectable]
    public int Good;

    [Inspectable]
    public int Evil;

    public Stats(int health, int wits, int guts, int heart, int good, int evil)
    {
        Health = health;
        Wits = wits;
        Guts = guts;
        Heart = heart;
        Good = good;
        Evil = evil;
    }

    // Method to adjust stats
    public void AdjustStats(int healthChange, int witsChange, int gutsChange, int heartChange, int goodChange, int evilChange)
    {
        Health += healthChange;
        Wits += witsChange;
        Guts += gutsChange;
        Heart += heartChange;
        Good += goodChange;
        Evil += evilChange;
    }

}
