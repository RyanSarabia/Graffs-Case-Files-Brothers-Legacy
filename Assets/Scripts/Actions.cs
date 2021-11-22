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
    private int numClues = 0;
    private List<int> clueRoomIDs;
    private List<int> pickUpRoomIDs;

    [SerializeField] private GameObject victoryCard;
    [SerializeField] TextMeshProUGUI clueNumText;
    [SerializeField] TextMeshProUGUI clueTotalText;

    [SerializeField] SearchTraversal traversalManager;

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
            clueRoomIDs.Add(id);
            numClues++;
        }
            
        if(numClues == totalClues)
        {
            victoryCard.SetActive(true);
        }
    }

    public void pickUpGet(int id, int amount)
    {
        if (!pickUpRoomIDs.Exists(x => x == id))
        {
            Debug.Log("action get");
            pickUpRoomIDs.Add(id);
            traversalManager.addEnergy(amount);
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
}
