using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskLaunchScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private bool canLaunch, forceIncreasing, chargingForce;
    // public float setForce;  static force used for first task
    private float posTimer;  // timer that determines if mask is still for ~1 second
    private Vector3 prevLocation;

    [SerializeField] private float forceVal = 0, forceRateChange = 4, maxForce = 5;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posTimer = 0;
        canLaunch = true;
        forceIncreasing = true;
        prevLocation = rb.position;
    }

    // Update is called once per frame
    private void Update()
    {
        // checking if mask can be launched now
        if (Input.GetButtonDown("Jump") && canLaunch && !chargingForce)
        {
            Debug.Log("Launch Mask");
            //rb.AddForce((Vector3.up + this.transform.forward) * setForce, ForceMode.Impulse);  previous force applied for first task
            chargingForce = true;  // force will now begin being charged
        }

        // currently charging force
        if (Input.GetButton("Jump") && chargingForce)
        {
            if (forceIncreasing)
            {
                forceVal += Time.deltaTime * forceRateChange;
            }
            else
            {
                forceVal -= Time.deltaTime * forceRateChange;
            }
            if (forceVal >= maxForce)
            {
                forceIncreasing = false;
            }
            if (forceVal <= 0)
            {
                forceIncreasing = true;
            }
        }

        // force charging button (space) has been released; time to launch mask!
        if (Input.GetButtonUp("Jump"))
        {
            Debug.Log(forceVal);  // display charged force
            rb.AddForce((Vector3.up + this.transform.forward) * forceVal, ForceMode.Impulse);  // apply current charged force

            // Reset force values
            forceVal = 0;
            forceIncreasing = true;
            chargingForce = false;
        }
    }

    void FixedUpdate()
    {
        if (prevLocation != rb.position)  // mask is moving
        {
            prevLocation = rb.position;
            posTimer = 1f;  // reset movement-tracking timer
            canLaunch = false;
        }
        else
        {
            posTimer -= Time.fixedDeltaTime;  // reduce timer value since mask is not moving
            Debug.Log(posTimer);
            if (posTimer <= 0)  // mask can be launched again
            {
                canLaunch = true;
                posTimer = 0;
            }
        }
    }

    public float getMaxForce()
    {
        return maxForce;
    }

    public float getCurrentForce() 
    {
        return forceVal;
    }
}
