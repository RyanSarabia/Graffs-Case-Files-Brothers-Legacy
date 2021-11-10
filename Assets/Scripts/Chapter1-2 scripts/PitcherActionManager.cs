using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PitcherActionManager : MonoBehaviour
{
    private static PitcherActionManager instance;
    public static PitcherActionManager GetInstance()
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

    private List<int> pitcherIDs;
    private List<Pitcher> pitcherList;

    [SerializeField] private State_Pitchers stateManager;
    [SerializeField] private Sink sink;
    [SerializeField] public Pitcher p1;
    [SerializeField] public Pitcher p2;
    [SerializeField] public Pitcher p3;

    // Start is called before the first frame update
    void Start()
    {
        pitcherIDs = new List<int>();
        pitcherList = new List<Pitcher>();
        
        pitcherList.Add(p1);
        pitcherList.Add(p2);
        pitcherList.Add(p3);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void interact(int id)
    {
        int excess;
        int firstSelect;

        if (!pitcherIDs.Exists(x => x == id))
        {
            pitcherIDs.Add(id);

            if(pitcherIDs.Count > 1)
            {                
                firstSelect = pitcherIDs[0];

                if(firstSelect == -1) // if first click is sink
                   pitcherList[id].fillWater(100);
                else
                {
                    if(id == -1) //if you click sink second
                        pitcherList[firstSelect].emptyPitcher();
                    else
                    {
                        excess = pitcherList[id].fillWater(pitcherList[firstSelect].getWaterAmount());
                        pitcherList[firstSelect].emptyPitcher();
                        _ = pitcherList[firstSelect].fillWater(excess);
                    }
                        
                }

                unSelect();
                stateManager.setCurState(p1.getWaterAmount(), p2.getWaterAmount(), p3.getWaterAmount());
                EventBroadcaster.Instance.PostEvent(GraphGameEventNames.WATER_CHANGED);
            }           
        }
        else
        {            
            unSelect();
        }
    }

    private void unSelect()
    {
        pitcherIDs.Clear();
        sink.unSelect();
        foreach (Pitcher pitcher in pitcherList)
            pitcher.unSelect();
    }
}
