using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitcherActionManager : MonoBehaviour, GMInterface
{
    // --------------------------- Singleton --------------------
    private static PitcherActionManager instance;
    public static PitcherActionManager GetInstance()
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
    [SerializeField] private static int targetAmount = 8;
    [SerializeField] private AdjacentStateManagerCh1_Pitchers adjacentStatePrefab;
    [SerializeField] private TimelineNode timelineNodePrefab;

    // UI stuff
    [SerializeField] private GameObject victoryCard;
    [SerializeField] private GameObject retryCard;
    [SerializeField] private GameObject clickBlocker;
    [SerializeField] private GameObject adjacentContainer;
    private bool panelFocus;
    private Vector3 timelineStartPosition;

    // GM variables
    private List<int> pitcherIDs;
    private List<Pitcher> pitcherList;
    [SerializeField] private Sink sink;
    [SerializeField] public Pitcher p1;
    [SerializeField] public Pitcher p2;
    [SerializeField] public Pitcher p3;

    [SerializeField] private AdjacentStateManagerCh1_Pitchers cam4CurrState;
    [SerializeField] private List<AdjacentStateManagerCh1_Pitchers> adjacentList = new List<AdjacentStateManagerCh1_Pitchers>();
    [SerializeField] private List<State_Pitchers> prevStates = new List<State_Pitchers>();
    [SerializeField] private List<TimelineNode> timelineNodes = new List<TimelineNode>();
    [SerializeField] private TimelineNode curTimelineNode;

    private static bool firstScene = true;
    private State_Pitchers curState;

    // Start is called before the first frame update
    void Start()
    {
        pitcherIDs = new List<int>();
        pitcherList = new List<Pitcher>();
        
        pitcherList.Add(p1);
        pitcherList.Add(p2);
        pitcherList.Add(p3);


        timelineNodes.Add(curTimelineNode);
        curState = newState();
        curState.setCurState(0, 0, 0);
        this.SetState(curState);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.CAM3_TO_MAINCAM, UpdateCam4StateFromCam3);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CONFIRMED, SetStateFromCam4);

        timelineStartPosition = new Vector3(curTimelineNode.transform.position.x, curTimelineNode.transform.position.y, curTimelineNode.transform.position.z);

        if (firstScene)
            firstScene = false;
        else
            SFXScript.GetInstance().PlayResetAnySFX();
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.CAM3_TO_MAINCAM);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.GRAPH_DEVICE_CONFIRMED);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void interact(int id)
    {
        int excess;
        int firstSelect;

        if (!pitcherIDs.Exists(x => x == id))
        {
            pitcherIDs.Add(id);

            if(pitcherIDs.Count > 1)
            {                
                firstSelect = pitcherIDs[0];

                if(firstSelect == -1) // if first click is sink
                   pitcherList[id].fillWater(100);
                else
                {
                    if(id == -1) //if you click sink second
                        pitcherList[firstSelect].emptyPitcher();
                    else
                    {
                        excess = pitcherList[id].fillWater(pitcherList[firstSelect].getWaterAmount());
                        pitcherList[firstSelect].emptyPitcher();
                        _ = pitcherList[firstSelect].fillWater(excess);
                    }
                        
                }

                unSelect();
                addPreviousNode();
                curState = newState();
                curState.setCurState(p1.getWaterAmount(), p2.getWaterAmount(), p3.getWaterAmount());

                this.SetState(curState);

                checkVictoryOrFail();
                
                EventBroadcaster.Instance.PostEvent(GraphGameEventNames.WATER_CHANGED);
            }           
        }
        else
        {
            unSelect();
        }
    }

    private void unSelect()
    {
        pitcherIDs.Clear();
        sink.unSelect();
        foreach (Pitcher pitcher in pitcherList)
            pitcher.unSelect();
    }

    private State_Pitchers newState()
    {
        State_Pitchers newState = new State_Pitchers();
        return newState;
    }

    private void addPreviousNode()
    {
        State_Pitchers prevState = curState;
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

        foreach (AdjacentStateManagerCh1_Pitchers adjacent_state in adjacentList)
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
    private void UpdateCam4State(State_Pitchers state)
    {
        this.cam4CurrState.SetState(state);
    }
    private void updateObjectsToState()
    {
        p1.setWater(curState.getP1());
        p2.setWater(curState.getP2());
        p3.setWater(curState.getP3());
    }

    private void checkVictoryOrFail()
    {
        int num;
        foreach (Pitcher pitcher in pitcherList)
        {
            num = pitcher.getWaterAmount();
            if (num == targetAmount)
            {
                gameEnd();
                victoryCard.SetActive(true);
                panelFocus = true;
                clickBlocker.gameObject.SetActive(true);
                SFXScript.GetInstance().VictorySFX();
            }
        }
    }

    public void gameEnd()
    {
        foreach (Pitcher pitcher in pitcherList)
        {
            pitcher.colliderOff();
        }
        sink.colliderOff();
        clickBlocker.SetActive(true);
    }

    // --------------------------- Getters & Setters ----------------------

    public void SetState(State_Pitchers state)
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
        checkVictoryOrFail();
    }

    public bool getPanelFocus()
    {
        return panelFocus;
    }

    public State_Pitchers GetPreviousNode(int index)
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
