using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button startBtn;

    private void Awake()
    {
        startBtn.onClick.AddListener(() =>
        {
            SceneManager.LoadScene(1);
        });
    }
}
