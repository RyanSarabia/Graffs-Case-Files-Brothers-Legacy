using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sink : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
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
        if (!selected)
            highlight.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!selected)
            highlight.SetActive(false);
    }

    public void colliderOff()
    {
        collider.enabled = false;
    }
}
