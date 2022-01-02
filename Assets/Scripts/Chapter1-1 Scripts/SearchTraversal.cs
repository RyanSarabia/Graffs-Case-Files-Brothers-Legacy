using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class SearchTraversal : MonoBehaviour
{
    [SerializeField] GraphSpawner graphContainer;
    [SerializeField] EnergyBar energyBar;
    [SerializeField] int nRooms;
    [SerializeField] int nEnergy = 15;
    [SerializeField] int nLightUses = 10;
    [SerializeField] TextMeshProUGUI lightUsesText;
    [SerializeField] TextMeshProUGUI energyText;
    [SerializeField] TextMeshProUGUI energyToBeUsed;
    [SerializeField] private Button bfsButton;
    [SerializeField] private Button dfsButton;
    [SerializeField] private Button confirmButton;
    private bool hasChosenLight = false;

    private int energyHolder = 1;
    private int preLightCounter = 0;

    List<Room> searchQueue = new List<Room>();

    Room firstRoom;
    Room currRoom;
    List<Room> roomList;

    // Start is called before the first frame update
    void Start()
    {
        DisableConfirmButton();
        this.energyText.SetText("Remaining Energy: " + nEnergy);
        this.energyToBeUsed.SetText(energyHolder.ToString());
        this.lightUsesText.SetText("Light Uses Left: " + nLightUses);
        energyBar.SetMaxEnergy(nEnergy);
        graphContainer.getFirstRoom().setIsLightOn(true);
        graphContainer.getFirstRoom().lightOn();
        graphContainer.getFirstRoom().showCharacter();
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.BFS_BUTTON_CLICK, this.BFS2);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.DFS_BUTTON_CLICK, this.DFS2);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.PLUS_BUTTON_CLICK, this.PlusClicked);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.MINUS_BUTTON_CLICK, this.MinusClicked);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.ARCH1_CONFIRM_BUTTON_CLICK, this.confirmLighting);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.ARCH1_PLAYER_MOVED, this.PlayerMoved);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.ARCH1_WALLS_EVENT, this.ResetQueueAndCounter);
    }

    // Update is called once per frame
    void Update()
    {
        energyText.SetText("Remaining Energy: " + nEnergy);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.BFS_BUTTON_CLICK);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.DFS_BUTTON_CLICK);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.PLUS_BUTTON_CLICK);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.MINUS_BUTTON_CLICK);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.ARCH1_CONFIRM_BUTTON_CLICK);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.ARCH1_PLAYER_MOVED);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.ARCH1_WALLS_EVENT);
    }

    public void addEnergy(int num)
    {
        nEnergy += num;
        
    }

    public void BFS2()
    {
        if (searchQueue.Count > 0)
        {
            while (searchQueue.Count > 0)
            {
                searchQueue[0].preLightOff();
                searchQueue.RemoveAt(0);
            }
        }
        this.hasChosenLight = true;
        currRoom = Actions.GetInstance().GetCurrRoom();
        searchQueue = new List<Room>();
        bool allNeightborsSearched = false;
        int searchQueueCtr = 0;
        while (!allNeightborsSearched)
        {
            List<Room> neighbors = currRoom.getNeighbors();
            
            foreach (Room neighbor in neighbors)
            {
                if (!searchQueue.Contains(neighbor) && neighbor.getIsLightOn() == false)
                {
                    //if (currRoom.getNeighbor(GraphGameEventNames.DIRECTION_UP) == neighbor && !currRoom.getWall(GraphGameEventNames.DIRECTION_UP))
                    //{
                    //    Debug.Log(searchQueue.Count);
                    //    searchQueue.Add(neighbor);
                    //}
                    //else if (currRoom.getNeighbor(GraphGameEventNames.DIRECTION_DOWN) == neighbor && !currRoom.getWall(GraphGameEventNames.DIRECTION_DOWN))
                    //{
                    //    Debug.Log(searchQueue.Count);
                    //    searchQueue.Add(neighbor);
                    //}
                    //else if (currRoom.getNeighbor(GraphGameEventNames.DIRECTION_LEFT) == neighbor && !currRoom.getWall(GraphGameEventNames.DIRECTION_LEFT))
                    //{
                    //    Debug.Log(searchQueue.Count);
                    //    searchQueue.Add(neighbor);
                    //}
                    //else if (currRoom.getNeighbor(GraphGameEventNames.DIRECTION_RIGHT) == neighbor && !currRoom.getWall(GraphGameEventNames.DIRECTION_RIGHT))
                    //{
                    //    Debug.Log(searchQueue.Count);
                    //    searchQueue.Add(neighbor);
                    //}
                    if (!neighbor.getHasWall())
                    {
                        Debug.Log(searchQueue.Count);
                        searchQueue.Add(neighbor);
                    }
                    
                }
            }
            if (searchQueueCtr >= searchQueue.Count)
                allNeightborsSearched = true;
            else
            {
                currRoom = searchQueue[searchQueueCtr];
                searchQueueCtr++;
            }
         

        }
        if (searchQueue.Count > 0)
        {
            DisableBFSButton();
            EnableDFSButton();
            EnableConfirmButton();
            Room lightUpRoom = searchQueue[0];
            preLightCounter = 0;
            //StartCoroutine(lighterDelay(lightUpRoom, n));
            for(preLightCounter = 0; preLightCounter < energyHolder; preLightCounter++)
            {
                if (preLightCounter < searchQueue.Count)
                    searchQueue[preLightCounter].preLightOn();
                else
                    break;
            }
        }
        else
        {
            //Call event na walang adjacent rooms na dark
        }
    }
    public void DFS2()
    {
        if (searchQueue.Count > 0)
        {
            while (searchQueue.Count > 0)
            {
                searchQueue[0].preLightOff();
                searchQueue.RemoveAt(0);
            }
        }
        this.hasChosenLight = true;
        currRoom = Actions.GetInstance().GetCurrRoom();
        bool firstRun = true;
        searchQueue = new List<Room>();

        void recursion(Room node)
        {
            if (firstRun)
            {
                firstRun = false;
                List<Room> neighbors = node.getNeighbors();
                foreach (Room neighbor in neighbors)
                {
                    if (!searchQueue.Contains(neighbor) && neighbor.getIsLightOn() == false)
                    {
                        //if (node.getNeighbor(GraphGameEventNames.DIRECTION_UP) == neighbor && !node.getWall(GraphGameEventNames.DIRECTION_UP))
                        //    recursion(neighbor);
                        //else if (node.getNeighbor(GraphGameEventNames.DIRECTION_DOWN) == neighbor && !node.getWall(GraphGameEventNames.DIRECTION_DOWN))
                        //    recursion(neighbor);
                        //else if (node.getNeighbor(GraphGameEventNames.DIRECTION_RIGHT) == neighbor && !node.getWall(GraphGameEventNames.DIRECTION_RIGHT))
                        //    recursion(neighbor);
                        //else if (node.getNeighbor(GraphGameEventNames.DIRECTION_LEFT) == neighbor && !node.getWall(GraphGameEventNames.DIRECTION_LEFT))
                        //    recursion(neighbor);
                        if (!neighbor.getHasWall())
                            recursion(neighbor);
                    }
                }
            }
            else
            {
                searchQueue.Add(node);

                List<Room> neighbors = node.getNeighbors();
                foreach (Room neighbor in neighbors)
                {
                    if (!searchQueue.Contains(neighbor) && neighbor.getIsLightOn() == false)
                    {
                        //if (node.getNeighbor(GraphGameEventNames.DIRECTION_UP) == neighbor && !node.getWall(GraphGameEventNames.DIRECTION_UP))
                        //    recursion(neighbor);
                        //else if (node.getNeighbor(GraphGameEventNames.DIRECTION_DOWN) == neighbor && !node.getWall(GraphGameEventNames.DIRECTION_DOWN))
                        //    recursion(neighbor);
                        //else if (node.getNeighbor(GraphGameEventNames.DIRECTION_RIGHT) == neighbor && !node.getWall(GraphGameEventNames.DIRECTION_RIGHT))
                        //    recursion(neighbor);
                        //else if (node.getNeighbor(GraphGameEventNames.DIRECTION_LEFT) == neighbor && !node.getWall(GraphGameEventNames.DIRECTION_LEFT))
                        //    recursion(neighbor);
                        if (!neighbor.getHasWall())
                        recursion(neighbor);
                    }
                }
            }
            
        }

        recursion(currRoom);

        if (searchQueue.Count > 0)
        {
            DisableDFSButton();
            EnableBFSButton();
            EnableConfirmButton();
            Room lightUpRoom = searchQueue[0];
            preLightCounter = 0;
            //StartCoroutine(lighterDelay(lightUpRoom, n));
            for (preLightCounter = 0; preLightCounter < energyHolder; preLightCounter++)
            {
                if (preLightCounter < searchQueue.Count)
                    searchQueue[preLightCounter].preLightOn();
                else
                    break;
            }
        }
        else
        {
            //Call event na walang adjacent rooms na dark
        }
    }


    //public void DFSrecursive()
    //{
    //    Debug.Log(graphContainer.getFirstRoom());
    //    firstRoom = graphContainer.getFirstRoom();

    //    searchQueue = new List<Room>();

    //    void recursion(Room node)
    //    {
    //        searchQueue.Add(node);

    //        List<Room> neighbors = node.getNeighbors();
    //        foreach (Room neighbor in neighbors)
    //        {
    //            if (!searchQueue.Contains(neighbor))
    //            {
    //                recursion(neighbor);
    //            }
    //        }
    //    }

    //    recursion(firstRoom);

    //    Room lightUpRoom = searchQueue[0];
    //    int n = 0;
    //    StartCoroutine(lighterDelay(lightUpRoom, n));
    //}

    public void confirmLighting()
    {
        if (searchQueue.Count > 0)
        {
            for (int i = 0; i < preLightCounter; i++)
            {
                searchQueue[i].preLightOff();
            }
            this.hasChosenLight = false;
            StartCoroutine(lighterDelay(searchQueue[0], 0));
            DisableConfirmButton();
            EnableBFSButton();
            EnableDFSButton();
            SFXScript.GetInstance().ConfirmLightingSFX();
            
        }
        this.preLightCounter = 0;
    }

    private void PlayerMoved()
    {
        ResetQueueAndCounter();
    }

    private IEnumerator lighterDelay(Room lightUpRoom, int n)
    {
        Debug.Log("Running Coroutine" + "energy count: " + n);
        Actions.GetInstance().setMidLightUp(true);
        lightUpRoom.setIsLightOn(true);
        lightUpRoom.lightOn();
        n++;
        yield return new WaitForSeconds(1.0f);

        if(searchQueue.Count > 0)
            searchQueue.RemoveAt(0);

        if (searchQueue.Count > 0 && n < energyHolder)
        {
            StartCoroutine(lighterDelay(searchQueue[0], n));
        }
        else
        {
            Actions.GetInstance().setMidLightUp(false);
            nEnergy = nEnergy - energyHolder;
            energyBar.SetEnergy(nEnergy);
            
            energyHolder = 1;
            this.energyToBeUsed.SetText(energyHolder.ToString());
            DecrementLightUse();
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
            if (hasChosenLight && searchQueue.Count >= energyHolder)
            {
                searchQueue[preLightCounter].preLightOn();
                preLightCounter++;
            }
        }
        
    }

    public void MinusClicked()
    {
        if (energyHolder > 1)
        {
            energyHolder--;
            this.energyToBeUsed.SetText(energyHolder.ToString());
            if (hasChosenLight && searchQueue.Count > energyHolder)
            {
                searchQueue[preLightCounter - 1].preLightOff();
                preLightCounter--;
            }
        }
        
    }

    private void DecrementLightUse()
    {
        nLightUses--;
        this.lightUsesText.SetText("Light Uses Left: " + nLightUses);
    }

    public void ResetQueueAndCounter()
    {
        this.hasChosenLight = false;
        foreach (Room room in searchQueue)
        {
            room.preLightOff();
        }
        this.searchQueue.Clear();
        EnableBFSButton();
        EnableDFSButton();
        this.energyHolder = 1;
        this.energyToBeUsed.SetText(energyHolder.ToString());
    }

    private void EnableDFSButton()
    {
        this.dfsButton.interactable = true;
    }
    private void EnableBFSButton()
    {
        this.bfsButton.interactable = true;
    }
    private void EnableConfirmButton()
    {
        this.confirmButton.interactable = true;
    }

    private void DisableDFSButton()
    {
        this.dfsButton.interactable = false;
    }
    private void DisableBFSButton()
    {
        this.bfsButton.interactable = false;
    }
    private void DisableConfirmButton()
    {
        this.confirmButton.interactable = false;
    }
}
