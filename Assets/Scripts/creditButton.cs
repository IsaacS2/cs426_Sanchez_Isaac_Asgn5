using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class creditButton : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI winMessage;
    [SerializeField] private Button replayButton;
    [SerializeField] private GameObject viewHolder;
    [SerializeField] private TextMeshProUGUI creditMessage;

    public bool creditMode= false;
    private bool panelSet = false;
    private float previousTime = 0.0f;

    public void showCredit(){
        Debug.Log("in showcredit");
        winMessage.gameObject.SetActive(!winMessage.isActiveAndEnabled);
        creditMessage.gameObject.SetActive(!creditMessage.isActiveAndEnabled);
        replayButton.gameObject.SetActive(!replayButton.isActiveAndEnabled);
        creditMode= creditMessage.isActiveAndEnabled;
    }

    void Update(){
        if(creditMode){
            float currentTime = Time.time; 

        if (currentTime - previousTime >= 1.0f) 
        {
            previousTime = currentTime; 

            float x= creditMessage.rectTransform.localPosition.x;
            float y= creditMessage.rectTransform.localPosition.y;
            float z= creditMessage.rectTransform.localPosition.z;
            creditMessage.rectTransform.localPosition=new Vector3(x,y+30,z);        
            
            if (y>4900.0){
                creditMessage.rectTransform.localPosition=new Vector3(x,-740,z);
            }
            
            }
            
        }
        
    }
}
