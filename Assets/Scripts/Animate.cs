using UnityEngine;

public class Human : MonoBehaviour
{
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerTerrifiedAnimation()
    {
        animator.SetTrigger("BecomeTerrified"); 
    }
}
