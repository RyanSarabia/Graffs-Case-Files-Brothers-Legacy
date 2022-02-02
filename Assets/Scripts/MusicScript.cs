using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicScript : MonoBehaviour
{
    // Start is called before the first frame update
    private static MusicScript instance;
    [SerializeField] private AudioSource audioSource;


    [SerializeField] private AudioClip ch1_1Music;
    [SerializeField] private AudioClip ch1_2Music;
    [SerializeField] private AudioClip ch1_3Music;
    [SerializeField] private AudioClip ch2_1Music;
    [SerializeField] private AudioClip ch2_2Music;
    [SerializeField] private AudioClip ch2_3Music;
    [SerializeField] private AudioClip ch3_1Music;
    [SerializeField] private AudioClip ch3_2Music;
    [SerializeField] private AudioClip ch3_3Music;
    [SerializeField] private AudioClip overworldMusic;
    [SerializeField] private AudioClip overworldMusicChill;

    [SerializeField] private AudioClip chillMusic1;
    [SerializeField] private AudioClip chillMusic2;
    [SerializeField] private AudioClip chillMusic3;
    [SerializeField] private AudioClip titleMusic;
    [SerializeField] private AudioClip actionMusic1;
    [SerializeField] private AudioClip actionMusic2;
    [SerializeField] private AudioClip actionMusic3;

    public static MusicScript GetInstance()
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

    public void PlayCh1_1Music()
    {
        StopMusic();
        audioSource.clip = ch1_1Music;
        audioSource.Play();
    }
    public void PlayCh2_1Music()
    {
        StopMusic();
        audioSource.clip = ch2_1Music;
        audioSource.Play();
    }
    public void PlayCh3_1Music()
    {
        StopMusic();
        audioSource.clip = ch3_1Music;
        audioSource.Play();
    }
    public void PlayCh1_2Music()
    {
        StopMusic();
        audioSource.clip = ch1_2Music;
        audioSource.Play();
    }
    public void PlayCh2_2Music()
    {
        StopMusic();
        audioSource.clip = ch2_2Music;
        audioSource.Play();
    }
    public void PlayCh3_2Music()
    {
        StopMusic();
        audioSource.clip = ch3_2Music;
        audioSource.Play();
    }
    public void PlayCh1_3Music()
    {
        StopMusic();
        audioSource.clip = ch1_3Music;
        audioSource.Play();
    }
    public void PlayCh2_3Music()
    {
        StopMusic();
        audioSource.clip = ch2_3Music;
        audioSource.Play();
    }
    public void PlayCh3_3Music()
    {
        StopMusic();
        audioSource.clip = ch3_3Music;
        audioSource.Play();
    }

    public void PlayOverworldMusic()
    {
        StopMusic();
        audioSource.clip = overworldMusic;
        audioSource.Play();
    }
    public void PlayOverworldMusicChill()
    {
        StopMusic();
        audioSource.clip = overworldMusicChill;
        audioSource.Play();
    }

    public void PlayChill1()
    {
        StopMusic();
        audioSource.clip = chillMusic1;
        audioSource.Play();
    }
    public void PlayChill2()
    {
        StopMusic();
        audioSource.clip = chillMusic2;
        audioSource.Play();
    }
    public void PlayChill3()
    {
        StopMusic();
        audioSource.clip = chillMusic3;
        audioSource.Play();
    }

    public void PlayTitle()
    {
        StopMusic();
        audioSource.clip = titleMusic;
        audioSource.Play();
    }


    public void StopMusic()
    {
        audioSource.Stop();
    }

}
