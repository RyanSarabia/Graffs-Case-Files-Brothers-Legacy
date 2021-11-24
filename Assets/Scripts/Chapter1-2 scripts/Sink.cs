using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sink : MonoBehaviour
{
    [SerializeField] private GameObject highlight;

    [SerializeField] private int id = -1;
    private new BoxCollider2D collider;

    private bool selected;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (!selected)
            highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        if (!selected)
            highlight.SetActive(false);
    }

    public void colliderOff()
    {
        collider.enabled = false;
    }
}
