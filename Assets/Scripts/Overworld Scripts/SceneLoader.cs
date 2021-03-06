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

    public void loadSceneID(int id)
    {
        SceneManager.LoadScene(id);
    }

    public void loadSceneString(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void returnToOverworld()
    {
        SceneManager.LoadScene("OverworldMap");
    }

    public void reloadScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        HintSystem.GetInstance().hintUp();
    }

    public void SavePlayer(int level)
    {
        SaveSystem.SavePlayer(level);
    }

    public void deleteSave()
    {
        SaveSystem.deleteSave();
    }

    public void overWorldDev()
    {
        if (PlayerPrefs.GetInt("devMode") == 1)
            devMode(false);
        else
            devMode(true);
    }

    public void devMode(bool state)
    {
        if(state)
            PlayerPrefs.SetInt("devMode" , 1);
        else
            PlayerPrefs.SetInt("devMode", 0);
    }

    public void quitGame()
    {
        Application.Quit();
    }
}
