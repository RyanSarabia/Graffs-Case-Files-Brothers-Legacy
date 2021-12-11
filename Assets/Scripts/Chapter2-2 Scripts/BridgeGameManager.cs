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

    [SerializeField] private GameObject StateBridge_container;
    [SerializeField] private State_Bridge StateBridge_template;
    [SerializeField] private List<State_Bridge> prevStates = new List<State_Bridge>();
    [SerializeField] private List<State_Bridge> adjacentList = new List<State_Bridge>();
    private State_Bridge curState;

    // Start is called before the first frame update
    void Start()
    {
        NPC.Add(child);
        NPC.Add(man);
        NPC.Add(woman);
        NPC.Add(oldie);
        curState = newState();
        curState.setCurState(0, true, "c,m,w,o", "");
        clearAdjacentNodes();
        curState.getAdjacentNodes(adjacentContainer, adjacentList);
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool getPanelFocus()
    {
        return panelFocus;
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

            State_Bridge prevState = curState;
            //prevState.connectToGameObjects() to prevNodes;
            prevStates.Add(prevState);

            curState = newState();
            curState.setCurState(tempTimeElapsed, !isLanternLeft, getLeftString(), getRightString());
            clearAdjacentNodes();
            curState.getAdjacentNodes(adjacentContainer, adjacentList);

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
        State_Bridge newState = GameObject.Instantiate(StateBridge_template);
        newState.transform.SetParent(StateBridge_container.transform);
        newState.transform.position = new Vector3(newState.transform.position.x, newState.transform.position.y, 0);
        newState.connectToGameObjects(timeCounter, child, man, woman, oldie);
        return newState;
    }

    private int numAtLeft()
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

    void clearAdjacentNodes()
    {
        //remove adjacent nodes from graph device

        foreach (State_Bridge adjacent_state in adjacentList)
        {
            Destroy(adjacent_state.gameObject);
        }
        adjacentList.Clear();
    }

}
