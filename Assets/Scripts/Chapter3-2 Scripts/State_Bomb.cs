using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Bomb : State_Script_Interface
{
    private int dialLeft;
    private int dialUp;
    private int dialDown;
    private int turnsLeft;

    private int nChildNodes;

    //CHANGE THE PARAMETER TYPES
    public void setCurState(int dLeft, int dUp, int dDown, int turnsLeft)
    {
        dialLeft = dLeft;
        dialUp = dUp;
        dialDown = dDown;
        this.turnsLeft = turnsLeft;
    }

    public void generateAdjacentNodes(GameObject adjacentContainer, List<AdjacentStateManager_Bomb> adjacentStates, AdjacentStateManager_Bomb prefabCopy)
    {
        List<int> vals = new List<int>();
        int newTurns = turnsLeft - 1;
        for (int i=0; i<3; i++)
        {
            vals.Clear();
            vals.Add(dialLeft);
            vals.Add(dialUp);
            vals.Add(dialDown);

            // add to current selection
            vals[i] = (vals[i] + 1) % 3;
            vals[(i+1)%3] = (vals[(i+1)%3] + 1) % 3;

            createState(vals[0], vals[1], vals[2], newTurns, adjacentContainer, adjacentStates, prefabCopy);

            // add another for '-1'
            vals[i] = (vals[i] + 1) % 3;
            vals[(i + 1) % 3] = (vals[(i + 1) % 3] + 1) % 3;
            createState(vals[0], vals[1], vals[2], newTurns, adjacentContainer, adjacentStates, prefabCopy);
        }

        Debug.Log("old (1,2,3): " + dialLeft + ',' + dialUp + ',' + dialDown);
        Debug.Log("new (1,2,3): " + vals[0] + ',' + vals[1] + ',' + vals[2]);

        this.nChildNodes = adjacentStates.Count;
    }

    //CHANGE THE PARAMETER TYPES
    void createState(int dLeft, int dUp, int dDown, int turnsLeft, GameObject adjacentContainer, List<AdjacentStateManager_Bomb> adjacentStates, AdjacentStateManager_Bomb prefabCopy)
    {
        Debug.Log("P1: " + dLeft);
        Debug.Log("P2: " + dUp);
        Debug.Log("P3: " + dDown);
        // spawn here
        AdjacentStateManager_Bomb newState = GameObject.Instantiate(prefabCopy);
        newState.transform.SetParent(adjacentContainer.transform);
        newState.transform.position = new Vector3(newState.transform.position.x, newState.transform.position.y, 0);
        newState.GetState().setCurState(dLeft, dUp, dDown, turnsLeft);
        newState.SetState(newState.GetState());

        adjacentStates.Add(newState);
        newState.SetIndex(adjacentStates.Count - 1);
    }

    public int getChildNodes()
    {
        return nChildNodes;
    }

    public int getDLeft()
    {
        return dialLeft;
    }
    public int getDUp()
    {
        return dialUp;
    }
    public int getDDown()
    {
        return dialDown;
    }

    public int getTurnsLeft()
    {
        return turnsLeft;
    }
}
