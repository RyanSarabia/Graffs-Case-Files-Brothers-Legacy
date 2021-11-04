using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SearchTraversal : MonoBehaviour
{
    [SerializeField] GraphSpawner graphContainer;
    [SerializeField] int nRooms;
    [SerializeField] int nEnergy = 10;
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] TextMeshProUGUI energyToBeUsed;
    private int energyHolder = 1;

    List<Room> searchQueue;
    List<Room> visited;
    Stack<Room> dfsStack;
    Room firstRoom;
    List<Room> roomList;

    // Start is called before the first frame update
    void Start()
    {
        this.energyText.SetText("Energy Left: " + nEnergy);
        this.energyToBeUsed.SetText(energyHolder.ToString());
        graphContainer.getFirstRoom().setIsLightOn(true);
        graphContainer.getFirstRoom().lightOn();
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.BFS_BUTTON_CLICK, this.BFS);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.DFS_BUTTON_CLICK, this.DFSrecursive);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.PLUS_BUTTON_CLICK, this.PlusClicked);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.MINUS_BUTTON_CLICK, this.MinusClicked);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.BFS_BUTTON_CLICK);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.DFS_BUTTON_CLICK);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.PLUS_BUTTON_CLICK);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.MINUS_BUTTON_CLICK);
    }

    public void BFS2() 
    { 

    }

    public void BFS()
    {
        //Debug.Log(graphContainer.getFirstRoom());
        firstRoom = graphContainer.getFirstRoom();
        roomList = graphContainer.getRoomList();

        searchQueue = new List<Room>();
        searchQueue.Add(firstRoom);

        int searchQueueCtr = 0;
        Room curRoom = firstRoom;
        while (searchQueue.Count < roomList.Count)
        {
            List<Room> neighbors = curRoom.getNeighbors();
            foreach (Room neighbor in neighbors)
            {
                if (!searchQueue.Contains(neighbor))
                {
                    searchQueue.Add(neighbor);
                }
            }
            searchQueueCtr++;
            curRoom = searchQueue[searchQueueCtr];
        }

        Room lightUpRoom = searchQueue[0];
        int n = 0;
        StartCoroutine(lighterDelay(lightUpRoom, n));

    }

    public void DFS()
    {
        Debug.Log(graphContainer.getFirstRoom());
        firstRoom = graphContainer.getFirstRoom();

        searchQueue = new List<Room>();
        dfsStack = new Stack<Room>();
        dfsStack.Push(firstRoom);

        while (dfsStack.Count > 0)
        {
            Room curRoom = dfsStack.Pop();

            if (searchQueue.Contains(curRoom))
            {
                continue;
            }

            searchQueue.Add(curRoom);

            List<Room> neighbors = curRoom.getNeighbors();
            foreach (Room neighbor in neighbors)
            {
                if (!searchQueue.Contains(neighbor))
                {
                    dfsStack.Push(neighbor);
                }
            }

        }

        Room lightUpRoom = searchQueue[0];
        int n = 0;
        StartCoroutine(lighterDelay(lightUpRoom, n));
    }

    public void DFSrecursive()
    {
        Debug.Log(graphContainer.getFirstRoom());
        firstRoom = graphContainer.getFirstRoom();

        searchQueue = new List<Room>();

        void recursion(Room node)
        {
            searchQueue.Add(node);

            List<Room> neighbors = node.getNeighbors();
            foreach (Room neighbor in neighbors)
            {
                if (!searchQueue.Contains(neighbor))
                {
                    recursion(neighbor);
                }
            }
        }

        recursion(firstRoom);

        Room lightUpRoom = searchQueue[0];
        int n = 0;
        StartCoroutine(lighterDelay(lightUpRoom, n));
    }

    private IEnumerator lighterDelay(Room lightUpRoom, int n)
    {
        Debug.Log("Running Coroutine" + "energy count: " + n);
        lightUpRoom.setIsLightOn(true);
        lightUpRoom.lightOn();
        n++;
        yield return new WaitForSeconds(1.0f);
        searchQueue.RemoveAt(0);

        if (searchQueue.Count > 0 && n < nRooms)
        {
            StartCoroutine(lighterDelay(searchQueue[0], n));
        }
    }

    public void lightOut()
    {
        roomList = graphContainer.getRoomList();

        foreach (Room room in roomList)
        {
            room.lightOff();
        }

    }
    public void PlusClicked()
    {
        if (energyHolder < nEnergy)
        {
            energyHolder++;
            this.energyToBeUsed.SetText(energyHolder.ToString());
        }
    }

    public void MinusClicked()
    {
        if (energyHolder > 1)
        {
            energyHolder--;
            this.energyToBeUsed.SetText(energyHolder.ToString());
        }
    }
}
