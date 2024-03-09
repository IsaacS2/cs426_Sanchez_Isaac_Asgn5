using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskLaunchScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private bool canLaunch;
    public float setForce;
    private float posTimer;
    private Vector3 prevLocation;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posTimer = 0;
        canLaunch = false;
        prevLocation = rb.position;
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetButtonDown("Jump") && canLaunch)
        {
            Debug.Log("Launch Mask");
            rb.AddForce((Vector3.up + Vector3.forward) * setForce, ForceMode.Impulse);
            canLaunch = false;
        }
    }

    void FixedUpdate()
    {
        if (prevLocation != rb.position)
        {
            prevLocation = rb.position;
            posTimer = 1f;
            canLaunch = false;
        }
        else
        {
            posTimer -= Time.fixedDeltaTime;
            Debug.Log(posTimer);
            if (posTimer <= 0)
            {
                canLaunch = true;
                posTimer = 0;
                Debug.Log(canLaunch);
            }
        }
    }
}
