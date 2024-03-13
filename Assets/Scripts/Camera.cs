using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform playerTransform; // Reference to the player's Transform
    private Vector3 offset; // Offset from the player
    private float distanceToPlayer = 3f; // Adjust this value to set how far back the camera should be
    private float heightAbovePlayer = 2f; // Adjust this value to set the height of the camera above the player
    private float rightOffset = 3f; // Distance to the right of the player to position the camera

    void Start()
    {
        // Calculate initial offset based on the player's position
        if (playerTransform != null)
        {
            // Adjust offset to not only be behind and above, but also to the right of the player
            offset = new Vector3(rightOffset, heightAbovePlayer, -distanceToPlayer);

            // Since the camera is initially a child of the player, let's detach it to make its movement independent
            transform.parent = null;

            // Adjust the camera's initial position relative to the player
            transform.position = playerTransform.position + offset;

            // Point the camera at the player
            transform.LookAt(playerTransform.position);
        }
    }

    void LateUpdate()
    {
        if (playerTransform != null)
        {
            // Update the camera's position to stay in a fixed position relative to the player
            transform.position = playerTransform.position + offset;

            // Keep the camera's rotation constant, always looking at the player
            transform.LookAt(playerTransform.position);
        }
    }
}
