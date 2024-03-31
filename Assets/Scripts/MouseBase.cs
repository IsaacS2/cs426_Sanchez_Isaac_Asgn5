using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseBase : MonoBehaviour
{
    private bool maskOn;
    private float timer;
    [SerializeField] private float defaultTimerVal = 1;  // what to set timer

    // Start is called before the first frame update
    void Start()
    {
        maskOn = false;
        timer = defaultTimerVal; 
    }

    private void Update()
    {
        if (maskOn)
        {
            timer -= Time.deltaTime;  // counting down before setting trap
        }
    }

    // Update is called once per frame
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            maskOn = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && timer > 0)
        {
            maskOn = false;
            timer = 1;
        }
    }
}
