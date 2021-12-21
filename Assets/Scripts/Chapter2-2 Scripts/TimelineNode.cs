using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using TMPro;

public class TimelineNode : MonoBehaviour
{
    public static string TIMELINE_NODE_INDEX = "TIMELINE_NODE_INDEX";

    // UI
    [SerializeField] private Button curNodeHighlight;
    [SerializeField] private TextMeshProUGUI curIndexText;
    [SerializeField] private GameObject youAreHere;
    [SerializeField] private GameObject questionMark;

    [SerializeField] private Button whiteNodeCircle;
    [SerializeField] private TextMeshProUGUI prevIndexText;
    [SerializeField] private GameObject branchBox;
    [SerializeField] private GameObject branchArrow;
    [SerializeField] private TextMeshProUGUI branchText;
    
    [SerializeField] private GameObject nextSpawn;

    // attributes
    [SerializeField] private State_Bridge state;
    [SerializeField] private bool isCurNode = false;

    // Start is called before the first frame update
    void Start()
    {
        if (isCurNode)
        {
            setIndex(1);
            curNodeHighlight.gameObject.SetActive(true);
            youAreHere.SetActive(true);
            questionMark.SetActive(true);

            whiteNodeCircle.gameObject.SetActive(false);
            branchArrow.SetActive(false);
            branchBox.SetActive(false);
            branchText.gameObject.SetActive(false);
        }
    }

    public void setState(State_Bridge newState)
    {
        state = newState;
        int n = state.getChildNodes();
        branchText.text = n.ToString() + " other\nbranches";
        this.gameObject.SetActive(true);
    }

    public Transform getNextSpawnPoint()
    {
        return nextSpawn.transform;
    }

    public void setIndex(int index)
    {
        curIndexText.text = index.ToString();
        prevIndexText.text = index.ToString();

        whiteNodeCircle.onClick.AddListener(() => {
            Parameters parameters = new Parameters();
            parameters.PutExtra(TIMELINE_NODE_INDEX, index - 1);
            EventBroadcaster.Instance.PostEvent(GraphGameEventNames.TIMELINE_PREVNODE_CLICKED);
        });
        curNodeHighlight.onClick.AddListener(() => {
            EventBroadcaster.Instance.PostEvent(GraphGameEventNames.TIMELINE_CURNODE_CLICKED);
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
