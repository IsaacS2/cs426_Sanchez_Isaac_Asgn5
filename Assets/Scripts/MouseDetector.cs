using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetector : MonoBehaviour
{
    public GameObject trapKiller;  // mouse clamp used to trap player

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))  // player collision
        {
            if (trapKiller != null)  // trap has not been destroyed yet
            {
                GetComponent<BoxCollider>().enabled = false;  // prevent player from activating trap again
                if (trapKiller.GetComponent<MouseTrap>() != null)  // ensure trap can have state changed
                {
                    trapKiller.GetComponent<MouseTrap>().ActivateTrap();  // set trap to state 1 (activated)
                }
            }
        }
    }
}
