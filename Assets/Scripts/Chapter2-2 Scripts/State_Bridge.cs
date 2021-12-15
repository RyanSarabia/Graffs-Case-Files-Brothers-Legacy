using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class State_Bridge : MonoBehaviour
{
    [SerializeField] private State_Bridge bridgePrefabCopy;

    [SerializeField] private bool isLanternLeft = true;
    [SerializeField] private int timeElapsed;

    [SerializeField] private TextMeshProUGUI timeCounter;
    [SerializeField] private NPC child_1;
    [SerializeField] private NPC man_2;
    [SerializeField] private NPC woman_5;
    [SerializeField] private NPC oldie_8;

    private string strLeft;
    private string strRight;

    [SerializeField] private List<NPC> leftSide = new List<NPC>();
    [SerializeField] private List<NPC> rightSide = new List<NPC>();

    private int nChildNodes;

    // Start is called before the first frame update
    void Start()
    {
        //EventBroadcaster.Instance.AddObserver(GraphGameEventNames.NPCS_MOVED, ClearAndGetAdjacent);
        //setCurState(0, true, "c,m,w,o", "");
    }

    private void OnDestroy()
    {
        //EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.NPCS_MOVED);
    }

    public void connectToGameObjects(TextMeshProUGUI timeCounter, NPC child, NPC man, NPC woman, NPC oldie) 
    {
        this.timeCounter = timeCounter;
        this.child_1 = child;
        this.man_2 = man;
        this.woman_5 = woman;
        this.oldie_8 = oldie;
    }

    public void setCurState(int timeElapsed, bool isLanternLeft, string left, string right)
    {
        Debug.Log(isLanternLeft);
        this.timeElapsed = timeElapsed;
        timeCounter.SetText("Time Left: " + (BridgeGameManager.targetTime - timeElapsed) + " min/s");
        
        this.isLanternLeft = isLanternLeft;

        this.strLeft = left;
        this.strRight = right;

        leftSide.Clear();
        rightSide.Clear();
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
    }

    public void getAdjacentNodes(GameObject adjacentContainer, List<State_Bridge> adjacentStates)
    {
        bool isLeftActive = this.isLanternLeft;
        Debug.Log("Test: "+isLeftActive);
        List<NPC> activeList;
        string strLeft = "";
        string strRight = "";

        if (isLeftActive)
        {
            activeList = leftSide;
            strRight = generateStrSide(rightSide);

            string tempLeft;
            string tempRight;

            for (int i = 0; i < activeList.Count; i++)
            {
                string iNPC = "," + idToString(activeList[i].getID());

                for (int j = i + 1; j < activeList.Count; j++)
                {
                    tempLeft = generateRemStr(leftSide, i, j);
                    tempRight = new string(strRight);

                    string jNPC = "," + idToString(activeList[j].getID());
                    tempRight += iNPC + jNPC;

                    //calculate time increase
                    int addedTime = activeList[i].getSpeed();
                    if(activeList[j].getSpeed() > addedTime)
                    {
                        addedTime = activeList[j].getSpeed();
                    }

                    //placeholder function!!!
                    createState(this.timeElapsed + addedTime, isLeftActive, tempLeft, tempRight, adjacentContainer, adjacentStates);

                    Debug.Log(i + "," + j + ": " + tempLeft + "||" + tempRight);
                }
            }
        }
        else
        {
            activeList = rightSide;
            strLeft = generateStrSide(leftSide);

            string tempLeft;
            string tempRight;

            for (int i = 0; i < activeList.Count; i++)
            {
                string iNPC = "," + idToString(activeList[i].getID());

                tempLeft = new string(strLeft);
                tempRight = generateRemStr(rightSide, i);

                tempLeft += iNPC;

                //calculate time increase
                int addedTime = activeList[i].getSpeed();

                //placeholder function!!!
                createState(this.timeElapsed + addedTime, isLeftActive, tempLeft, tempRight, adjacentContainer, adjacentStates);

                Debug.Log(i + ": " + tempLeft + "||" + tempRight);
            }

        }

        this.nChildNodes = adjacentStates.Count;
    }

    private void createState(int timeTotal, bool isLanternLeft, string left, string right, GameObject adjacentContainer, List<State_Bridge> adjacentStates)
    {
        // spawn here
        State_Bridge newState = GameObject.Instantiate(this.bridgePrefabCopy);
        newState.setCurState(timeTotal, isLanternLeft, left, right);
        newState.transform.SetParent(adjacentContainer.transform);
        newState.transform.position = new Vector3(newState.transform.position.x, newState.transform.position.y, 0);

        //State_Pitchers newState = new State_Pitchers(); //pangtest ko lang tong line na to pero mali to
        // hindi to gagana hanggat wala yung mismong newState sa scene
        // newState.setStatesAndPitcherValues(p1, p2, p3);
        adjacentStates.Add(newState);
        newState.GetComponent<AdjacentStateManager>().SetIndex(adjacentStates.FindIndex(x => x == newState));
    }

    private string generateRemStr(List<NPC> npcs, int i, int j)
    {
        List<NPC> remList = new List<NPC>(npcs);
        remList.RemoveAt(j);
        remList.RemoveAt(i);
        return generateStrSide(remList);
    }

    private string generateRemStr(List<NPC> npcs, int i)
    {
        List<NPC> remList = new List<NPC>(npcs);
        remList.RemoveAt(i);
        return generateStrSide(remList);
    }

    private string generateStrSide(List<NPC> npcs)
    {
        string result = "";

        foreach (var npc in npcs)
        {
            switch (npc.getID())
            {
                case 0: result += ",c"; break;
                case 1: result += ",m"; break;
                case 2: result += ",w"; break;
                case 3: result += ",o"; break;
            }
        }
        if(result.Length > 1)
        {
            return result.Substring(1);
        }
        else
        {
            return "";
        }
    }

    public static string idToString(int i)
    {
        switch (i)
        {
            case 0: return "c";
            case 1: return "m";
            case 2: return "w";
            case 3: return "o";
            default: return null;
        }
    }

    // -------------- getter funcs --------------

    public int getTimeElapsed()
    {
        return this.timeElapsed;
    }

    public bool getIsLanternLeft()
    {
        return this.isLanternLeft;
    }

    public string getStrLeft()
    {
        return strLeft;
    }
    public string getStrRight()
    {
        return strRight;
    }

    public int getChildNodes()
    {
        return nChildNodes;
    }
}
