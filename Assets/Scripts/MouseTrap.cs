using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MouseTrap : MonoBehaviour
{
    [SerializeField] private GameObject trapBase;

    public void DeconstraintX()  // let trap clam on mask
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.constraints = rb.constraints ^ RigidbodyConstraints.FreezePositionX;
    }

    private void OnCollisionStay(Collision collision)
    {
        if (trapBase.GetComponent<MouseBase>().maskIsOn() && collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<MaskLaunchScript>().loseTurn(this.gameObject);
        }
    }
}
