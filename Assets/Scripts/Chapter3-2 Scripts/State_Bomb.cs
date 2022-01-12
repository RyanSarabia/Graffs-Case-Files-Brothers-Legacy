using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Bomb : State_Script_Interface
{
    private int dial1;
    private int dial2;
    private int dial3;
    private int turnsLeft;

    private int nChildNodes;

    //CHANGE THE PARAMETER TYPES
    public void setCurState(int d1, int d2, int d3, int turnsLeft)
    {
        dial1 = d1;
        dial2 = d2;
        dial3 = d3;
        this.turnsLeft = turnsLeft;
    }

    public void generateAdjacentNodes(GameObject adjacentContainer, List<AdjacentStateManager_Bomb> adjacentStates, AdjacentStateManager_Bomb prefabCopy)
    {
        List<int> vals = new List<int>();
        int newTurns = turnsLeft - 1;
        for (int i=0; i<3; i++)
        {
            vals.Clear();
            vals.Add(dial1);
            vals.Add(dial2);
            vals.Add(dial3);

            // add to current selection
            vals[i] = (vals[i] + 1) % 3;
            vals[(i+1)%3] = (vals[(i+1)%3] + 1) % 3;

            createState(vals[0], vals[1], vals[2], newTurns, adjacentContainer, adjacentStates, prefabCopy);

            // add another for '-1'
            vals[i] = (vals[i] + 1) % 3;
            vals[(i + 1) % 3] = (vals[(i + 1) % 3] + 1) % 3;
            createState(vals[0], vals[1], vals[2], newTurns, adjacentContainer, adjacentStates, prefabCopy);
        }

        Debug.Log("old (1,2,3): " + dial1 + ',' + dial2 + ',' + dial3);
        Debug.Log("new (1,2,3): " + vals[0] + ',' + vals[1] + ',' + vals[2]);

        this.nChildNodes = adjacentStates.Count;
    }

    //CHANGE THE PARAMETER TYPES
    void createState(int d1, int d2, int d3, int turnsLeft, GameObject adjacentContainer, List<AdjacentStateManager_Bomb> adjacentStates, AdjacentStateManager_Bomb prefabCopy)
    {
        Debug.Log("P1: " + d1);
        Debug.Log("P2: " + d2);
        Debug.Log("P3: " + d3);
        // spawn here
        AdjacentStateManager_Bomb newState = GameObject.Instantiate(prefabCopy);
        newState.transform.SetParent(adjacentContainer.transform);
        newState.transform.position = new Vector3(newState.transform.position.x, newState.transform.position.y, 0);
        newState.GetState().setCurState(d1, d2, d3, turnsLeft);
        newState.SetState(newState.GetState());

        adjacentStates.Add(newState);
        newState.SetIndex(adjacentStates.Count - 1);
    }

    public int getChildNodes()
    {
        return nChildNodes;
    }

    public int getD1()
    {
        return dial1;
    }
    public int getD2()
    {
        return dial2;
    }
    public int getD3()
    {
        return dial3;
    }
}
