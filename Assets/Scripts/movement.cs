using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    Transform t;
    public float rotationSpeed = 60;

    void Start()
    {
        t = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            //create the pointer thingy and move it upwards

            // rb.velocity+=this.transform.forward * speed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.S))
        {
            //create the pointer thingy and move it downwards

            // rb.velocity -= this.transform.forward * speed * Time.deltaTime;
        }


        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {

            t.rotation *= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
            // this.transform.forward=new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            t.rotation *= Quaternion.Euler(0, -rotationSpeed * Time.deltaTime, 0);
            // this.transform.forward=new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

        }
    }
}