using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arch3Manager : MonoBehaviour
{

    private static Arch3Manager instance;
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
        //EventBroadcaster.Instance.AddObserver(GraphGameEventNames.ARCH3_NODECLICKED, this.NodeClicked);
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
