using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Actions : MonoBehaviour
{
    private static Actions instance;
    private int selectRoom;

    public GraphSpawner spawner;

    private List<Room> roomList;
    private Room curRoom;
    private Room prevRoom;

    [SerializeField] private int totalClues;
    [SerializeField] private int wallCount = 0;
    private int numClues = 0;
    private List<int> clueRoomIDs;
    private List<int> pickUpRoomIDs;

    [SerializeField] private GameObject victoryCard;
    [SerializeField] TextMeshProUGUI clueNumText;
    [SerializeField] TextMeshProUGUI clueTotalText;
    [SerializeField] private Animator briefTextMaxWalls;

    [SerializeField] SearchTraversal traversalManager;
    [SerializeField] private bool isChapter1 = false;

    private bool midLightingUp;

    

    public static Actions GetInstance()
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

    // Start is called before the first frame update
    void Start()
    {
        curRoom = spawner.getFirstRoom();
        prevRoom = spawner.getFirstRoom();

        clueRoomIDs = new List<int>();
        pickUpRoomIDs = new List<int>();
        
        this.clueTotalText.SetText(totalClues.ToString());
    }

    // Update is called once per frame
    void Update()
    {
        this.clueNumText.SetText(numClues.ToString());
    
    }

    public void character(int id)
    {
        curRoom.SetCharacterHintOff();
        roomList = spawner.getRoomList();
        curRoom = roomList[id];

        if (!curRoom.getCharacterPresent() && curRoom.getIsLightOn())
        {
            EventBroadcaster.Instance.PostEvent(GraphGameEventNames.ARCH1_PLAYER_MOVED);
            if(prevRoom != null)
                prevRoom.hideCharacter();
            curRoom.showCharacter();
        }

        else
        {
            //curRoom.hideCharacter();
            //prevRoom.hideCharacter();
            Debug.Log("room not lit or character present");
            curRoom = prevRoom;
        }
        
        foreach (Room room in curRoom.getNeighbors())
        {
            if (room.getRoomClueState())
            {
                if(!clueRoomIDs.Exists(x => x == room.getRoomID()))
                    curRoom.SetCharacterHintOn();
            }
        }

        if(curRoom.getIsLightOn())
            prevRoom = curRoom;
    }

    public Room GetCurrRoom()
    {
        return curRoom;
    }

    public void clueFound(int id)
    {
        if(!clueRoomIDs.Exists(x => x == id))
        {
            curRoom.setClueSprite(false);
            clueRoomIDs.Add(id);
            numClues++;
            SFXScript.GetInstance().ClueAcquiredSFX();
        }
            
        if(numClues == totalClues)
        {
            victoryCard.SetActive(true);
            SFXScript.GetInstance().VictorySFX();
        }
    }

    public void pickUpGet(int id, int amount)
    {
        if (!pickUpRoomIDs.Exists(x => x == id))
        {

            Debug.Log("action get");
            pickUpRoomIDs.Add(id);
            traversalManager.addEnergy(amount);
            curRoom.setPickUpSprite(false);
            SFXScript.GetInstance().EnergyAcquiredSFX();
        }
    }

    public void setMidLightUp(bool state)
    {
        midLightingUp = state;
    }

    public bool getMidLightUp()
    {
        return midLightingUp;
    }

    public int getWallCount()
    {
        return wallCount;
    }

    public void setWallCount(int wallCount)
    {
        this.wallCount = wallCount;
    }

    public void AddWallToRoom(Room roomToAddWall)
    {
        if (!isChapter1)
        {
            if (getWallCount() < 3)
            {
                SFXScript.GetInstance().CreateWallSFX();
                roomToAddWall.ToggleHasWall();
                roomToAddWall.EnableWallSprite();
                setWallCount(getWallCount() + 1);
            }
            else
            {
                briefTextMaxWalls.Play("BriefText");
                Debug.Log("Max walls reached");
                //Max walls reached
            }
            EventBroadcaster.Instance.PostEvent(GraphGameEventNames.ARCH1_WALLS_EVENT);
        }
        
    }

    public void RemoveWallFromRoom(Room roomToRemoveWall)
    {
        if (!isChapter1)
        {
            roomToRemoveWall.ToggleHasWall();
            roomToRemoveWall.DisableWallSprite();
            setWallCount(getWallCount() - 1);
            EventBroadcaster.Instance.PostEvent(GraphGameEventNames.ARCH1_WALLS_EVENT);
        }
        
    }
}
