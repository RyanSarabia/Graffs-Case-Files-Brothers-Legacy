using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SearchTraversal : MonoBehaviour
{
    [SerializeField] GraphSpawner graphContainer;
    [SerializeField] int nRooms;
    List<Room> searchQueue;
    List<Room> visited;
    Stack<Room> dfsStack;
    int ctr = 0;
    Room firstRoom;
    List<Room> roomList;

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.BFS_BUTTON_CLICK, this.BFS);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.DFS_BUTTON_CLICK, this.DFSrecursive);
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.BFS_BUTTON_CLICK);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.DFS_BUTTON_CLICK);

    }

    public void BFS()
    {
        ctr++;
        Debug.Log(ctr);
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
}
