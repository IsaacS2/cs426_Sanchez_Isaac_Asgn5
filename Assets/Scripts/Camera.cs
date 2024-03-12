using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public MaskLaunchScript playerScript; // Reference to the MaskLaunchScript
    public Vector3 offset; // Offset from the player
    private Quaternion initialRotation; // To store the initial rotation of the camera

    void Start()
    {
        // Store the initial rotation of the camera
        initialRotation = transform.rotation;
    }

    void LateUpdate()
    {
        if (playerScript != null)
        {
            // Make sure there's a Rigidbody component to follow
            Rigidbody playerRb = playerScript.GetComponent<Rigidbody>();
            if (playerRb != null)
            {
                // Set the camera position to follow the player's Rigidbody position plus the offset
                transform.position = playerRb.position + offset;

                // Keep the camera's rotation constant regardless of the player's rotation
                transform.rotation = initialRotation;
            }
        }
    }
}
