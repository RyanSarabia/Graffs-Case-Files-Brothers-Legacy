using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private static SceneLoader instance;
    public static SceneLoader GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            GameObject.Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void loadScene(int id)
    {
        switch (id)
        {
            case 1: SceneManager.LoadScene(1); break;
            case 2: SceneManager.LoadScene(2); break;
            case 3: SceneManager.LoadScene(3); break;           
        }
    }

    public void returnToOverworld()
    {
        SceneManager.LoadScene("OverworldMap");
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
