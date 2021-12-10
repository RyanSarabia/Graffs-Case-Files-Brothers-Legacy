using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class State_Dial : MonoBehaviour
{
    [SerializeField] private AdjacentStateDial dialPrefabCopy;
    [SerializeField] private int curVal = 0;
    [SerializeField] private List<AdjacentStateDial> adjacentStates = new List<AdjacentStateDial>();
    [SerializeField] private GameObject adjacentContainer;

    public static readonly int DIAL_SIZE = 7;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void getAdjacentNodes()
    {
        int temp;

        // get +3
        temp = (curVal + 3) % DIAL_SIZE;
        createState(temp);

        // get +4
        temp = (curVal + 4) % DIAL_SIZE;
        createState(temp);
    }
    private void createState(int nextPosition)
    {
        // spawn here
        AdjacentStateDial newState = GameObject.Instantiate(this.dialPrefabCopy, this.transform);
        newState.setCurState(nextPosition);
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
