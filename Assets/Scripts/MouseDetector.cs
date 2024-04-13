using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseDetector : MonoBehaviour
{
    public GameObject trapKiller;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (trapKiller != null)
            {
                GetComponent<BoxCollider>().enabled = false;
            }
        }
    }
}
