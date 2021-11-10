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
    private List<object> obj;

    [SerializeField] private Sink sink;
    [SerializeField] private Pitcher p1;
    [SerializeField] private Pitcher p2;

    // Start is called before the first frame update
    void Start()
    {
        pitcherIDs = new List<int>();
        obj = new List<object>();
        obj.Add(sink);
        obj.Add(p1);
        obj.Add(p2);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void interact(int id)
    {
        int excess;

        if (!pitcherIDs.Exists(x => x == id))
        {
            pitcherIDs.Add(id);

            if(pitcherIDs.Count > 1)
            {
                switch (pitcherIDs[0])
                {
                    case 0: _ = ((Pitcher)obj[id]).fillWater(100); break;
                    case 1: 
                    if (id == 0)                        
                        ((Pitcher)obj[1]).emptyPitcher();
                    else
                    {
                        excess = ((Pitcher)obj[id]).fillWater(((Pitcher)obj[1]).getWaterAmount());
                        ((Pitcher)obj[1]).emptyPitcher();
                        _ = ((Pitcher)obj[1]).fillWater(excess);
                    }                              
                    break;
                    case 2:
                    if (id == 0)
                        ((Pitcher)obj[2]).emptyPitcher();
                    else
                    {
                        excess = ((Pitcher)obj[id]).fillWater(((Pitcher)obj[2]).getWaterAmount());
                        ((Pitcher)obj[2]).emptyPitcher();
                        _ = ((Pitcher)obj[2]).fillWater(excess);
                    }
                    break;
                }


                pitcherIDs.Clear();
                sink.unSelect();
                p1.unSelect();
                p2.unSelect();
            }           
        }
        else
        {
            pitcherIDs.Clear();

            switch (id)
            {
                case 0: sink.unSelect(); break;
                case 1: p1.unSelect(); break;
                case 2: p2.unSelect(); break;
            }
        }
    }
}
