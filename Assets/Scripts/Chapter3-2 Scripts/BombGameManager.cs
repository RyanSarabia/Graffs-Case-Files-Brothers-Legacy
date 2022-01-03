using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombGameManager : MonoBehaviour
{
    private static BombGameManager instance;
    public static BombGameManager GetInstance()
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

    [SerializeField] private GameObject victoryCard;
    [SerializeField] private GameObject clickBlocker;
    private bool panelFocus;

    [SerializeField] private List<Dial> dials = new List<Dial>();
    [SerializeField] private List<int> targetStates = new List<int>();
    private Dial activeDial;
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
    }
    public void unSelect()
    {
        activeDial = null;
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
}
