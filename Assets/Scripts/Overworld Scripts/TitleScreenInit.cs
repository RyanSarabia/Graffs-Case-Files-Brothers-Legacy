using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TitleScreenInit : MonoBehaviour
{
    [SerializeField] private Button cont;
    private void Awake()
    {
        if (SaveSystem.checkSave())
        {
            cont.interactable = true;
        }
        else
        {
            cont.interactable = false;
            PlayerPrefs.SetFloat("music", 0.2f);
            PlayerPrefs.SetFloat("sfx", 0.2f);
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        MusicScript.GetInstance().PlayTitle();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
