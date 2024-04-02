using UnityEngine;

public class Human : MonoBehaviour
{
    private Animator animator;
    public  GameObject spider;

    private void Start()
    {
        animator = GetComponent<Animator>();
    }

    public void TriggerTerrifiedAnimation()
    {
        animator.SetTrigger("BecomeTerrified"); 
    }
    public void OnTriggerEnter(Collider col){
        //actiivate spider script if 
        Debug.Log("Something is close to the face");

        if (col.gameObject.CompareTag("Player")){
            Debug.Log("Face is near Human");
            spider.GetComponent<SpiderController>().target= col.gameObject.transform; 
            spider.GetComponent<SpiderController>().enabled= true;
            

        }
    }
}
