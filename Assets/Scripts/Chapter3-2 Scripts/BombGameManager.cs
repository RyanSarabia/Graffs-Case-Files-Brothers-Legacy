using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGameManager : MonoBehaviour, GMInterface
{
    // --------------------------- Singleton --------------------
    private static BombGameManager instance;
    public static BombGameManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        EventBroadcaster.Instance.RemoveAllObservers();
        if (instance == null)
            instance = this;
        else
            GameObject.Destroy(gameObject);
    }

    // --------------------------- GM stuff ----------------------

    // static
    [SerializeField] private AdjacentStateManager_Bomb adjacentStatePrefab;
    [SerializeField] private TimelineNode timelineNodePrefab;

    // UI stuff
    [SerializeField] private GameObject victoryCard;
    [SerializeField] private GameObject retryCard;
    [SerializeField] private GameObject clickBlocker;
    [SerializeField] private GameObject adjacentContainer;
    [SerializeField] private GameObject CWBtn;
    [SerializeField] private GameObject CCWBtn;
    private bool panelFocus;
    private Vector3 timelineStartPosition;

    // GM variables
    [SerializeField] private List<Dial> dials = new List<Dial>();
    [SerializeField] private List<int> targetStates = new List<int>();
    private Dial activeDial;
    private int time = 14;

    [SerializeField] private AdjacentStateManager_Bomb cam4CurrState;
    [SerializeField] private List<AdjacentStateManager_Bomb> adjacentList = new List<AdjacentStateManager_Bomb>();
    [SerializeField] private List<State_Bomb> prevStates = new List<State_Bomb>();
    [SerializeField] private List<TimelineNode> timelineNodes = new List<TimelineNode>();
    [SerializeField] private TimelineNode curTimelineNode;

    private static bool firstScene = true;
    private State_Bomb curState;

    // Start is called before the first frame update
    void Start()
    {
        timelineNodes.Add(curTimelineNode);
        curState = newState();
        curState.setCurState(dials[0].getState(), dials[1].getState(), dials[2].getState(), time);
        this.SetState(curState);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.CAM3_TO_MAINCAM, UpdateCam4StateFromCam3);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CONFIRMED, SetStateFromCam4);

        timelineStartPosition = new Vector3(curTimelineNode.transform.position.x, curTimelineNode.transform.position.y, curTimelineNode.transform.position.z);

        if (firstScene)
            firstScene = false;
        else
            SFXScript.GetInstance().PlayResetAnySFX();
    }
    // Update is called once per frame
    void Update()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.CAM3_TO_MAINCAM);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.GRAPH_DEVICE_CONFIRMED);
    }
    public void select(Dial dial)
    {
        if(activeDial != null)
            activeDial.unSelect();
        activeDial = dial;
        CWBtn.SetActive(true);
        CCWBtn.SetActive(true);
    }
    public void unSelect()
    {
        activeDial = null;
        CWBtn.SetActive(false);
        CCWBtn.SetActive(false);
    }

    public void rotateCW()
    {
        activeDial.rotateCW();
        activeDial.getCWFollower(true).rotateCW();
        checkVictory();
    }
    public void rotateCCW()
    {
        activeDial.rotateCCW();
        activeDial.getCWFollower(false).rotateCCW();
        checkVictory();
    }
    public void checkVictory()
    {
        int num = 0;

        for(int i = 0; i < dials.Count; i++)
        {
            if(dials[i].getState() == targetStates[i])
            {
                num++;
            }
        }

        if(num == dials.Count)
        {
            victoryCard.SetActive(true);
            panelFocus = true;
            clickBlocker.gameObject.SetActive(true);
            activeDial.unSelect();
            foreach(Dial dial in dials)
            {
                dial.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }
    public void testSetState0()
    {
        activeDial.setValue(0);
    }
    public void testSetState1()
    {
        activeDial.setValue(1);
    }
    public void testSetState2()
    {
        activeDial.setValue(2);
    }

    private State_Bomb newState()
    {
        State_Bomb newState = new State_Bomb();
        return newState;
    }

    private void addPreviousNode()
    {
        State_Bomb prevState = curState;
        prevStates.Add(prevState);

        TimelineNode newNode = GameObject.Instantiate(timelineNodePrefab, curTimelineNode.transform.parent);
        newNode.transform.position = timelineNodes[0].transform.position;

        // append prev to prevprev
        if (timelineNodes.Count > 1)
        {
            newNode.transform.position = timelineNodes[timelineNodes.Count - 2].getNextSpawnPoint().position;
        }

        // append cur to prev
        curTimelineNode.transform.position = newNode.getNextSpawnPoint().position;

        //add to list
        timelineNodes.Insert(timelineNodes.Count - 1, newNode);

        // set index
        curTimelineNode.setIndex(timelineNodes.Count);
        newNode.setIndex(timelineNodes.Count - 1);

        // set state
        newNode.setState(prevState);
    }

    void clearAdjacentNodes()
    {
        //remove adjacent nodes from graph device
        foreach (AdjacentStateManager_Bomb adjacent_state in adjacentList)
        {
            Destroy(adjacent_state.gameObject);
        }
        adjacentList.Clear();
    }

    // this is the revert function
    private void UpdateCam4StateFromCam3(Parameters parameters)
    {
        Debug.Log(parameters.GetIntExtra("CAM3_PREVSTATE_INDEX", 0));
        int prevIndex = parameters.GetIntExtra("CAM3_PREVSTATE_INDEX", 0);
        this.SetState(GetPreviousNode(prevIndex));

        while (!timelineNodes[prevIndex].Equals(curTimelineNode))
        {
            TimelineNode tempNode = timelineNodes[prevIndex];
            timelineNodes.RemoveAt(prevIndex);
            prevStates.RemoveAt(prevIndex);
            Destroy(tempNode.gameObject);
        }

        // append cur to prev
        if (prevIndex > 0)
            curTimelineNode.transform.position = timelineNodes[prevIndex - 1].getNextSpawnPoint().position;
        else
            curTimelineNode.transform.position = timelineStartPosition;
        curTimelineNode.setIndex(prevIndex + 1);
        SFXScript.GetInstance().ClickConfirmGraphDevice();
    }
    private void UpdateCam4State(State_Bomb state)
    {
        this.cam4CurrState.SetState(state);
    }
    private void updateObjectsToState()
    {
        dials[0].setValue(curState.getD1());
        dials[1].setValue(curState.getD2());
        dials[2].setValue(curState.getD3());
    }

    // --------------------------- Getters & Setters ----------------------
    public void SetState(State_Bomb state)
    {
        this.curState = state;
        updateObjectsToState();
        clearAdjacentNodes();
        this.curState.generateAdjacentNodes(adjacentContainer, adjacentList, adjacentStatePrefab);
        UpdateCam4State(curState);
        this.gameObject.GetComponent<ButtonScripts>().SetPrevStatesCount(this.GetPrevStatesCount());
    }

    public void SetStateFromCam4(Parameters parameters)
    {
        addPreviousNode();
        Debug.Log("STATE INDEX = " + parameters.GetIntExtra("State Index", 0));
        this.SetState(adjacentList[parameters.GetIntExtra("State Index", 0)].GetState());
        SFXScript.GetInstance().ClickConfirmGraphDevice();
        checkVictory();
    }

    public bool getPanelFocus()
    {
        return panelFocus;
    }

    public State_Bomb GetPreviousNode(int index)
    {
        if (index == -1)
        {
            return prevStates[prevStates.Count - 1];
        }
        else
            return prevStates[index];
    }

    public int GetPrevStatesCount()
    {
        return prevStates.Count;
    }

}
