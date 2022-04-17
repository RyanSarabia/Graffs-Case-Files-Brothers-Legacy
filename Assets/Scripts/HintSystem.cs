using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Events;

public class HintSystem : MonoBehaviour
{
    private static HintSystem instance;
    public static HintSystem GetInstance()
    {
        return instance;
    }

    [SerializeField] private int hintLvl;
    private string hintString;    

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            GameObject.Destroy(gameObject);

        hintString = SceneManager.GetActiveScene().name + "hints";

        hintLvl = PlayerPrefs.GetInt(hintString);
    }    

    // Start is called before the first frame update
    void Start()
    {       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void hintUp()
    {
        PlayerPrefs.SetInt(hintString, PlayerPrefs.GetInt(hintString) + 1);
    }

    public int getHintLvl()
    {
        return hintLvl;
    }    
    public void resetHintLvl()
    {
        PlayerPrefs.SetInt(hintString, 0);
    }
}
