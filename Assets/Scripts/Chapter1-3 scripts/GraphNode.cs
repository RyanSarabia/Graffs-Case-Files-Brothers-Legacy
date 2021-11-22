using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GraphNode : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] GameObject highlight;
    [SerializeField] Arch3Node node;
    private bool selected = false;
    private bool isClickSource = false;
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.ARCH3_NODECLICKED, this.handleMouseDown);
    }
    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.ARCH3_NODECLICKED);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (!Arch3Manager.GetInstance().getPanelFocus())
        {
            isClickSource = true;
            EventBroadcaster.Instance.PostEvent(GraphGameEventNames.ARCH3_NODECLICKED);
        }
        
    }

    private void handleMouseDown()
    {
        if (selected)
        {
            unSelect();
            if (isClickSource)
            {
                Arch3Manager.GetInstance().closeActionsMenu();
            }
        }
        else if (!selected && isClickSource)
        {
            select();
            Arch3Manager.GetInstance().openActionsMenu(this);
        }
        isClickSource = false;
        
        //if (selected || !isClickSource)
        //{
        //    unSelect();
        //}
        //else if (isClickSource)
        //{
        //    isClickSource = false;
        //    select();
        //    Arch3Manager.GetInstance().openActionsMenu(this);
        //}
    }

    public void select()
    {
        selected = true;
        highlight.SetActive(true);
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
}
