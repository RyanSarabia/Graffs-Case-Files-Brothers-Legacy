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
    private State_Bridge state = new State_Bridge();
    private Parameters parameters;

    void Awake()
    {
        state.connectToGameObjects(timeCounter, child, man, woman, oldie);
    }

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CLICKED, DisableHighlight);
        //EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.GRAPH_DEVICE_CONFIRM_OCCURRED);
    }

    // Update is called once per frame
    void Update()
    {
        
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
        if (this.highlightImage != null)
            this.highlightImage.gameObject.SetActive(false);
        if (this.arrowHead != null)
            this.arrowHead.GetComponent<SpriteRenderer>().color = Color.white;
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
}
