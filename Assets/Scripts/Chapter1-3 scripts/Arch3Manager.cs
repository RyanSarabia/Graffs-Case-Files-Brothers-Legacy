using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Arch3Manager : MonoBehaviour
{
    [SerializeField] private Arch3Player player;
    [SerializeField] private Arch3Node startingNode;
    [SerializeField] private Culprit culprit;
    private static Arch3Manager instance;
    private List<Arch3Node> selection;
    [SerializeField] private TextMeshProUGUI stepCountText;
    private int stepCount = 0;

    [SerializeField] private Arch3Node finalNode;
    [SerializeField] private Arch3Edge finalEdge;
    private int finalEdgeWeight;

    [SerializeField] private GameObject victoryCard;
    [SerializeField] private GameObject retryCard;
    [SerializeField] private GameObject clickBlocker;
    private bool panelFocus = false;   

    [SerializeField] private TextMeshProUGUI turnCountText;
    private int turnCount = 1;

    [SerializeField] private List<Arch3Edge> varEdges = new List<Arch3Edge>();
    [SerializeField] private List<int> edgeTurnChange = new List<int>();
    [SerializeField] private List<int> edgeWeightChange = new List<int>();

    private bool waitForAnimation;
    public static Arch3Manager GetInstance()
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

    

    Arch3Node curSelectedNode;
   

    // Start is called before the first frame update
    void Start()
    {      
        startingNode.revealEdges();
        startingNode.disableClick();
        selection = new List<Arch3Node>();
        selection.Add(startingNode);
        finalEdgeWeight = finalEdge.GetWeight();
        player.gameObject.transform.position = new Vector3(startingNode.transform.position.x, startingNode.transform.position.y + 0.3f, 0);
    }

    public void setWait(bool state)
    {
        waitForAnimation = state;
    }
    public void setStepCount(int num)
    {
        stepCount = num;
    }

    public void openActionsMenu(GraphNode selectedNode)
    {
        //scanBtn.gameObject.SetActive(true);      
        curSelectedNode = selectedNode.GetComponent<Arch3Node>();       
    }

    public void closeActionsMenu()
    {
        //scanBtn.gameObject.SetActive(false);    
        curSelectedNode = null;
    }

    public void clickScanBtn()
    {
        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.ARCH3_LOCKNODES);
        curSelectedNode.revealEdges();
        curSelectedNode.disableClick();
    }
    public void clickMoveBtn(GraphNode selectedNode)
    {
        if (!waitForAnimation)
        {       
            curSelectedNode = selectedNode.GetComponent<Arch3Node>();
            //player.gameObject.transform.position = new Vector3(curSelectedNode.transform.position.x, curSelectedNode.transform.position.y + 0.3f, 0);
       
            int weight = weightOfNodes();
            Debug.Log(weight.ToString());
            if(weight > 0)
            {
                player.move(weight, curSelectedNode);

                
                culprit.move(weight);

                stepCount += weight;

                turnCount++;

                for (int i = 0; i < edgeTurnChange.Count; i++)
                {
                    if(edgeTurnChange[i] == turnCount)
                    {
                        varEdges[i].addWeight(edgeWeightChange[i]);
                    }
                }            
            }
            if(curSelectedNode == finalNode && stepCount <= finalEdgeWeight)
            {
                victoryCard.SetActive(true);
                panelFocus = true;
                clickBlocker.gameObject.SetActive(true);
                SFXScript.GetInstance().VictorySFX();
            }
            SFXScript.GetInstance().ClickNodeArch3SFX();
        }
    }

    public Arch3Node getCurNode()
    {
        return curSelectedNode;
    }

    public void setCulprit(Culprit var)
    {
        culprit = var;
    }

    public int weightOfNodes()
    {
        bool adjacentFlag = true;
        int weight = 0;
        if (!selection.Exists(x => x == curSelectedNode))
        {
            selection.Add(curSelectedNode);

            if (selection.Count > 1)
            {                
                try
                {
                    weight = selection[0].getNeighbors()[curSelectedNode].GetWeight();                    
                }
                catch(KeyNotFoundException)
                {
                    adjacentFlag = false;
                }                

                if (!adjacentFlag)
                {
                    Debug.Log("These nodes arent adjacent boi");
                    weight = -1;
                }
                adjacentFlag = true;
                selection.RemoveAt(0);                
            }            
        }

        return weight;
    }

    public void retry()
    {
        SFXScript.GetInstance().DefeatSFX();
        retryCard.SetActive(true);
        panelFocus = true;
        clickBlocker.gameObject.SetActive(true);
    }

    public bool getPanelFocus()
    {
        return panelFocus;
    }

    // Update is called once per frame
    void Update()
    {
        stepCountText.SetText(stepCount.ToString() + " Steps Taken");
        turnCountText.SetText("Turn " + turnCount.ToString());
    }
}
