using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpiderController : MonoBehaviour
{
    // Start is called before the first frame update
    
    public Transform target; // The target object's Transform
    private float speed = 1.5f;  
    private Rigidbody rb;
    
    public bool detected= false;

    void OnEnable(){
        detected=true;
    }
    void Start()
    {
        // Get the Rigidbody component
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate() // Use FixedUpdate for physics-based movement
    {
        // Ensure a target is assigned and we have a Rigidbody
        if (target != null && rb != null && detected)
        {
            // Calculate the direction towards the target
            Vector3 direction = (target.position - transform.position).normalized;

            // Apply force in the desired direction
            rb.AddForce(direction * speed, ForceMode.Force); 
        }
    }
}
