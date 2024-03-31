using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Unity.VisualScripting;

public class MaskLaunchScript : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody rb;
    private bool canLaunch, forceIncreasing, chargingForce, turnLost, sticky, trapContact;
    // public float setForce;  static force used for first task
    private float posTimer;  // timer that determines if mask is still for ~1 second
    private Vector3 prevLocation, startLocation, spawnLocation; 
    private Quaternion startRotation;
    private LineRenderer trajectoryline;
    private GameObject killTrap;

    [SerializeField] private float forceVal = 0, forceRateChange = 4, maxForce = 5;
    [SerializeField] private GameObject nextPlayer;
    [SerializeField] private GameObject camHolder;
    [SerializeField] private Camera cam;
    [SerializeField] private TextMeshProUGUI winMessage;
    [SerializeField] private TextMeshProUGUI statusMessage;
    [SerializeField] private float maxPositionDiff = 0.01f;
    private float rotationSpeed = 5.0f; 

    // trajectory values
    float angle=0;
    private GameObject AngleFab;
    private Vector3 throwDirection= new Vector3(0,1,0);
    private float throwVal= 0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        posTimer = 0;
        canLaunch = true;
        forceIncreasing = true;
        prevLocation = rb.position;
        startLocation = rb.position;
        spawnLocation = rb.position;
        
        // angle line values
        trajectoryline= GetComponent<LineRenderer>();
        trajectoryline.SetPosition( 0,rb.position);
        trajectoryline.enabled= false;
        AngleFab= this.transform.Find("Angle").gameObject;
    }

    private void OnEnable()
    {
        canLaunch = true;
        posTimer = 0;
        gameObject.GetComponent<movement>().enabled = true;
        if (AngleFab != null) {  // return camHolder view to behind the launch trajectory of mask
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            camHolder.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }
        camHolder.GetComponent<movement>().enabled = true;

        if (turnLost)
        {
            posTimer = 1;
        }
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
                rb.AddForce((Vector3.up + AngleFab.transform.forward) * forceVal, ForceMode.Impulse);  // apply current charged force
                canLaunch = false;  // player can't launch until other players have gotten their turns
                posTimer = 1f;  // start movement-tracking timer
                AngleFab.transform.localEulerAngles= new Vector3(0, 0, 0);

                // Reset force values
                forceVal = 0;
                throwVal=0;
                forceIncreasing = true;
                chargingForce = false;
                trajectoryline.enabled= false;
                gameObject.GetComponent<movement>().enabled = false;
                camHolder.GetComponent<movement>().enabled = false;
            }            

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                trajectoryline.enabled= true;
                angle+=Time.deltaTime;
                throwVal += Time.deltaTime * forceRateChange;
                
                if (AngleFab.transform.localEulerAngles.x == 0 || AngleFab.transform.localEulerAngles.x >=285.0 ) 
                { 
                    float rotationAmount = Mathf.Min(0.25f, Time.deltaTime * rotationSpeed);
                    AngleFab.transform.Rotate(-rotationAmount, 0, 0, Space.Self);
                    Vector3 maskvelocity= (AngleFab.transform.forward +  throwDirection).normalized * Mathf.Min(angle * throwVal, maxForce);
                    ShowTrajectory(AngleFab.transform.position,maskvelocity);
                }

                // if (AngleFab.transform.rotation.eulerAngles.x>=-90.0f){
                //     AngleFab.transform.forward=new Vector3(AngleFab.transform.forward.x, AngleFab.transform.forward.y+0.1f, AngleFab.transform.forward.z);
                //     Debug.Log(AngleFab.transform.rotation.eulerAngles);
                // }
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow)){
                trajectoryline.enabled= true;
                angle-=Time.deltaTime;
                throwVal -= Time.deltaTime * forceRateChange;
                if (AngleFab.transform.localEulerAngles.x == 285.0 || AngleFab.transform.localEulerAngles.x <359.0 ) { 
                    float rotationAmount = Mathf.Min(0.25f, Time.deltaTime * rotationSpeed);
                    AngleFab.transform.Rotate(rotationAmount, 0, 0, Space.Self);
                    Vector3 maskvelocity= (AngleFab.transform.forward +  throwDirection).normalized * Mathf.Min(angle * throwVal, maxForce);
                    ShowTrajectory(rb.position,maskvelocity);
                }
            }
        }
    }

    void FixedUpdate()
    {
        if (Vector3.Distance(prevLocation, rb.position) > maxPositionDiff)  // mask is moving
        {
            prevLocation = rb.position;
            posTimer = 1f;  // reset movement-tracking timer
        }
        else
        {
            posTimer -= Time.fixedDeltaTime;  // reduce timer value since mask is not moving
            if (posTimer <= 0)  // mask can be launched again
            {
                Debug.Log("0");
                if ((!canLaunch || turnLost) && !winMessage.isActiveAndEnabled && !trapContact)
                {
                    Debug.Log("1");
                    if (turnLost)
                    {
                        Debug.Log("2");
                        if (!trapContact)
                        {
                            turnLost = false;
                            statusMessage.gameObject.SetActive(false);
                            Destroy(killTrap);
                            rb.constraints = ~RigidbodyConstraints.FreezeAll;
                        }
                        else {
                            trapContact = false;
                            Debug.Log("3");
                        }
                    }
                    Debug.Log("4");
                    // activate other player and deactivate camera
                    nextPlayer.GetComponent<MaskLaunchScript>().enabled = true;
                    revertCamera();  // this player's camera deactivated first to switch to other camera
                    nextPlayer.GetComponent<MaskLaunchScript>().revertCamera();
                    enabled = false;
                }
            }
        }

        if (rb.position.y < -10)  // player off map
        {
            rb.position = startLocation;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Face") && !winMessage.isActiveAndEnabled)
        {
            winMessage.gameObject.SetActive(true);
        }

        if (collision.gameObject.CompareTag("Trap") && !turnLost)
        {
            Debug.Log("Trap obtained");
            //posTimer = 3f;  // provide more time to see if player settled in trap
            trapContact = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Trap") && !turnLost)
        {
            trapContact = false;
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

    void ShowTrajectory(Vector3 origin, Vector3 Speed){
        Vector3[] points= new Vector3[100];
        trajectoryline.positionCount= points.Length;
        for(int i= 0; i<points.Length; i++){
            float time= i* 0.1f;
            points[i] = origin + Speed * time + 0.5f * Physics.gravity * time * time;
        }
        trajectoryline.SetPositions(points);
    }

    public void loseTurn(GameObject killer)
    {
        turnLost = true;
        statusMessage.gameObject.SetActive(true);
        statusMessage.text = "Trapped! Lost 1 turn!";
        killTrap = killer;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }
}
