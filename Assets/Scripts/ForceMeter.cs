using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceMeter : MonoBehaviour
{
    [SerializeField] private GameObject player;
    private float maxForce;

    private void Start()
    {
        maxForce = player.GetComponent<MaskLaunchScript>().getMaxForce();
    }

    // LateUpdate is called once per frame after Update
    void LateUpdate()
    {
        // Get current force so that the UI meter reflects current mask launch force
        transform.localScale = new Vector3(1, player.GetComponent<MaskLaunchScript>().getCurrentForce() / maxForce, 1);
    }
}
