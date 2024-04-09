using UnityEngine;

public class FaceCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            transform.parent.SendMessage("TriggerTerrifiedAnimation");
        }
    }
}
