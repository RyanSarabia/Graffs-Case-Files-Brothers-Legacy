using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AdjacentStateManager_Bomb : MonoBehaviour
{
    [SerializeField] private Dial dialUp;
    [SerializeField] private Dial dialLeft;
    [SerializeField] private Dial dialDown;

    [SerializeField] private TextMeshProUGUI turnsLeft;

    [SerializeField] Image hoverImage;
    [SerializeField] Image highlightImage;
    [SerializeField] GameObject arrowHead;
    [SerializeField] int index;
    private State_Bomb state = new State_Bomb();
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
        //p1Object.setWater(state.getP1());
        //p2Object.setWater(state.getP2());
        //p3Object.setWater(state.getP3());
        //Debug.Log("STATE P1: " + state.getP1());
        //Debug.Log("STATE P2: " + state.getP2());
        //Debug.Log("STATE P3: " + state.getP3());

        dialUp.setValue(state.getDUp());
        dialLeft.setValue(state.getDLeft());
        dialDown.setValue(state.getDDown());
        turnsLeft.SetText("" + state.getTurnsLeft());
    }


    public void SetState(State_Bomb state)
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

    public State_Bomb GetState()
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
