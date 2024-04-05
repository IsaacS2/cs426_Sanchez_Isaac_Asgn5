using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MouseTrap : MonoBehaviour
{
    public void DeconstraintX()  // let trap clam on mask
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = rb.constraints ^ RigidbodyConstraints.FreezeRotationX;
    }
}
