using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalVariables : MonoBehaviour
{
    public static GlobalVariables FM;  // static object that will keep track of when a player has won the level
    public bool win;
    // Start is called before the first frame update
    void Start()
    {
        win = false;
        FM = this;
    }
}
