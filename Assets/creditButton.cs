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

    public void showCredit(){
        Debug.Log("in showcredit");
        winMessage.gameObject.SetActive(!winMessage.isActiveAndEnabled);
        viewHolder.gameObject.SetActive(!viewHolder.active);
        replayButton.gameObject.SetActive(!replayButton.isActiveAndEnabled);

    }
}
