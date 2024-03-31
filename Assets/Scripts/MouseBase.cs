using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBase : MonoBehaviour
{
    private bool maskOn;
    private float timer;
    private GameObject currentMask;
    [SerializeField] private float defaultTimerVal = 0.5f;  // what to set timer
    [SerializeField] private GameObject trapKiller;

    // Start is called before the first frame update
    void Start()
    {
        maskOn = false;
        timer = defaultTimerVal; 
    }

    private void Update()
    {
        if (trapKiller != null) {
            if (maskOn)
            {
                timer -= Time.deltaTime;  // counting down before setting trap
                if (timer <= 0)
                {
                    Debug.Log("Drop the trap!");
                    trapKiller.GetComponent<MouseTrap>().DeconstraintX();
                    trapKiller = null;
                }
            }
        }
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("mask Obtained!");
        if (collision.gameObject.CompareTag("Player"))  // mask entered trap
        {
            Debug.Log("mask Obtained!");
            maskOn = true;
            currentMask = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && timer > 0)  // mask left before trap closed
        {
            maskOn = false;
            timer = defaultTimerVal;
        }
    }

    public bool maskIsOn()
    {
        return maskOn;
    }
}
