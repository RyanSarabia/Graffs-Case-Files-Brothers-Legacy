using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour
{
    //[SerializeField] private GameObject closedWall;
    [SerializeField] private GameObject openedWall;
    [SerializeField] private GameObject nextRoom;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void closeWall()
    {
        openedWall.SetActive(false);
        //closedWall.SetActive(true);
    }

    public void openWall()
    {
        //closedWall.SetActive(false);
        openedWall.SetActive(true);
    }

    public Vector3 getNextRoomPosition()
    {
        return nextRoom.transform.position;
    }
}
