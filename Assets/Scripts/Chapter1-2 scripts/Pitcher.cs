using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Pitcher : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int waterAmount;
    [SerializeField] private int waterCap;
    [SerializeField] TextMeshProUGUI waterAmountText;
    [SerializeField] TextMeshProUGUI waterCapText;
    [SerializeField] private Slider slider;

    [SerializeField] private GameObject highlight;

    [SerializeField] private int id;
    private bool selected;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.waterAmountText.SetText(waterAmount.ToString());
        this.waterCapText.SetText(waterCap.ToString());
        if (slider)
        {
            slider.maxValue = waterCap;
            slider.value = waterAmount;
        }
    }

    public int getCap()
    {
        return waterCap;
    }

    public int getWaterAmount()
    {
        return waterAmount;
    }

    private void OnMouseDown()
    {
        selected = true;
        highlight.SetActive(true);
        PitcherActionManager.GetInstance().interact(id);
    }

    public void unSelect()
    {
        selected = false;
        highlight.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if(!selected)
            highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (!selected)
            highlight.SetActive(false);
    }

    public int fillWater(int water)
    {
        int excess;

        excess = waterAmount + water - waterCap;
        if(excess <= 0)
        {
            waterAmount += water;
            return 0;
        }
        else
        {
            waterAmount = waterCap;
            return excess;
        }
        
    }

    public void emptyPitcher()
    {
        waterAmount = 0;
    }

    public void setWater(int water)
    {
        waterAmount = water;
    }
}
