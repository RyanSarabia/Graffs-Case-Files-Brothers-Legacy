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
    private bool panelFocus;

    // GM variables
    [SerializeField] private NPC child;
    [SerializeField] private NPC man;
    [SerializeField] private NPC woman;
    [SerializeField] private NPC oldie;
    [SerializeField] private List<NPC> NPC = new List<NPC>();
    private List<NPC> selectedNPC = new List<NPC>();

    //[SerializeField] private GameObject StateBridge_container;
    //[SerializeField] private State_Bridge StateBridge_template;
    [SerializeField] private List<AdjacentStateManager> adjacentList = new List<AdjacentStateManager>();
    [SerializeField] private List<State_Bridge> prevStates = new List<State_Bridge>();
    [SerializeField] private List<TimelineNode> timelineNodes = new List<TimelineNode>();
    [SerializeField] private TimelineNode curTimelineNode;
    private State_Bridge curState;

    // Start is called before the first frame update
    void Start()
    {
        NPC.Add(child);
        NPC.Add(man);
        NPC.Add(woman);
        NPC.Add(oldie);

        timelineNodes.Add(curTimelineNode);
        curState = newState();
        curState.setCurState(0, true, "c,m,w,o", "");
        clearAdjacentNodes();
        curState.generateAdjacentNodes(adjacentContainer, adjacentList, adjacentStatePrefab);
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
            }
            else
            {
                Debug.Log("left side full");
                //play error sound
            }
        }
        else
        {            
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
            }
            else
            {
                Debug.Log("right side full");
                //same error sound prob
            }            
        }
        else
        {            
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
            clearAdjacentNodes();
            curState.generateAdjacentNodes(adjacentContainer, adjacentList, adjacentStatePrefab);

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


    // --------------------------- Getters & Setters ----------------------
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
        return prevStates[index];
    }

}
