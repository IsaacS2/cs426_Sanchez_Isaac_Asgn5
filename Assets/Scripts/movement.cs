using System.Collections;
using System.Collections.Generic;

using Unity.VisualScripting;
using UnityEngine;

public class movement : MonoBehaviour
{
    // Start is called before the first frame update
    Transform t;
    public float rotationSpeed= 45;
    public LineRenderer lineRenderer;
    private GameObject AngleFab;
    private MaskLaunchScript maskLaunchScript;
   

    void Start()
    {
        t= GetComponent<Transform>();
        lineRenderer=GetComponent<LineRenderer>(); 
        AngleFab= this.transform.Find("Angle").gameObject;
        maskLaunchScript= GetComponent<MaskLaunchScript>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W)){
            //create the pointer thingy and move it upwards

            // rb.velocity+=this.transform.forward * speed * Time.deltaTime;
        }
            
        else if (Input.GetKey(KeyCode.S)){
             //create the pointer thingy and move it downwards

            // rb.velocity -= this.transform.forward * speed * Time.deltaTime;
        }
           

       if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
             Quaternion rotation= Quaternion.Euler(0, rotationSpeed * Time.deltaTime, 0);
            t.rotation *= rotation;
            //rerender line renderer
            if(lineRenderer){
                lineRenderer.enabled=true;

               
                Vector3 maskvelocity= (AngleFab.transform.forward +  new Vector3(0,1,0)).normalized * Mathf.Min(maskLaunchScript.angle *maskLaunchScript.throwVal, 5);
                ShowTrajectory(AngleFab.transform.position,maskvelocity );
            }
            

            // Vector3[] positions= new Vector3[lineRenderer.positionCount];
            // lineRenderer.GetPositions(positions);
            // for (int i = 0; i < positions.Length;i++){
            //     positions[i]=rotation * positions[i];
            // }
            // lineRenderer.SetPositions(positions);
            // this.transform.forward=new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));

        }
        else if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
           
            Quaternion rotation=  Quaternion.Euler(0, -rotationSpeed* Time.deltaTime, 0);
            t.rotation *= rotation;
            // this.transform.forward=new Vector3(Input.GetAxis("Horizontal"),0,Input.GetAxis("Vertical"));
           if(lineRenderer){
                lineRenderer.enabled=true;

               Debug.Log("Angle : "+maskLaunchScript.angle);
                Vector3 maskvelocity= (AngleFab.transform.forward +  new Vector3(0,1,0)).normalized * Mathf.Min(maskLaunchScript.angle *maskLaunchScript.throwVal, 5);
                ShowTrajectory(AngleFab.transform.position,maskvelocity);
            }

            // Vector3[] positions= new Vector3[lineRenderer.positionCount];
            // lineRenderer.GetPositions(positions);
            // for (int i = 0; i < positions.Length;i++){
            //     positions[i]=rotation* positions[i];
            // }
            // lineRenderer.SetPositions(positions);
        } 
    }
    void ShowTrajectory(Vector3 origin, Vector3 Speed){
        Debug.Log("prevspeed : "+maskLaunchScript.preSpeed);
        Debug.Log("movement script : " + Speed);
        Vector3[] points= new Vector3[100];
        lineRenderer.positionCount= points.Length;
        for(int i= 0; i<points.Length; i++){
            float time= i* 0.1f;
            points[i] = origin + Speed * time + 0.5f * Physics.gravity * time * time;
        }
        lineRenderer.SetPositions(points);
    }
}
