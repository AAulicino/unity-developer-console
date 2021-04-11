using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    [ConsoleCommand("Hmm")]
    static string Hmm (string para)
    {
        return "yes! " + para;
    }
    [ConsoleCommand("GiveHealth")]
    static string GiveHealth (int health)
    {
        return "yes! " + health;
    }
    [ConsoleCommand("GiveMoney")]
    static string GiveMoney (int amount)
    {
        return "yes! " + amount;
    }
}
