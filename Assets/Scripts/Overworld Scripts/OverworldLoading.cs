using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldLoading : MonoBehaviour
{
    [SerializeField] List<OverworldIcons> icons = new List<OverworldIcons>();

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        int n;
        LevelData data = SaveSystem.LoadPlayer();

        if(data != null)
        {
            n = data.currentLevel;

            if(PlayerPrefs.GetInt("devMode") != 1)
            {
                icons[n].exclamationState(true);
                icons[n].setCollider(true);

                if (n <= 3)
                    MusicScript.GetInstance().PlayOverworldMusicChill();
                if (n <= 6 || n == 10)
                    MusicScript.GetInstance().PlayOverworldMusic();
                if (n <= 9)
                    MusicScript.GetInstance().PlayOverworldMusicRising();
            }
            else
            {
                foreach(var icon in icons)
                {
                    icon.exclamationState(true);
                    icon.setCollider(true);
                }
                MusicScript.GetInstance().PlayOverworldMusicChill();
            }
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
