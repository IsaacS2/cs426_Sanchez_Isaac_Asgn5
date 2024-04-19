using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseTrapManager : MonoBehaviour
{
    public static MouseTrapManager FM;
    public GameObject[] allMouseTrap;
    // Start is called before the first frame update
    void Start()
    {
        //allMouseTraps 
        allMouseTrap = GameObject.FindGameObjectsWithTag("KillerTrap");
    }
}
