using UnityEngine;

public class FaceCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.name == "P1Mask" || other.gameObject.name == "P2Mask")
        {
            transform.parent.SendMessage("TriggerTerrifiedAnimation");
        }
    }
}
