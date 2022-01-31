using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Options : MonoBehaviour
{
    private SceneLoader sceneLoader;
    [SerializeField] private AudioSource soundManager;
    [SerializeField] private Button overworldBtn;
    [SerializeField] private Button titleScreenBtn;
    [SerializeField] private Slider volumeSlider;
    [SerializeField] private Toggle dontRemind;

    // Start is called before the first frame update
    void Start()
    {
        if (soundManager)
        {
            volumeSlider.value = soundManager.volume;
        }
        
        sceneLoader = SceneLoader.GetInstance();
        volumeSlider.onValueChanged.AddListener(delegate { changeVolume(); } );
        overworldBtn.onClick.AddListener(delegate { sceneLoader.returnToOverworld(); } );
        titleScreenBtn.onClick.AddListener(delegate { sceneLoader.loadSceneString("TitleScreen"); } );
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void changeVolume()
    {
        soundManager.volume = volumeSlider.value;
    }
}
