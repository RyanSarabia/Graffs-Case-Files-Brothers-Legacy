using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFXScript : MonoBehaviour
{
    // Start is called before the first frame update
    private static SFXScript instance;
    [SerializeField] private AudioSource audioSource;


    [SerializeField] private AudioClip createWallSound;
    [SerializeField] private AudioClip clueAcquiredSound;
    [SerializeField] private AudioClip energyAcquiredSound;
    [SerializeField] private AudioClip victorySound;
    [SerializeField] private AudioClip defeatSound;
    [SerializeField] private AudioClip confirmLightingSound;


    [SerializeField] private AudioClip clickArch3NodeSound;

    [SerializeField] private AudioClip resetAnySound;
    public static SFXScript GetInstance()
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

    public void CreateWallSFX()
    {
        audioSource.PlayOneShot(createWallSound);
    }

    public void ClueAcquiredSFX()
    {
        audioSource.PlayOneShot(clueAcquiredSound);
    }

    public void EnergyAcquiredSFX()
    {
        audioSource.PlayOneShot(energyAcquiredSound);
    }

    public void PlayResetAnySFX()
    {
        audioSource.clip = resetAnySound;
        audioSource.Play();
        
    }

    public void VictorySFX()
    {
        audioSource.PlayOneShot(victorySound);
    }

    public void DefeatSFX()
    {
        audioSource.PlayOneShot(defeatSound);
    }
    public void ConfirmLightingSFX()
    {
        audioSource.PlayOneShot(confirmLightingSound);
    }


    public void ClickNodeArch3SFX()
    {
        audioSource.PlayOneShot(clickArch3NodeSound);
    }

}
