using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetector : MonoBehaviour
{
    public GameObject trapKiller;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (trapKiller != null) {
                trapKiller.GetComponent<MouseTrap>().DeconstraintX();
            }
        }
    }
}
