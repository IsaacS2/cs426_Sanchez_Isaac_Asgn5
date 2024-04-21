using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Human : MonoBehaviour
{
    private Animator animator;
    public  GameObject spider;

    public GameObject obj1;
    public GameObject obj2;
    [SerializeField] private TextMeshProUGUI gremlinMessage;
    
    [SerializeField] private TextMeshProUGUI statusMessage;
    private void Start()
    {
        
        animator = GetComponent<Animator>();
        
    }

    public void TriggerTerrifiedAnimation()
    {
        animator.SetTrigger("BecomeTerrified"); 
    }
    public void OnTriggerEnter(Collider col){
        //  actiivate spider script if mask touches trigger below human
        if (col.gameObject.CompareTag("Player")){
            if (col.gameObject.GetComponent<MaskLaunchScript>() != null) {  // safety check for player script
                if (!col.gameObject.GetComponent<MaskLaunchScript>().LaunchStatus()) {  // mask has been launched
                    statusMessage.gameObject.SetActive(true);
                    statusMessage.text = "Gremlin is Active!!!";
                    statusMessage.color = Color.red;
                    if (obj1 == null) {
                        obj1 = col.GameObject();
                    }
                    else if (obj2 == null) {
                        obj2 = col.GameObject();
                    }
                    col.gameObject.GetComponent<MaskLaunchScript>().nearface = true;

                    spider.GetComponent<SpiderController>().target = col.gameObject.transform;
                    spider.GetComponent<SpiderController>().detected = true;
                }
            }
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
            other.gameObject.GetComponent<MaskLaunchScript>().exitface= true;
            
            if (obj1 ==null && obj2==null){
            Debug.Log("No more players");
            other.gameObject.GetComponent<MaskLaunchScript>().nearface= false;
            spider.GetComponent<SpiderController>().enabled= false;
            spider.GetComponent<SpiderController>().detected= false;
            spider.GetComponent<SpiderController>().anim.SetTrigger("stop running");
         }
         }
         
    }
}
