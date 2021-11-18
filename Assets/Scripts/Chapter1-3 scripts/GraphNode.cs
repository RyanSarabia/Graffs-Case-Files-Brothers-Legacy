using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject highlight;
    private bool selected = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //private void OnMouseDown()
    //{
    //    selected = true;
    //    highlight.SetActive(true);
    //    //PitcherActionManager.GetInstance().interact(id);
    //}

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
}
