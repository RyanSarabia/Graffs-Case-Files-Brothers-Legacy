using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    private static Actions instance;
    private int selectRoom;

    public GraphSpawner spawner;

    private List<Room> roomList;    
    private Room prevRoom;

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
       
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void character(int id)
    {
        roomList = spawner.getRoomList();
        Room room = roomList[id];

        if (!room.getCharacterPresent())
        {
            if(prevRoom != null)
                prevRoom.hideCharacter();
            room.showCharacter();
        }
            
        else
            room.hideCharacter();

        prevRoom = room;
    }
}
