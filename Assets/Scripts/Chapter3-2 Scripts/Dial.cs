using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dial : MonoBehaviour
{
    private bool selected;
    [SerializeField] private GameObject highlight;

    [SerializeField] private int id;
    [SerializeField] private int state = 0;
    [SerializeField] private Dial followCW;
    [SerializeField] private Dial followCCW;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnMouseDown()
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
    public void unSelect()
    {
        selected = false;
        highlight.SetActive(false);
    }
    private void OnMouseEnter()
    {
        if (!selected)
            highlight.SetActive(true);
    }
    private void OnMouseExit()
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
}
