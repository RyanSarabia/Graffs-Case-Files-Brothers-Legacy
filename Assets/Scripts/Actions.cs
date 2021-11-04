using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Actions : MonoBehaviour
{
    private static Actions instance;
    private int selectRoom;

    public GraphSpawner spawner;

    private List<Room> roomList;
    private Room curRoom;
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
        }            

        if(curRoom.getIsLightOn())
            prevRoom = curRoom;
    }

    public Room GetCurrRoom()
    {
        return curRoom;
    }
}
