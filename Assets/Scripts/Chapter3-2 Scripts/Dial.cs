using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Dial : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
{
    private bool selected;
    [SerializeField] private GameObject highlight;

    [SerializeField] private int id;
    [SerializeField] private int state;
    [SerializeField] private Dial followCW;
    [SerializeField] private Dial followCCW;
    // Start is called before the first frame update
    void Start()
    {
        setValue(state);
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void unSelect()
    {
        selected = false;
        highlight.SetActive(false);
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        if (!selected)
        {
            selected = true;
            highlight.SetActive(true);
            BombGameManager.GetInstance().select(this);
        }
        else
        {
            selected = false;
            highlight.SetActive(false);
            BombGameManager.GetInstance().unSelect();
        }

    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!selected)
            highlight.SetActive(true);
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (!selected)
            highlight.SetActive(false);
    }

    public void rotateCW()
    {
        this.transform.Rotate(0, 0, -120);
        state = (state + 1) % 3;       
    }
    public void rotateCCW()
    {
        this.transform.Rotate(0, 0, 120);
        if (state == 0)
            state = 2;
        else
            state--;
    }
    public Dial getCWFollower(bool state)
    {
        if (state)
            return followCW;
        else
            return followCCW;
    }

    public int getState()
    {
        return state;
    }

    public void setValue(int n)
    {
        state = n;
        switch (n)
        {
            case 0: this.transform.rotation = Quaternion.Euler(0, 0, 30); break;
            case 1: this.transform.rotation = Quaternion.Euler(0, 0, 270); break;
            case 2: this.transform.rotation = Quaternion.Euler(0, 0, 150); break;
        }
    }
}
