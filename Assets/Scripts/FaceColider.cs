using UnityEngine;

public class FaceCollider : MonoBehaviour
{
    public AudioSource soundEffect; // Reference to the AudioSource component for the sound effect

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            // Send message to trigger terrified animation
            transform.parent.SendMessage("TriggerTerrifiedAnimation");

            // Play the sound effect
            if (soundEffect != null)
            {
                soundEffect.Play();
            }
        }
    }
}
