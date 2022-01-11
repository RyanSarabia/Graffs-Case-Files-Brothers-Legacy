using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Bomb : State_Script_Interface
{
    private int dial1;
    private int dial2;
    private int dial3;

    private int nChildNodes;

    //CHANGE THE PARAMETER TYPES
    public void setCurState(int d1, int d2, int d3)
    {
        dial1 = d1;
        dial2 = d2;
        dial3 = d3;
    }

    public void generateAdjacentNodes(GameObject adjacentContainer, List<AdjacentStateManagerCh1_Pitchers> adjacentStates, AdjacentStateManagerCh1_Pitchers prefabCopy)
    {


        this.nChildNodes = adjacentStates.Count;
    }

    //CHANGE THE PARAMETER TYPES
    void createState(int d1, int d2, int d3, GameObject adjacentContainer, List<AdjacentStateManagerCh1_Pitchers> adjacentStates, AdjacentStateManagerCh1_Pitchers prefabCopy)
    {
        Debug.Log("P1: " + d1);
        Debug.Log("P2: " + d2);
        Debug.Log("P3: " + d3);
        // spawn here
        AdjacentStateManagerCh1_Pitchers newState = GameObject.Instantiate(prefabCopy);
        newState.transform.SetParent(adjacentContainer.transform);
        newState.transform.position = new Vector3(newState.transform.position.x, newState.transform.position.y, 0);
        newState.GetState().setCurState(d1, d2, d3);
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
