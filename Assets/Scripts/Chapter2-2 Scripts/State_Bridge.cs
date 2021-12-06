using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Bridge : MonoBehaviour
{

    [SerializeField] private NPC child_1;
    [SerializeField] private NPC man_2;
    [SerializeField] private NPC woman_5;
    [SerializeField] private NPC oldie_8;
    [SerializeField] private BridgeGameManager bridgeGM;
    private int timeTotal;
    private bool isLanternLeft;

    private List<NPC> leftSide = new List<NPC>();
    private List<NPC> rightSide = new List<NPC>();

    // Start is called before the first frame update
    void Start()
    {
        setCurState(0, false, "c,m", "w,o");
        getAdjacentNodes();
    }

    void setCurState(int timeTotal, bool isLanternLeft, string left, string right)
    {
        leftSide.Clear();
        rightSide.Clear();
        this.timeTotal = timeTotal;
        this.isLanternLeft = isLanternLeft;
        string[] leftChars = left.Split(',', System.StringSplitOptions.RemoveEmptyEntries);
        string[] rightChars = right.Split(',', System.StringSplitOptions.RemoveEmptyEntries);

        foreach (var letter in leftChars)
        {
            switch (letter)
            {
                case "c": leftSide.Add(child_1); break;
                case "m": leftSide.Add(man_2); break;
                case "w": leftSide.Add(woman_5); break;
                case "o": leftSide.Add(oldie_8); break;
            }
        }

        foreach (var letter in rightChars)
        {
            switch (letter)
            {
                case "c": rightSide.Add(child_1); break;
                case "m": rightSide.Add(man_2); break;
                case "w": rightSide.Add(woman_5); break;
                case "o": rightSide.Add(oldie_8); break;
            }
        }
        
        Debug.Log(leftSide);
        Debug.Log(rightSide);
    }

    void getAdjacentNodes()
    {
        bool isLeftActive = isLanternLeft;
        List<NPC> activeList;
        string strLeft = "";
        string strRight = "";

        if (isLeftActive)
        {
            activeList = leftSide;
            strRight = generateStrSide(rightSide);
        }
        else
        {
            activeList = rightSide;
            strLeft = generateStrSide(leftSide);
        }

        string tempLeft;
        string tempRight;

        for (int i = 0; i < activeList.Count; i++)
        {
            string iNPC = "," + idToString(activeList[i].getID());

            for (int j = i + 1; j < activeList.Count; j++)
            {
                tempLeft = new string(strLeft);
                tempRight = new string(strRight);

                string jNPC = "," + idToString(activeList[j].getID());

                if (isLeftActive)
                {
                    tempLeft = generateRemStr(leftSide, i, j);
                    tempRight += iNPC + jNPC;
                } else
                {
                    tempRight = generateRemStr(rightSide, i, j);
                    tempLeft += iNPC + jNPC;
                }

                //placeholder function!!!
                //setCurState(timeTotal, isLeftActive, strLeft, strRight);

                Debug.Log(i + "," + j + ": " + tempLeft + "||"+ tempRight);
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
    // scratch
    // c m w o

    // cm, cw, co, mw, mo, wo



    // Update is called once per frame
    void Update()
    {
        
    }

}
