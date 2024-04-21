using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskCollisionScript : MonoBehaviour
{
    private bool killerTouching, trapBaseTouching;

    private void OnCollisionEnter(Collision collision)
    {
        // player is on the base of a mouse trap
        if (collision.gameObject.CompareTag("TrapBase"))
        {
            trapBaseTouching = true;
        }

        // player is in contact with the clamp/killer of the mouse trap
        if (collision.gameObject.CompareTag("KillerTrap"))
        {
            killerTouching = true;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        // player is off the base of a mouse trap
        if (collision.gameObject.CompareTag("TrapBase"))
        {
            trapBaseTouching = false;
        }

        // player is not in contact with the clamp of the mouse trap
        if (collision.gameObject.CompareTag("KillerTrap"))
        {
            killerTouching = false;
        }
    }

    public bool GetTrapBaseCollision()
    {
        return trapBaseTouching;
    }

    public bool GetTrapKillerCollision()
    {
        return killerTouching;
    }
}
