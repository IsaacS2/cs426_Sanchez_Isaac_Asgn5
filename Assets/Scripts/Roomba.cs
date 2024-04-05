using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Roomba : MonoBehaviour
{
    [SerializeField] private float minTargetDistance = 0.01f;  // minimum distance roomba must be to targets to change course
    [SerializeField] private NavMeshAgent agent;
    [SerializeField] private Transform[] patrollingTargets;  // default positions that roomba moves to when idle

    private int state, patrollingInt;  // int representing the current state of the roomba
    private Vector3 target;  // current position target for roomba
    private Vector3[] patrollingVectors;
    private GameObject capturedMask;
    
    // Start is called before the first frame update
    void Start()
    {
        state = 0;  // 0 == idle state
        patrollingInt = 0;  // target is first vector in patrolling positions list
        agent.updateRotation = false;

        patrollingVectors = new Vector3[patrollingTargets.Length];

        // getting position vectors from transform parameters
        for (int i = 0; i < patrollingTargets.Length; i++)
        {
            patrollingVectors[i] = patrollingTargets[i].position;
        }

        target = patrollingVectors[patrollingInt];
        agent.SetDestination(target);
    }

    // Update is called once per frame
    void Update()
    {
        /*if (patrollingVectors.Length <= 0)
        {
            for (int i = 0; i < patrollingTargets.Length; i++)
            {
                patrollingVectors[i] = patrollingTargets[i].position;
            }
        }*/

        //Debug.Log("Distance: " + Vector3.Distance(target, transform.position) 
                    //+ ", target: " + target + ", position: " + transform.position);
        if (Vector3.Distance(transform.position, target) <= minTargetDistance) // time to switch patrolling targets
        {
            if (state == 0 || state == 2)  // in idle state
            {
                if (state == 0)
                {
                    Debug.Log("switch state 0");
                }
                patrollingInt++;
                if (patrollingInt >= patrollingVectors.Length)
                {
                    patrollingInt = 0;
                }

                target = patrollingVectors[patrollingInt];

                if (state == 2)  // in returning state
                {
                    Debug.Log("switch state 2");
                    state = 0;
                    capturedMask = null;
                    GetComponent<BoxCollider>().enabled = true;
                }
            }

            else if (state == 1)  // in maskAbsorbed state
            {
                Debug.Log("switch state 1");
                capturedMask.GetComponent<MaskLaunchScript>().RoombaReturning();
                GetComponent<BoxCollider>().enabled = false;
                state = 2;
                var currentShortestDistance = 10000f;
                for (int i = 0; i < patrollingVectors.Length; i++)
                {
                    // getting closest patrolling target to return to idle state
                    if (Vector3.Distance(transform.position, patrollingVectors[i]) <= currentShortestDistance)
                    {
                        currentShortestDistance = Vector3.Distance(transform.position, patrollingVectors[i]);
                        patrollingInt = i;
                    }
                }
                target = patrollingVectors[patrollingInt];
            }

            Debug.Log("Target changed!");
            agent.SetDestination(target);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && capturedMask == null && state == 0)
        {
            if (collision.gameObject.GetComponent<MaskLaunchScript>() != null) {
                Debug.Log("Mask touched!");
                state = 1;  // 1 == maskAbsorbed state (the mask will be taken to its spawn position)
                var maskVector = collision.gameObject.GetComponent<MaskLaunchScript>().RoombaTriggered(gameObject);
                target = maskVector;
                capturedMask = collision.gameObject;
                Debug.Log("Target changed!");

                agent.SetDestination(target);
            }
        }
    }
}
