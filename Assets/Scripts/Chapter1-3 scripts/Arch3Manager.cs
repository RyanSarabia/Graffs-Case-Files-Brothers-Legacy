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
        //EventBroadcaster.Instance.AddObserver(GraphGameEventNames.ARCH3_NODECLICKED, this.NodeClicked);
        selection = new List<Arch3Node>();
        selection.Add(startingNode);
        finalEdgeWeight = finalEdge.GetWeight();
    }
    private void OnDestroy()
    {
        //EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.GRAPH_DEVICE_CLICKED);
    }

    public void openActionsMenu(GraphNode selectedNode)
    {
        scanBtn.gameObject.SetActive(true);
        moveBtn.gameObject.SetActive(true);

        curSelectedNode = selectedNode.GetComponent<Arch3Node>();
    }

    public void closeActionsMenu()
    {
        scanBtn.gameObject.SetActive(false);
        moveBtn.gameObject.SetActive(false);

        curSelectedNode = null;
    }

    void clickScanBtn()
    {
        curSelectedNode.revealEdges();
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

    // Update is called once per frame
    void Update()
    {
        stepCountText.SetText(stepCount.ToString() + " Steps Taken");
    }
}
