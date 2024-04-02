using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roomba : MonoBehaviour
{
    [SerializeField] private float minTargetDistance = 0.01f;  // minimum distance roomba must be to targets to change course

    private int state;  // int representing the current state of the roomba
    private Vector3 target;  // current position target for roomba
    private Vector3[] patrollingTargets;  // default positions that roomba moves to when idle

    // Start is called before the first frame update
    void Start()
    {
        state = 0;  // 0 == idle state
    }

    // Update is called once per frame
    void Update()
    {
        if (state == 0)  // in idle state
        {

        }

        // in maskAbsorbed state
        else if (state == 1)
        {

        }

        // in returning state
        else if (state == 2) // && Vector3.Distance(transform.position, target) <= minTargetDistance)
        {

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            state = 1;  // 1 == maskAbsorbed state (the mask will be taken to its spawn position)
        }
    }
}
