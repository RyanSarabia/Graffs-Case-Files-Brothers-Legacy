using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BridgeGameManager : MonoBehaviour
{
    // --------------------------- Singleton --------------------
    private static BridgeGameManager instance;
    public static BridgeGameManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            GameObject.Destroy(gameObject);
    }


    // --------------------------- GM stuff ----------------------

    // static
    [SerializeField] public static int targetTime = 15;
    [SerializeField] private AdjacentStateManager adjacentStatePrefab;
    [SerializeField] private TimelineNode timelineNodePrefab;

    // UI stuff
    [SerializeField] private GameObject victoryCard;
    [SerializeField] private GameObject retryCard;
    [SerializeField] private GameObject clickBlocker;
    [SerializeField] private TextMeshProUGUI timeCounter;
    [SerializeField] private GameObject adjacentContainer;
    [SerializeField] private TextMeshProUGUI goText;
    private bool panelFocus;
    private Vector3 timelineStartPosition;

    // GM variables
    [SerializeField] private NPC child;
    [SerializeField] private NPC man;
    [SerializeField] private NPC woman;
    [SerializeField] private NPC oldie;
    [SerializeField] private List<NPC> NPC = new List<NPC>();
    private List<NPC> selectedNPC = new List<NPC>();

    //[SerializeField] private GameObject StateBridge_container;
    //[SerializeField] private State_Bridge StateBridge_template;
    [SerializeField] private AdjacentStateManager cam4CurrState;
    [SerializeField] private List<AdjacentStateManager> adjacentList = new List<AdjacentStateManager>();
    [SerializeField] private List<State_Bridge> prevStates = new List<State_Bridge>();
    [SerializeField] private List<TimelineNode> timelineNodes = new List<TimelineNode>();
    [SerializeField] private TimelineNode curTimelineNode;
    
    private State_Bridge curState;

    // Start is called before the first frame update
    void Start()
    {
        SetGoTextRed();

        NPC.Add(child);
        NPC.Add(man);
        NPC.Add(woman);
        NPC.Add(oldie);

        timelineNodes.Add(curTimelineNode);
        curState = newState();
        curState.setCurState(0, true, "c,m,w,o", "");
        this.SetState(curState);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.CAM3_TO_MAINCAM, UpdateCam4StateFromCam3);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CONFIRMED, SetStateFromCam4);

        timelineStartPosition = new Vector3(curTimelineNode.transform.position.x, curTimelineNode.transform.position.y, curTimelineNode.transform.position.z);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.CAM3_TO_MAINCAM);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void leftSelectNPC(int id, bool ready)
    {
        if (!ready)
        {
            if(selectedNPC.Count < 2)
            {               
                selectedNPC.Add(NPC[id]);
                NPC[id].move();
                if (selectedNPC.Count == 2)
                {
                    SetGoTextGreen();
                }
            }
            else
            {
                Debug.Log("left side full");
                //play error sound
            }
        }
        else
        {
            SetGoTextRed();
            selectedNPC.Remove(NPC[id]);
            NPC[id].move();
        }
        
    }

    public void rightSelectNPC(int id, bool ready)
    {
        if (!ready)
        {
            if (selectedNPC.Count < 1)
            {                
                selectedNPC.Add(NPC[id]);
                NPC[id].move();
                if (selectedNPC.Count == 1)
                {
                    SetGoTextGreen();
                }
            }
            else
            {
                Debug.Log("right side full");
                //same error sound prob
            }            
        }
        else
        {
            SetGoTextRed();
            selectedNPC.Clear();
            NPC[id].move();
        }        
    }

    public void sendNPC()
    {
        int tempTimeElapsed = curState.getTimeElapsed();
        bool isLanternLeft = curState.getIsLanternLeft();

        if (selectedNPC[0].isLeftSide() && selectedNPC.Count < 2)
        {
            Debug.Log("You are only sending one at the left");
        }
        else
        {
            if (selectedNPC.Count > 1)
                tempTimeElapsed += Mathf.Max(selectedNPC[0].getSpeed(), selectedNPC[1].getSpeed());
            else
                tempTimeElapsed += selectedNPC[0].getSpeed();         

            foreach (var npc in selectedNPC)
            {
                npc.setReady(false);
                npc.cross();
            }

            selectedNPC.Clear();

            //State_Bridge prevState = curState;
            ////prevState.connectToGameObjects() to prevNodes;
            //prevStates.Add(prevState);
            addPreviousNode();
            curState = newState();
            curState.setCurState(tempTimeElapsed, !isLanternLeft, getLeftString(), getRightString());

            this.SetState(curState);

            if (numAtLeft() == 0 && curState.getTimeElapsed() == targetTime)
            {
                victoryCard.SetActive(true);
                panelFocus = true;
                clickBlocker.gameObject.SetActive(true);
            }
            else if (curState.getTimeElapsed() > targetTime)
            {
                retryCard.SetActive(true);
                panelFocus = true;
                clickBlocker.gameObject.SetActive(true);
            }
        }

        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.NPCS_MOVED);
        SetGoTextRed();
    }

    private State_Bridge newState()
    {
        State_Bridge newState = new State_Bridge();
        newState.connectToGameObjects(timeCounter, child, man, woman, oldie);
        return newState;
    }

    private void addPreviousNode()
    {
        State_Bridge prevState = curState;
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
        timelineNodes.Insert(timelineNodes.Count-1, newNode);

        // set index
        curTimelineNode.setIndex(timelineNodes.Count);
        newNode.setIndex(timelineNodes.Count-1);

        // set state
        newNode.setState(prevState);
    }

    void clearAdjacentNodes()
    {
        //remove adjacent nodes from graph device

        foreach (AdjacentStateManager adjacent_state in adjacentList)
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
    }
    private void UpdateCam4State(State_Bridge state)
    {
        this.cam4CurrState.SetState(state);
    }

    // --------------------------- Getters & Setters ----------------------

    public void SetState(State_Bridge state)
    {
        this.curState = state;
        this.curState.connectToGameObjects(timeCounter, child, man, woman, oldie);
        this.curState.updateObjectsToState();
        foreach(NPC npc in NPC)
        {
            npc.UpdateCollider();
        }
        clearAdjacentNodes();
        this.curState.generateAdjacentNodes(adjacentContainer, adjacentList, adjacentStatePrefab);
        UpdateCam4State(curState);
        //this.DisableNPCs();
    }

    public void SetStateFromCam4(Parameters parameters)
    {
        addPreviousNode();
        Debug.Log("STATE INDEX = " + parameters.GetIntExtra("State Index", 0));
        this.SetState(adjacentList[parameters.GetIntExtra("State Index", 0)].GetState());
    }
    public bool getPanelFocus()
    {
        return panelFocus;
    }

    public int numAtLeft()
    {
        int num = 0;

        foreach(var npc in NPC)
        {
            if (npc.isLeftSide())
            {
                num++;
            }
        }

        return num;
    }

    public string getRightString()
    {
        string result = "";

        foreach(NPC npc in NPC)
        {
            if (!npc.isLeftSide())
            {
                switch (npc.getID())
                {
                    case 0: result += ",c"; break;
                    case 1: result += ",m"; break;
                    case 2: result += ",w"; break;
                    case 3: result += ",o"; break;
                }
            }
        }

        if (result.Length > 1)
        {
            return result.Substring(1);
        }
        else
        {
            return "";
        }
    }
    public string getLeftString()
    {
        string result = "";

        foreach (NPC npc in NPC)
        {
            if (npc.isLeftSide())
            {
                switch (npc.getID())
                {
                    case 0: result += ",c"; break;
                    case 1: result += ",m"; break;
                    case 2: result += ",w"; break;
                    case 3: result += ",o"; break;
                }
            }
        }

        if (result.Length > 1)
        {
            return result.Substring(1);
        }
        else
        {
            return "";
        }
    }

    public bool getIsLanternLeft()
    {
        return curState.getIsLanternLeft();
    }

    public State_Bridge GetPreviousNode(int index)
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

    public void SetGoTextRed()
    {
        this.goText.color = new Color(0.3301887f, 0, 0);
    }

    public void SetGoTextGreen()
    {
        this.goText.color = new Color(0.3789885f, 1.0f, 0.3537736f);
    }

}
