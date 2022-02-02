using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Pitcher : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    // Start is called before the first frame update
    [SerializeField] private int waterAmount;
    [SerializeField] private int waterCap;
    [SerializeField] TextMeshProUGUI waterAmountText;
    [SerializeField] TextMeshProUGUI waterCapText;
    [SerializeField] private Slider slider;

    [SerializeField] private GameObject highlight;

    [SerializeField] private int id;

    private new BoxCollider2D collider;
    private bool selected;
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        this.waterAmountText.SetText(waterAmount.ToString() + " L");
        this.waterCapText.SetText(waterCap.ToString() + " L");
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

    public void unSelect()
    {
        selected = false;
        highlight.SetActive(false);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selected = true;
        highlight.SetActive(true);
        PitcherActionManager.GetInstance().interact(id);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if(!selected)
            highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
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
        Debug.Log("SUMET WATER!!! = " + water);
        waterAmount = water;
    }
    public void colliderOff()
    {
        collider.enabled = false;
    }
}
