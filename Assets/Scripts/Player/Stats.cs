using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Stats
{
    public int Wits { get; set; }
    public int Guts { get; set; }
    public int Heart { get; set; }
    public int Good { get; set; }
    public int Evil { get; set; }

    public Stats(int wits, int guts, int heart, int good, int evil)
    {
        Wits = wits;
        Guts = guts;
        Heart = heart;
        Good = good;
        Evil = evil;
    }

    // Method to adjust stats
    public void AdjustStats(int witsChange, int gutsChange, int heartChange, int goodChange, int evilChange)
    {
        Wits += witsChange;
        Guts += gutsChange;
        Heart += heartChange;
        Good += goodChange;
        Evil += evilChange;
    }

}
