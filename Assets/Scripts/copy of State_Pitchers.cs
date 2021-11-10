//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class State_Pitchers : MonoBehaviour
//{

//    [SerializeField] State_Pitchers pitchersPrefabCopy;
//    [SerializeField] Pitcher p1Object;
//    [SerializeField] Pitcher p2Object;
//    [SerializeField] Pitcher p3Object;

//    private int pitcher1; // max 16
//    private int pitcher2; // max 9
//    private int pitcher3; // max 7
//    private readonly static int P1MAX = 16; // max 16
//    private readonly static int P2MAX = 9; // max 9
//    private readonly static int P3MAX = 7; // max 7

//    [SerializeField] private List<State_Pitchers> adjacentStates;

//    // Start is called before the first frame update
//    void Start()
//    {
//        adjacentStates = new List<State_Pitchers>();
//        setCurState(0, 0, 0);
//    }

//    public void setCurState(int p1, int p2, int p3)
//    {
//        clearAdjacentNodes();

//        pitcher1 = p1;
//        pitcher2 = p2;
//        pitcher3 = p3;

//        getAdjacentNodes();
//    }
//    void setStatesAndPitcherValues(int p1, int p2, int p3)
//    {
//        pitcher1 = p1;
//        p1Object.setWater(p1);

//        pitcher2 = p2;
//        p2Object.setWater(p2);

//        pitcher3 = p3;
//        p3Object.setWater(p3);

//    }

//    void clearAdjacentNodes()
//    {
//        //remove adjacent nodes from graph device
//        adjacentStates.Clear();
//    }

//    void getAdjacentNodes()
//    {
//        // Fill actions
//        //if pitcher 1 not full
//        if (pitcher1 != P1MAX)
//            createState(P1MAX, pitcher2, pitcher3);
//        //if pitcher 2 not full
//        if (pitcher2 != P2MAX)
//            createState(pitcher1, P2MAX, pitcher3);
//        //if pitcher 3 not full
//        if (pitcher3 != P3MAX)
//            createState(pitcher1, pitcher2, P3MAX);

//        // empty actions
//        //if pitcher 1 not empty
//        if (pitcher1 != 0)
//            createState(0, pitcher2, pitcher3);
//        //if pitcher 2 not empty
//        if (pitcher2 != 0)
//            createState(pitcher1, 0, pitcher3);
//        //if pitcher 3 not empty
//        if (pitcher3 != 0)
//            createState(pitcher1, pitcher2, 0);

//        // from pitcher1 if pitcher 1 not empty
//        if (pitcher1 != 0)
//        {
//            //if pitcher 2 not full
//            if (pitcher2 != P2MAX)
//            {
//                transferAndCreateState("p1", "p2");
//            }
//            //if pitcher 3 not full
//            if (pitcher3 != P3MAX)
//            {
//                transferAndCreateState("p1", "p3");
//            }
//        }
//        // from pitcher2 if pitcher 2 not empty
//        if (pitcher2 != 0)
//        {
//            //if pitcher 1 not full
//            if (pitcher1 != P1MAX)
//            {
//                transferAndCreateState("p2", "p1");
//            }
//            //if pitcher 3 not full
//            if (pitcher3 != P3MAX)
//            {
//                transferAndCreateState("p2", "p3");
//            }
//        }
//        // from pitcher3 if pitcher 3 not empty
//        if (pitcher3 != 0)
//        {
//            //if pitcher 1 not full
//            if (pitcher1 != P1MAX)
//            {
//                transferAndCreateState("p3", "p1");
//            }
//            //if pitcher 2 not full 
//            if (pitcher2 != P2MAX)
//            {
//                transferAndCreateState("p3", "p2");
//            }
//        }
//    }

//    void createState(int p1, int p2, int p3)
//    {
//        // spawn here
//        // State_Pitchers newState = GameObject.Instantiate(this.pitchersPrefabCopy, this.transform);
        
//        // hindi to gagana hanggat wala yung mismong newState sa scene
//        // newState.setStatesAndPitcherValues(p1, p2, p3);
//        // adjacentStates.Add(newState);
//    }
//    private class IntWrapper
//    {
//        public int val;
//    }

//    void transferAndCreateState(string source, string dest)
//    {

//        IntWrapper p1 = new IntWrapper();
//        IntWrapper p2 = new IntWrapper();
//        IntWrapper p3 = new IntWrapper();

//        p1.val = pitcher1;
//        p2.val = pitcher2;
//        p3.val = pitcher3;


//        IntWrapper srcVar = p1; //tempVal
//        IntWrapper destVar = p2; //tempVal

//        switch (source)
//        {
//            case "p1": srcVar = p1; break;
//            case "p2": srcVar = p2; break;
//            case "p3": srcVar = p3; break;
//        }

//        int destMax = 0;
//        switch (dest)
//        {
//            case "p1": destVar = p1; destMax = P1MAX; break;
//            case "p2": destVar = p2; destMax = P2MAX; break;
//            case "p3": destVar = p3; destMax = P3MAX; break;
//        }

//        int space = destMax - destVar.val;
//        if(space >= srcVar.val)
//        {
//            destVar.val = destVar.val + srcVar.val;
//            srcVar.val = 0;
//        } else
//        {
//            destVar.val = destMax;
//            srcVar.val = srcVar.val - space;
//        }

//        createState(p1.val, p2.val, p3.val);
//    }

//    // Update is called once per frame
//    void Update()
//    {
        
//    }
//}
