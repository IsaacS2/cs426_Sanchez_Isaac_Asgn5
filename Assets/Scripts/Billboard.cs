using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Billboard : MonoBehaviour
{
    private Camera theCam;

    void Start()
    {
        // Assigns the main camera to theCam, adjust if you use specific cameras
        theCam = Camera.main;
    }

void LateUpdate()
{
    Vector3 targetPosition = new Vector3(theCam.transform.position.x, transform.position.y, theCam.transform.position.z);
    transform.LookAt(targetPosition);
}

}
