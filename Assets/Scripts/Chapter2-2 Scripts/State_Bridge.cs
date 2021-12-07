using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Bridge : MonoBehaviour
{
    [SerializeField] private AdjacentStateBridge bridgePrefabCopy;
    [SerializeField] private NPC child_1;
    [SerializeField] private NPC man_2;
    [SerializeField] private NPC woman_5;
    [SerializeField] private NPC oldie_8;
    [SerializeField] private BridgeGameManager bridgeGM;

    private List<NPC> leftSide = new List<NPC>();
    private List<NPC> rightSide = new List<NPC>();

    [SerializeField] private List<AdjacentStateBridge> adjacentStates = new List<AdjacentStateBridge>();
    [SerializeField] private GameObject adjacentContainer;

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.NPCS_MOVED, getAdjacentNodes);
        setCurState(0, true, "c,m,w,o", "");
        getAdjacentNodes();
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.NPCS_MOVED);
    }

    void setCurState(int timeTotal, bool isLanternLeft, string left, string right)
    {
        leftSide.Clear();
        rightSide.Clear();
        bridgeGM.setTotalTime(timeTotal);
        bridgeGM.setLanternPosition(isLanternLeft);
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

    void getAdjacentNodes()
    {
        bool isLeftActive = bridgeGM.getLanternPosition();
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

                    //placeholder function!!!
                    createState(bridgeGM.getCurrentTime(), isLeftActive, tempLeft, tempRight);

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

                //placeholder function!!!
                createState(bridgeGM.getCurrentTime(), isLeftActive, tempLeft, tempRight);

                Debug.Log(i + ": " + tempLeft + "||" + tempRight);
            }

        }
    }

    string generateRemStr(List<NPC> npcs, int i, int j)
    {
        List<NPC> remList = new List<NPC>(npcs);
        remList.RemoveAt(j);
        remList.RemoveAt(i);
        return generateStrSide(remList);
    }

    string generateRemStr(List<NPC> npcs, int i)
    {
        List<NPC> remList = new List<NPC>(npcs);
        remList.RemoveAt(i);
        return generateStrSide(remList);
    }

    string generateStrSide(List<NPC> npcs)
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

    string idToString(int i)
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

    void createState(int timeTotal, bool isLanternLeft, string left, string right)
    {
        // spawn here
        AdjacentStateBridge newState = GameObject.Instantiate(this.bridgePrefabCopy, this.transform);
        newState.setCurState(timeTotal, isLanternLeft, left, right);
        newState.transform.SetParent(adjacentContainer.transform);
        newState.transform.position = new Vector3(newState.transform.position.x, newState.transform.position.y, 0);

        //State_Pitchers newState = new State_Pitchers(); //pangtest ko lang tong line na to pero mali to
        // hindi to gagana hanggat wala yung mismong newState sa scene
        // newState.setStatesAndPitcherValues(p1, p2, p3);
        adjacentStates.Add(newState);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
