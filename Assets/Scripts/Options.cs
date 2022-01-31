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
}
