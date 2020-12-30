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
}
