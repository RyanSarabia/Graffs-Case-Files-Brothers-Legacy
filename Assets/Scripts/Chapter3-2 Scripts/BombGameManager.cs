using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGameManager : MonoBehaviour, GMInterface
{
    // --------------------------- Singleton --------------------
    private static BombGameManager instance;
    public static BombGameManager GetInstance()
    {
        return instance;
    }

    private void Awake()
    {
        EventBroadcaster.Instance.RemoveAllObservers();
        if (instance == null)
            instance = this;
        else
            GameObject.Destroy(gameObject);
    }

    // --------------------------- GM stuff ----------------------

    // static
    [SerializeField] private AdjacentStateManagerCh1_Pitchers adjacentStatePrefab;
    [SerializeField] private TimelineNode timelineNodePrefab;

    // UI stuff
    [SerializeField] private GameObject victoryCard;
    [SerializeField] private GameObject retryCard;
    [SerializeField] private GameObject clickBlocker;
    [SerializeField] private GameObject adjacentContainer;
    [SerializeField] private GameObject CWBtn;
    [SerializeField] private GameObject CCWBtn;
    private bool panelFocus;
    private Vector3 timelineStartPosition;

    // GM variables
    [SerializeField] private List<Dial> dials = new List<Dial>();
    [SerializeField] private List<int> targetStates = new List<int>();
    private Dial activeDial;

    [SerializeField] private AdjacentStateManagerCh1_Pitchers cam4CurrState;
    [SerializeField] private List<AdjacentStateManagerCh1_Pitchers> adjacentList = new List<AdjacentStateManagerCh1_Pitchers>();
    [SerializeField] private List<State_Bomb> prevStates = new List<State_Bomb>();
    [SerializeField] private List<TimelineNode> timelineNodes = new List<TimelineNode>();
    [SerializeField] private TimelineNode curTimelineNode;

    private static bool firstScene = true;
    private State_Pitchers curState;

    // Start is called before the first frame update
    void Start()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
    public void select(Dial dial)
    {
        if(activeDial != null)
            activeDial.unSelect();
        activeDial = dial;
        CWBtn.SetActive(true);
        CCWBtn.SetActive(true);
    }
    public void unSelect()
    {
        activeDial = null;
        CWBtn.SetActive(false);
        CCWBtn.SetActive(false);
    }

    public void rotateCW()
    {
        activeDial.rotateCW();
        activeDial.getCWFollower(true).rotateCW();
        checkVictory();
    }
    public void rotateCCW()
    {
        activeDial.rotateCCW();
        activeDial.getCWFollower(false).rotateCCW();
        checkVictory();
    }
    public void checkVictory()
    {
        int num = 0;

        for(int i = 0; i < dials.Count; i++)
        {
            if(dials[i].getState() == targetStates[i])
            {
                num++;
            }
        }

        if(num == dials.Count)
        {
            victoryCard.SetActive(true);
            panelFocus = true;
            clickBlocker.gameObject.SetActive(true);
            activeDial.unSelect();
            foreach(Dial dial in dials)
            {
                dial.GetComponent<CircleCollider2D>().enabled = false;
            }
        }
    }
    public void testSetState0()
    {
        activeDial.setValue(0);
    }
    public void testSetState1()
    {
        activeDial.setValue(1);
    }
    public void testSetState2()
    {
        activeDial.setValue(2);
    }


    public int GetPrevStatesCount()
    {
        return prevStates.Count;
    }

}
