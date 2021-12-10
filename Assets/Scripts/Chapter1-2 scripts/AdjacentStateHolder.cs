using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AdjacentStateHolder : MonoBehaviour
{

    [SerializeField] Pitcher p1Object;
    [SerializeField] Pitcher p2Object;
    [SerializeField] Pitcher p3Object;
    [SerializeField] Image hoverImage;
    [SerializeField] Image highlightImage;
    [SerializeField] GameObject arrowHead;
    Parameters parameters;
    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CONFIRM_OCCURRED, ConfirmEventOccurred);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CLICKED, DisableHighlight);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_RETURN_CLICKED, DisableHighlight);
    }


    public void setCurState(int p1, int p2, int p3)
    {
        p1Object.setWater(p1);
        p2Object.setWater(p2);
        p3Object.setWater(p3);

    }

    private void OnMouseDown()
    {
        
        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.GRAPH_DEVICE_CLICKED);
        this.arrowHead.GetComponent<SpriteRenderer>().color = Color.yellow;
        this.highlightImage.gameObject.SetActive(true);
        parameters = new Parameters();
        parameters.PutExtra("Pitcher 1 Value", p1Object.getWaterAmount());
        parameters.PutExtra("Pitcher 2 Value", p2Object.getWaterAmount());
        parameters.PutExtra("Pitcher 3 Value", p3Object.getWaterAmount());
        
    }

    private void OnMouseEnter()
    {
        this.hoverImage.gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        this.hoverImage.gameObject.SetActive(false);
    }

    private void DisableHighlight()
    {
        if (this.highlightImage!= null)
        this.highlightImage.gameObject.SetActive(false);
        if (this.arrowHead != null)
            this.arrowHead.GetComponent<SpriteRenderer>().color = Color.white;
    }

    private void ConfirmEventOccurred()
    {
        //this.highlightImage.gameObject.SetActive(false);
        if (parameters!=null)
            EventBroadcaster.Instance.PostEvent(GraphGameEventNames.GRAPH_DEVICE_CONFIRMED, parameters);
    }

    private void OnApplicationQuit()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.GRAPH_DEVICE_CONFIRM_OCCURRED);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.GRAPH_DEVICE_CLICKED);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.GRAPH_DEVICE_RETURN_CLICKED);
    }


}
