using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class MaskLaunchScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private bool canLaunch, forceIncreasing, chargingForce;
    // public float setForce;  static force used for first task
    private float posTimer;  // timer that determines if mask is still for ~1 second
    private Vector3 prevLocation, startLocation;

    [SerializeField] private float forceVal = 0, forceRateChange = 4, maxForce = 5;
    [SerializeField] private GameObject nextPlayer;
    [SerializeField] private Camera cam;
    [SerializeField] private TextMeshProUGUI winMessage;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posTimer = 0;
        canLaunch = true;
        forceIncreasing = true;
        prevLocation = rb.position;
        startLocation = rb.position;
    }

    private void OnEnable()
    {
        canLaunch = true;
        posTimer = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        if (canLaunch)
        {
            // checking if mask can be launched now
            if (Input.GetButtonDown("Jump") && !chargingForce)
            {
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
            if (Input.GetButtonUp("Jump") && chargingForce)
            {
                Debug.Log(forceVal);  // display charged force
                rb.AddForce((Vector3.up + this.transform.forward) * forceVal, ForceMode.Impulse);  // apply current charged force
                canLaunch = false;  // player can't launch until other players have gotten their turns
                posTimer = 1f;  // start movement-tracking timer

                // Reset force values
                forceVal = 0;
                forceIncreasing = true;
                chargingForce = false;
            }
        }
    }

    void FixedUpdate()
    {
        if (prevLocation != rb.position)  // mask is moving
        {
            prevLocation = rb.position;
            posTimer = 1f;  // reset movement-tracking timer
        }
        else
        {
            posTimer -= Time.fixedDeltaTime;  // reduce timer value since mask is not moving
            if (posTimer <= 0)  // mask can be launched again
            {
                if (!canLaunch && !winMessage.isActiveAndEnabled)
                {
                    // activate other player
                    // deactivate camera
                    nextPlayer.GetComponent<MaskLaunchScript>().enabled = true;
                    revertCamera();
                    nextPlayer.GetComponent<MaskLaunchScript>().revertCamera();
                    enabled = false;
                }
            }
        }

        if (rb.position.y < -10)
        {
            rb.position = startLocation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(winMessage.isActiveAndEnabled);
        if (collision.gameObject.CompareTag("Face") && !winMessage.isActiveAndEnabled)
        {
            winMessage.gameObject.SetActive(true);
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

    public void revertCamera()
    {
        cam.enabled = !cam.enabled;
    }
}
