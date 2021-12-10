using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class AdjacentStateBridge : MonoBehaviour
{
    [SerializeField] private NPC child_1;
    [SerializeField] private NPC man_2;
    [SerializeField] private NPC woman_5;
    [SerializeField] private NPC oldie_8;
    [SerializeField] private TextMeshProUGUI timeCounter;
    [SerializeField] Image hoverImage;
    [SerializeField] Image highlightImage;
    Parameters parameters;

    private int timeTotal;
    private bool isLanternLeft;
    private List<NPC> leftSide = new List<NPC>();
    private List<NPC> rightSide = new List<NPC>();

    // Start is called before the first frame update
    void Start()
    {

        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CONFIRM_OCCURRED_CH2, ConfirmEventOccurred);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CLICKED_CH2, DisableHighlight);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setCurState(int timeTotal, bool isLanternLeft, string left, string right)
    {
        leftSide.Clear();
        rightSide.Clear();
        this.timeTotal = timeTotal;
        timeCounter.SetText("Time Left: " + (BridgeGameManager.targetTime - timeTotal) + " min");

        this.isLanternLeft = isLanternLeft;
        string[] leftChars = left.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
        string[] rightChars = right.Split(',', System.StringSplitOptions.RemoveEmptyEntries);

        foreach (var letter in leftChars)
        {
            switch (letter)
            {
                case "c": leftSide.Add(child_1); child_1.crossToLeft(); break;
                case "m": leftSide.Add(man_2); man_2.crossToLeft(); break;
                case "w": leftSide.Add(woman_5); woman_5.crossToLeft(); break;
                case "o": leftSide.Add(oldie_8); oldie_8.crossToLeft(); break;
            }
        }

        foreach (var letter in rightChars)
        {
            switch (letter)
            {
                case "c": rightSide.Add(child_1); child_1.crossToRight(); break;
                case "m": rightSide.Add(man_2); man_2.crossToRight(); break;
                case "w": rightSide.Add(woman_5); woman_5.crossToRight(); break;
                case "o": rightSide.Add(oldie_8); oldie_8.crossToRight(); break;
            }
        }

        Debug.Log(leftSide);
        Debug.Log(rightSide);
        this.child_1.GetComponent<BoxCollider2D>().enabled = false;
        this.woman_5.GetComponent<BoxCollider2D>().enabled = false;
        this.man_2.GetComponent<BoxCollider2D>().enabled = false;
        this.oldie_8.GetComponent<BoxCollider2D>().enabled = false;
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

        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.GRAPH_DEVICE_CLICKED_CH2);
        DisableHighlight();
        this.highlightImage.gameObject.SetActive(true);
        parameters = new Parameters();
        parameters.PutExtra("Child Left State", child_1.isLeftSide());
        parameters.PutExtra("Woman Left State", woman_5.isLeftSide());
        parameters.PutExtra("Man Left State", man_2.isLeftSide());
        parameters.PutExtra("Oldie Left State", oldie_8.isLeftSide());
        parameters.PutExtra("Lantern Position", this.isLanternLeft);
        parameters.PutExtra("Time Left", this.timeTotal);

    }

    private void ConfirmEventOccurred()
    {
        //this.highlightImage.gameObject.SetActive(false);
        if (parameters != null)
            EventBroadcaster.Instance.PostEvent(GraphGameEventNames.GRAPH_DEVICE_CONFIRMED_CH2, parameters);
    }

    private void DisableHighlight()
    {
        if (this.highlightImage != null)
            this.highlightImage.gameObject.SetActive(false);
    }

    private void OnApplicationQuit()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.GRAPH_DEVICE_CONFIRM_OCCURRED_CH2);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.GRAPH_DEVICE_CLICKED_CH2);
    }
}
