using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BridgeGameManager : MonoBehaviour
{
    private static BridgeGameManager instance;
    public static BridgeGameManager GetInstance()
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

    [SerializeField] private TextMeshProUGUI timeCounter;
    [SerializeField] private List<NPC> NPC = new List<NPC>();    

    private List<NPC> selectedNPC = new List<NPC>();

    private bool lanternAtLeft = true;

    private int totalSpeed;

    [SerializeField] private int targetTime = 15;
    [SerializeField] private GameObject victoryCard;
    [SerializeField] private GameObject retryCard;
    [SerializeField] private GameObject clickBlocker;
    private bool panelFocus;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timeCounter.SetText("Time Taken: " + totalSpeed + " min");
    }

    public bool getLanternPosition()
    {
        return lanternAtLeft;
    }
    public bool getPanelFocus()
    {
        return panelFocus;
    }

    public int getTargetTime()
    {
        return targetTime;
    }
    public int getCurrentTime()
    {
        return totalSpeed;
    }

    public void leftSelectNPC(int id, bool ready)
    {
        if (!ready)
        {
            if(selectedNPC.Count < 2)
            {               
                selectedNPC.Add(NPC[id]);
                NPC[id].move();
            }
            else
            {
                Debug.Log("left side full");
                //play error sound
            }
        }
        else
        {            
            selectedNPC.Remove(NPC[id]);
            NPC[id].move();
        }
        
    }

    public void rightSelectNPC(int id, bool ready)
    {
        if (!ready)
        {
            if (selectedNPC.Count < 1)
            {                
                selectedNPC.Add(NPC[id]);
                NPC[id].move();
            }
            else
            {
                Debug.Log("right side full");
                //same error sound prob
            }            
        }
        else
        {            
            selectedNPC.Clear();
            NPC[id].move();
        }        
    }

    public void sendNPC()
    {      
        if (selectedNPC[0].isLeftSide() && selectedNPC.Count < 2)
        {
            Debug.Log("You are only sending one at the left");
        }
        else
        {
            if (selectedNPC.Count > 1)
                totalSpeed += Mathf.Max(selectedNPC[0].getSpeed(), selectedNPC[1].getSpeed());
            else
                totalSpeed += selectedNPC[0].getSpeed();         

            foreach (var npc in selectedNPC)
            {
                //npc.cross();
                npc.setReady(false);
                if (npc.isLeftSide())
                {
                    npc.crossToRight();
                    lanternAtLeft = false;
                }
                else
                {
                    npc.crossToLeft();
                    lanternAtLeft = true;
                }
            }

            selectedNPC.Clear();

            if (numAtLeft() == 0 && totalSpeed == targetTime)
            {
                victoryCard.SetActive(true);
                panelFocus = true;
                clickBlocker.gameObject.SetActive(true);
            }
            else if (totalSpeed > targetTime)
            {
                retryCard.SetActive(true);
                panelFocus = true;
                clickBlocker.gameObject.SetActive(true);
            }
        }
    }

    private int numAtLeft()
    {
        int num = 0;

        foreach(var npc in NPC)
        {
            if (npc.isLeftSide())
            {
                num++;
            }
        }

        return num;
    }

    public void setLanternPosition(bool isLeft)
    {
        this.lanternAtLeft = isLeft;
    }
    public void setTotalTime(int time)
    {
        this.totalSpeed = time;
    }
}
