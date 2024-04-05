using Unity.VisualScripting;
using UnityEngine;

public class Human : MonoBehaviour
{
    private Animator animator;
    public  GameObject spider;

    public GameObject obj1;
    public GameObject obj2;

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
            
            if(obj1 == null){
                obj1= col.GameObject();
            }
            else if (obj2== null){
                obj2= col.GameObject();
            }
            Debug.Log("Face is near Human");
            spider.GetComponent<SpiderController>().enabled= true;
            spider.GetComponent<SpiderController>().target= col.gameObject.transform; 
            spider.GetComponent<SpiderController>().detected= true;
            
            

        }
    }
    void OnTriggerExit(Collider other)
    {
         if (other.gameObject.CompareTag("Player")){
            Debug.Log(obj1);
            Debug.Log(obj2);
            Debug.Log("Player exitted");
            if (obj1 == other.GameObject()){
                obj1=null;
                if (obj2 != null){
                     spider.GetComponent<SpiderController>().target= obj2.transform; 

                }

            }
            else if (obj2 == other.GameObject()){
                obj2=null;
                if (obj1 != null){
                     spider.GetComponent<SpiderController>().target= obj1.transform; 

                }
            }
        
         }
         if (obj1 ==null && obj2==null){
            Debug.Log("No more players");
            spider.GetComponent<SpiderController>().enabled= false;
            spider.GetComponent<SpiderController>().detected= false;
            spider.GetComponent<SpiderController>().anim.SetTrigger("stop running");
         }
    }
}
