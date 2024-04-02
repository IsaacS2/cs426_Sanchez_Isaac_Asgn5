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
    private Transform target;  // current position target for roomba
    private GameObject capturedMask;
    private Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        state = 0;  // 0 == idle state
        patrollingInt = 0;  // target is first vector in patrolling positions list
        target = patrollingTargets[patrollingInt];
        agent.SetDestination(target.position);
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, target.position) <= minTargetDistance) // time to switch patrolling targets
        {
            if (state == 0 || state == 2)  // in idle state
            {
                patrollingInt++;
                if (patrollingInt >= patrollingTargets.Length)
                {
                    patrollingInt = 0;
                }

                target = patrollingTargets[patrollingInt];
            }

            else if (state == 1)  // in maskAbsorbed state
            {
                capturedMask.GetComponent<MaskLaunchScript>().RoombaReturning();
                state = 2;
                var currentShortestDistance = 10000f;
                for (int i = 0; i < patrollingTargets.Length; i++)
                {
                    // getting closest patrolling target to return to idle state
                    if (Vector3.Distance(transform.position, patrollingTargets[i].position) <= currentShortestDistance)
                    {
                        currentShortestDistance = Vector3.Distance(transform.position, patrollingTargets[i].position);
                        patrollingInt = i;
                    }
                }
                target = patrollingTargets[patrollingInt];
            }
            
            if (state == 2)  // in returning state
            {
                state = 0;
            }

            agent.SetDestination(target.position);
            Debug.Log("Target Changed!");
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Roomba touched something!");
        if (collision.gameObject.CompareTag("Player") && capturedMask == null)
        {
            Debug.Log("Mask touched!");
            state = 1;  // 1 == maskAbsorbed state (the mask will be taken to its spawn position)
            target.position = collision.gameObject.GetComponent<MaskLaunchScript>().RoombaTriggered(gameObject);
            capturedMask = collision.gameObject;
        }
    }
}
