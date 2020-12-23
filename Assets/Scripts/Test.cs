using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    // Start is called before the first frame update
    void Start ()
    {
        DeveloperConsole.Initialize();
    }

    [ConsoleCommand("Hmm")]
    static string Hmm (string para)
    {
        return "yes! " + para;
    }
}
