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

    [SerializeField] Button scanBtn;
    [SerializeField] Button moveBtn;

    Arch3Node curSelectedNode;

    // Start is called before the first frame update
    void Start()
    {
        scanBtn.onClick.AddListener(clickScanBtn);
        moveBtn.onClick.AddListener(clickMoveBtn);
        moveBtn.onClick.AddListener(clickScanBtn);
        startingNode.revealEdges();
        startingNode.disableClick();
        selection = new List<Arch3Node>();
        selection.Add(startingNode);
        finalEdgeWeight = finalEdge.GetWeight();
    }

    public void openActionsMenu(GraphNode selectedNode)
    {
        //scanBtn.gameObject.SetActive(true);
        moveBtn.gameObject.SetActive(true);

        curSelectedNode = selectedNode.GetComponent<Arch3Node>();
    }

    public void closeActionsMenu()
    {
        //scanBtn.gameObject.SetActive(false);
        moveBtn.gameObject.SetActive(false);

        curSelectedNode = null;
    }

    void clickScanBtn()
    {
        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.ARCH3_LOCKNODES);
        curSelectedNode.revealEdges();
        curSelectedNode.disableClick();
    }
    void clickMoveBtn()
    {
        player.gameObject.transform.position = new Vector3(curSelectedNode.transform.position.x, curSelectedNode.transform.position.y + 0.3f, 0);
       
        int weight = weightOfNodes();
        if(weight > 0)
        {
            culprit.move(weight);
            stepCount += weight;
        }
        if(curSelectedNode == finalNode && stepCount <= finalEdgeWeight)
        {
            victoryCard.SetActive(true);
            panelFocus = true;
            clickBlocker.gameObject.SetActive(true);
        }      
            
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
                    weight = selection[0].getNeighbors()[curSelectedNode];                    
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

    public void resetToStart()
    {
        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.ARCH3_LOCKNODES);

        selection.Clear();
        selection.Add(startingNode);
        player.gameObject.transform.position = new Vector3(startingNode.transform.position.x, startingNode.transform.position.y + 0.3f, 0);
        stepCount = 0;
        culprit.reset();
        panelFocus = false;
        clickBlocker.gameObject.SetActive(false);

        curSelectedNode = startingNode;
        curSelectedNode.revealEdges();
        curSelectedNode.disableClick();
    }

    public void retry()
    {
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
    }
}
