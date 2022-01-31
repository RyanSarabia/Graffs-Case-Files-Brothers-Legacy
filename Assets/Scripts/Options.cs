using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private SceneLoader sceneLoader;
    [SerializeField] private AudioSource SFXManager;
    [SerializeField] private AudioSource musicManager;
    [SerializeField] private Button overworldBtn;
    [SerializeField] private Button titleScreenBtn;
    [SerializeField] private Slider SFXVolumeSlider;
    [SerializeField] private Slider musicVolumeSlider;
    [SerializeField] private Toggle dontRemind;
    [SerializeField] private int level;
    [SerializeField] private GameObject instructPanel;

    // Start is called before the first frame update
    void Start()
    {
        if (SFXManager)
        {
            SFXVolumeSlider.value = SFXManager.volume;
        }
        if (musicManager)
        {
            musicVolumeSlider.value = musicManager.volume;
        }

        sceneLoader = SceneLoader.GetInstance();
        SFXVolumeSlider.onValueChanged.AddListener(delegate { changeVolume(SFXManager, SFXVolumeSlider); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { changeVolume(musicManager, musicVolumeSlider); });
        overworldBtn.onClick.AddListener(delegate { sceneLoader.returnToOverworld(); } );
        titleScreenBtn.onClick.AddListener(delegate { sceneLoader.loadSceneString("TitleScreen"); } );
                       
        if (PlayerPrefs.GetInt("lvl" + level) == 1)
        {
            instructPanel.SetActive(false);
            dontRemind.isOn = true;
        }
        else
            instructPanel.SetActive(true);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void changeVolume(AudioSource source, Slider slider)
    {
        if (source)
        {
            source.volume = slider.value;
        }
    }
    public void remindTick()
    {
        Debug.Log("check mark" + dontRemind.isOn + " lvl" + level);
        if(dontRemind.isOn)
            PlayerPrefs.SetInt("lvl" + level, 1);
        else
            PlayerPrefs.SetInt("lvl" + level, 0);
    }
}
