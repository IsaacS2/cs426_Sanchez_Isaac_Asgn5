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
    private bool canLaunch, forceIncreasing, chargingForce, sticky, trapContact;
    // public float setForce;  static force used for first task
    private float posTimer;  // timer that determines if mask is still for ~1 second
    private Vector3 prevLocation, startLocation, spawnLocation; 
    private Quaternion startRotation;
    private LineRenderer trajectoryline;
    private GameObject killTrap;
    private GameObject roombaTrap;
    private AudioSource audSource;
    private bool moving=false;

    public AudioSource trampolineSound;
    public AudioSource angleAdjustSound;

    [SerializeField] private float forceVal = 0, forceRateChange = 4, maxForce = 5, defaultTimeVal = 1.5f, temp_forceVal=0;
    [SerializeField] private GameObject nextPlayer;
    [SerializeField] private GameObject camHolder;
    [SerializeField] private Camera cam;
    [SerializeField] private TextMeshProUGUI winMessage;
    [SerializeField] private TextMeshProUGUI statusMessage;
    [SerializeField] private float maxPositionDiff = 0.075f;
    [SerializeField] private ParticleSystem launchParticles; // Assign in the Inspector
    [SerializeField] private AudioClip chargeClip;
    [SerializeField] private GameObject lauchsound;
    [SerializeField] private GameObject dropsound;
    [SerializeField] private GameObject mudtrapsound;

    [SerializeField] private GameObject mousetrapsound;
    [SerializeField] private Button replayButton;
    [SerializeField] private TextMeshProUGUI yourTurnMessage;


    private float rotationSpeed = 5.0f; 

    // trajectory values
    float angle=0;
    private GameObject AngleFab;
    private Vector3 throwDirection= new Vector3(0,1,0);
    private float throwVal= 0;
    int trap_cond= 0;
    private float lastSoundTime = 0f; 
    private const float soundCooldown = 3f;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audSource = GetComponent<AudioSource>();
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

        if (AngleFab != null) {
            transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
            camHolder.transform.eulerAngles = new Vector3(0, transform.eulerAngles.y, 0);
        }

        camHolder.GetComponent<movement>().enabled = true;
        // Show the turn message and set it to hide after 2 seconds
        yourTurnMessage.text = "Your Turn!";
        yourTurnMessage.gameObject.SetActive(true);
        Invoke(nameof(HideTurnMessage), 2f); // Using Invoke to delay the call to HideTurnMessage
    }

    private void HideTurnMessage()
    {
        yourTurnMessage.gameObject.SetActive(false);
    }


    // Update is called once per frame
    private void Update()
    {
        if (Vector3.Distance(prevLocation, rb.position) < maxPositionDiff){//mask not moving?
            if (moving && Time.time - lastSoundTime >= soundCooldown) { // Check the cooldown
                dropsound.GetComponent<AudioSource>().Play();
                moving = false;
                lastSoundTime = Time.time; // Update the time the sound was last played 
            }
        }
        if (canLaunch)
        {
            // checking if mask can be launched now
            if (Input.GetButtonDown("Jump") && !chargingForce && roombaTrap == null)
            {
                chargingForce = true;  // force will now begin being charged
                audSource.Play();
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
                    audSource.pitch = -1;
                }
                if (forceVal <= 0)
                {
                    forceIncreasing = true;
                    audSource.pitch = 1;
                    audSource.Play();
                }
                
            }

            // force charging button (space) has been released; time to launch mask!
            if (Input.GetButtonUp("Jump") && chargingForce)
            {   
                lauchsound.GetComponent<AudioSource>().Play();
                moving=true;
                lastSoundTime= Time.time;
                rb.AddForce((Vector3.up + AngleFab.transform.forward) * forceVal, ForceMode.Impulse);  // apply current charged force
                canLaunch = false;  // player can't launch until other players have gotten their turns
                posTimer = defaultTimeVal;  // start movement-tracking timer
                AngleFab.transform.localEulerAngles= new Vector3(0, 0, 0);
                temp_forceVal= forceVal;

                // Reset force values
                forceVal = 0;
                throwVal=0;
                forceIncreasing = true;
                chargingForce = false;
                trajectoryline.enabled= false;

                gameObject.GetComponent<movement>().enabled = false;
                camHolder.GetComponent<movement>().enabled = false;
                PlayLaunchParticles();
                audSource.Stop();
            }

            if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow) )
            {
                if (!angleAdjustSound.isPlaying)
                {
                    angleAdjustSound.Play();
                }

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

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                if (!angleAdjustSound.isPlaying)
                {
                    angleAdjustSound.Play();
                }

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
        if (roombaTrap != null)  // player inside roomba
        {
            rb.position = roombaTrap.transform.position;
        }

        if (Vector3.Distance(prevLocation, rb.position) > maxPositionDiff)  // mask is moving
        {
            prevLocation = rb.position;
            posTimer = defaultTimeVal;  // reset movement-tracking timer
            
        }
        else
        {
            
            posTimer -= Time.fixedDeltaTime;  // reduce timer value since mask is not moving
            if (posTimer <= 0)  // mask can be launched again
            {
                StopLaunchParticles();
                
                
                if (!canLaunch && !winMessage.isActiveAndEnabled)
                {
                    statusMessage.gameObject.SetActive(false);
                    // activate other player
                    // deactivate camera
                    if (nextPlayer.GetComponent<MaskLaunchScript>().trap_cond==0){
                        nextPlayer.GetComponent<MaskLaunchScript>().enabled = true;
                        revertCamera();
                        nextPlayer.GetComponent<MaskLaunchScript>().revertCamera();
                        enabled = false;
                    }
                    else{
                        nextPlayer.GetComponent<MaskLaunchScript>().trap_cond=0;
                        enabled=true;
                        // statusMessage.gameObject.SetActive(true);
                        // statusMessage.text = "Xtra Turn!";

                        OnEnable();

                    }
                    
                    if (trapContact)
                    {
                        trapContact = false;
                        
                    }
                    else if (killTrap != null) {
                        Debug.Log("Trap Destroyed");
                        Destroy(killTrap);
                    }
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
        if (collision.gameObject.CompareTag("Trap"))
        {
            if (killTrap == null)
            {
                killTrap = collision.gameObject.GetComponent<MouseDetector>().trapKiller;
                trapContact = true;
                mousetrapsound.GetComponent<AudioSource>().Play();
                statusMessage.gameObject.SetActive(true);
                statusMessage.text = "Oops, activated trap!";
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    { 
        if (other.gameObject.CompareTag("Face") && !winMessage.isActiveAndEnabled)
        {
            winMessage.gameObject.SetActive(true); 
            replayButton.gameObject.SetActive(true);
            winMessage.color = Color.yellow;
        }
        else if (other.gameObject.CompareTag("Trap2") && trap_cond==0){
            mudtrapsound.GetComponent<AudioSource>().Play();
            moving = false;
            statusMessage.gameObject.SetActive(true);
            statusMessage.text = "Oops, trap! Lose a turn";
            trap_cond=1;
            rb.velocity = Vector3.zero;
        }
        else if (other.gameObject.CompareTag("trampoline"))
        {
           
             // Play the trampoline sound effect
            if (trampolineSound != null)
            {
                trampolineSound.Play();
            }

            // Add the bounce force to the object's Rigidbody
            
            if (rb != null)
            {
                rb.AddForce(((Vector3.up * 2) + AngleFab.transform.forward) * temp_forceVal, ForceMode.Impulse);
                Debug.Log("WTF trampoline");
            }
        }

        else if (other.gameObject.CompareTag("spider")){
            rb.position = startLocation;
            other.gameObject.GetComponent<SpiderController>().detected= false;
            other.gameObject.GetComponent<SpiderController>().enabled= false;
            other.gameObject.GetComponent<SpiderController>().anim.SetTrigger("stop running");
        }
    }

     // Call this method to play the launch particles
    private void PlayLaunchParticles()
    {
        if(launchParticles != null)
            launchParticles.Play();
    }

    // Call this method to stop the launch particles
    private void StopLaunchParticles()
    {
        if(launchParticles != null)
            launchParticles.Stop();
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

    public Vector3 RoombaTriggered(GameObject roomba)
    {
        Debug.Log("Got sucked!");
        GetComponent<Renderer>().enabled = false;
        GetComponent<Rigidbody>().isKinematic = true;
        roombaTrap = roomba;
        return spawnLocation;
    }

    public void RoombaReturning()
    {
        GetComponent<Renderer>().enabled = true;
        GetComponent<Rigidbody>().isKinematic = false;
        roombaTrap = null;
        rb.position = spawnLocation;
        statusMessage.text= "";
        statusMessage.gameObject.SetActive(false);
        Debug.Log("Return roomba");
    }
}
