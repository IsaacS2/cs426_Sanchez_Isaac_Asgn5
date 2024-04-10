using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MouseTrap : MonoBehaviour
{
    private bool setDestroy;

    private void Start()
    {
        setDestroy = false;
    }

    public void DeconstraintX()  // let trap clam on mask
    {
        Debug.Log("Deconstraint!");
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = rb.constraints ^ RigidbodyConstraints.FreezeRotationX;
    }

    public bool GetDestroyStatus()
    {
        return setDestroy;
    }

    public void SetDestroyStatus()
    {
        setDestroy = true;
    }
}
