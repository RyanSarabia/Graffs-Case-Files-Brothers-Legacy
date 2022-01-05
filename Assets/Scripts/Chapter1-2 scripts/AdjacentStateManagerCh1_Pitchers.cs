using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdjacentStateManagerCh1_Pitchers : MonoBehaviour
{
    [SerializeField] private Pitcher p1Object;
    [SerializeField] private Pitcher p2Object;
    [SerializeField] private Pitcher p3Object;

    [SerializeField] Image hoverImage;
    [SerializeField] Image highlightImage;
    [SerializeField] GameObject arrowHead;
    [SerializeField] int index;
    private State_Pitchers state = new State_Pitchers();
    Parameters parameters;

    void Awake()
    {

    }

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CLICKED, DisableHighlight);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CONFIRM_OCCURRED, ConfirmEventOccurred);
    }

    private void updateObjectsToState()
    {
        p1Object.setWater(state.getP1());
        p2Object.setWater(state.getP2());
        p3Object.setWater(state.getP3());
        Debug.Log("STATE P1: " + state.getP1());
        Debug.Log("STATE P2: " + state.getP2());
        Debug.Log("STATE P3: " + state.getP3());
    }


    public void SetState(State_Pitchers state)
    {
        this.state = state;
        updateObjectsToState();
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

    public State_Pitchers GetState()
    {
        return state;
    }

    public int GetIndex()
    {
        return index;
    }

    public void SetIndex(int index)
    {
        this.index = index;
    }
}
