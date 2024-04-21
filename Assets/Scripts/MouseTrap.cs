using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;

public class MouseTrap : MonoBehaviour
{
    private int state;  // 4 states: not activated, activated, about to be destroyed, destroy (this state is more implied since it'll be destroyed)
    private Rigidbody rb;

    private void Start()
    {
        state = 0;  // not activated yet
        rb = GetComponent<Rigidbody>();
    }

    public void ActivateTrap()
    {
        state = 1;
    }

    public void ChangeTrapState(int stateIncrementor = 1)
    {
        if (state >= 1)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;

            state += stateIncrementor;
            if (state >= 4)  // must destroy trap
            {
                Destroy(gameObject);
            }
        }
    }
}
