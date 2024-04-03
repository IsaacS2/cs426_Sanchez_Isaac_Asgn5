using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator anim;
    
    public Transform target; // The target object's Transform
    [SerializeField] private float speed = 0.7f;  
    private Rigidbody rb;
    
    public bool detected= false;

    void OnEnable(){
         detected=true;
    }
    
    void Start()
    {
        // Get the Rigidbody component
        anim=GetComponent<Animator> ();
        rb = GetComponent<Rigidbody>();
        
    }
    void Update(){
        if (target==null){
            detected= false;
        }
    }
    
// https://youtu.be/U0dlWhB_e0E?si=thr9UFNEQvmZdpYW
// https://www.youtube.com/watch?v=hlO0XlqZFBo&ab_channel=Imphenzia
    void FixedUpdate() // Use FixedUpdate for physics-based movement
    {
        // Ensure a target is assigned and we have a Rigidbody
        if (target != null && rb != null && detected==true)
        {
            // Calculate the direction towards the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Apply force in the desired direction
            rb.AddForce(direction * speed, ForceMode.Force); 

            // Update Animator with movement information
            float velocityMagnitude = rb.velocity.magnitude; 
            anim.SetFloat("speed", velocityMagnitude); 

            // Optionally: Set facing direction based on velocity
            if (rb.velocity != Vector3.zero) {
                transform.forward = rb.velocity;
            }

            anim.SetTrigger("run");
        }
    }
}
