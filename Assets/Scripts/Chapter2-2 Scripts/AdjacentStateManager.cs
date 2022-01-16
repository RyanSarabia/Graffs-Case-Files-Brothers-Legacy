using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdjacentStateManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI timeCounter;
    [SerializeField] private NPC child;
    [SerializeField] private NPC man;
    [SerializeField] private NPC woman;
    [SerializeField] private NPC oldie;

    [SerializeField] Image hoverImage;
    [SerializeField] Image highlightImage;
    [SerializeField] GameObject arrowHead;
    [SerializeField] int index;

    [SerializeField] private GameObject lanternLeft;
    [SerializeField] private GameObject lanternRight;
    private State_Bridge state = new State_Bridge();
    Parameters parameters;

    void Awake()
    {
        
        state.connectToGameObjects(timeCounter, child, man, woman, oldie);
    }

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CLICKED, DisableHighlight);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CONFIRM_OCCURRED, ConfirmEventOccurred);
    }


    public State_Bridge getState()
    {
        return state;
    }

    public void SetState(State_Bridge state)
    {
        this.state = state;
        this.state.connectToGameObjects(timeCounter, child, man, woman, oldie);
        this.state.updateObjectsToState();
        this.ToggleLanterns();
        //this.DisableNPCs();
    }
    private void OnMouseEnter()
    {
        this.hoverImage.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        this.hoverImage.gameObject.SetActive(false);
    }

    private void OnMouseDown()
    {
        
        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.GRAPH_DEVICE_CLICKED);
        DisableHighlight();
        this.highlightImage.gameObject.SetActive(true);
        this.arrowHead.GetComponent<SpriteRenderer>().color = Color.yellow;
        Debug.Log("ADJ INDEX = " + this.index);
        parameters = new Parameters();
        parameters.PutExtra("State Index", this.index);
    }

    private void ConfirmEventOccurred()
    {
        //this.highlightImage.gameObject.SetActive(false);
        if (parameters != null)
            EventBroadcaster.Instance.PostEvent(GraphGameEventNames.GRAPH_DEVICE_CONFIRMED, parameters);
    }

    private void DisableHighlight()
    {
        PurgeParameters();
        if (this.highlightImage != null)
            this.highlightImage.gameObject.SetActive(false);
        if (this.arrowHead != null)
            this.arrowHead.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void PurgeParameters()
    {
        this.parameters = null;
    }

    private void OnApplicationQuit()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.GRAPH_DEVICE_CONFIRM_OCCURRED);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.GRAPH_DEVICE_CLICKED);
    }

    public int GetIndex()
    {
        return index;
    }

    public State_Bridge GetState()
    {
        return state;
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }

    public void DisableNPCs()
    {
        this.child.DisableCollider();
        this.man.DisableCollider();
        this.woman.DisableCollider();
        this.oldie.DisableCollider();
    }

    public void ToggleLanterns()
    {
        if (state.getIsLanternLeft() == true)
        {
            this.lanternLeft.SetActive(true);
            this.lanternRight.SetActive(false);

        }
        else
        {
            this.lanternLeft.SetActive(false);
            this.lanternRight.SetActive(true);
        }
    }
}
