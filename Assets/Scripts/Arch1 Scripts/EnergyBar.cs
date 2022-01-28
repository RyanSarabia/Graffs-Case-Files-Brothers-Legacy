using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBar : MonoBehaviour
{
    public static readonly string ENERGY_INT = "ENERGY_INT";

    [SerializeField] private Slider slider;

    private void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.ARCH1_ENERGY_ADDED, this.SetEnergy);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.ARCH1_ENERGY_ADDED);
    }

    public void SetMaxEnergy(int energy)
    {
        slider.maxValue = energy;
        slider.value = energy;
    }
    public void SetEnergy(int energy)
    {
        slider.value = energy;
    }

    private void SetEnergy(Parameters par)
    {
        Debug.Log("Hello set energy");
        slider.value = par.GetIntExtra(ENERGY_INT, (int)slider.value);
    }

}
