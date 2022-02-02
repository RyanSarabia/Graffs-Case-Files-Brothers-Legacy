using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class GraphNode : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler,IPointerExitHandler
{
    // Start is called before the first frame update
    [SerializeField] GameObject highlight;
    [SerializeField] Arch3Node node;
    private bool selected = false;
    private bool isClickSource = false;
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.ARCH3_NODECLICKED, this.handleMouseDown);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.ARCH3_LOCKNODES, this.unSelect);
    }
    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.ARCH3_NODECLICKED);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.ARCH3_LOCKNODES);
    }

    // Update is called once per frame
    void Update()
    {
        
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
            Arch3Manager.GetInstance().clickMoveBtn(this);
            Arch3Manager.GetInstance().clickScanBtn();
        }
        isClickSource = false;
        
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

    public void OnPointerClick(PointerEventData eventData)
    {
        if (!Arch3Manager.GetInstance().getPanelFocus())
        {
            isClickSource = true;
            EventBroadcaster.Instance.PostEvent(GraphGameEventNames.ARCH3_NODECLICKED);
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
}
