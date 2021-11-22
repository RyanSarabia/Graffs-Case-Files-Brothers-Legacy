using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Room : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private GameObject roomLight;

    [SerializeField] private Wall upWall;
    [SerializeField] private Wall downWall;
    [SerializeField] private Wall rightWall;
    [SerializeField] private Wall leftWall;

    private Vector2 coordinate = new Vector2();

    [SerializeField] private Room upNeighbor;
    [SerializeField] private Room rightNeighbor;
    [SerializeField] private Room leftNeighbor;
    [SerializeField] private Room downNeighbor;

    [SerializeField] private GameObject highlight;
    [SerializeField] private GameObject clueIcon;
    [SerializeField] private GameObject character;
    [SerializeField] private GameObject hintSprite;
    [SerializeField] private GameObject pickUpSprite;

    [SerializeField]private int roomID;
    [SerializeField]private bool characterPresent = false;
    [SerializeField]private bool isLightOn = false;

    [SerializeField] private bool hasClue = false;
    [SerializeField] private bool hasPickUp = false;
    [SerializeField] private int pickUpAmount;

    [SerializeField] private BoxCollider2D collider;

    // Start is called before the first frame update
    void Start()
    {
        //roomLight.SetActive(false);
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isLightOn)
        {
            collider.enabled = true;
        }
        else
            collider.enabled = false;
    }


    public Vector3 getNeighborSpawnPosition(string direction)
    {
        switch (direction)
        {
            case GraphGameEventNames.DIRECTION_UP:
                return upWall.getNextRoomPosition();
            case GraphGameEventNames.DIRECTION_DOWN:
                downWall.openWall();
                return downWall.getNextRoomPosition();
            case GraphGameEventNames.DIRECTION_LEFT:
                leftWall.openWall();
                return leftWall.getNextRoomPosition();
            case GraphGameEventNames.DIRECTION_RIGHT:
                rightWall.openWall();
                return rightWall.getNextRoomPosition();
            default: return Vector3.zero;
        }
    }

    public void lightOn()
    {
        this.roomLight.SetActive(true);
    }

    public void lightOff()
    {
        roomLight.SetActive(false);
    }

    public void setCoordinate(Vector2 newCoor)
    {
        coordinate = newCoor;
    }
    public Vector2 getCoordinate()
    {
        return coordinate;
    }

    public void setRoomID(int id)
    {
        roomID = id;
    }

    public int getRoomID()
    {
        return roomID;
    }

    public void setIsLightOn(bool state)
    {
        isLightOn = state;
        if (state && getRoomClueState())
            clueIcon.SetActive(true);
        else if (state && getRoomPickUpState())
            pickUpSprite.SetActive(true);
    }

    public bool getIsLightOn()
    {
        return isLightOn;
    }    

    public void setNeighborAndOpenWalls(Room neighbor, string localDirection)
    {
        switch (localDirection)
        {
            case GraphGameEventNames.DIRECTION_UP:
                upWall.openWall(); 
                upNeighbor = neighbor; break;
            case GraphGameEventNames.DIRECTION_DOWN:
                downWall.openWall(); 
                downNeighbor = neighbor; break;
            case GraphGameEventNames.DIRECTION_LEFT:
                leftWall.openWall(); 
                leftNeighbor = neighbor; break;
            case GraphGameEventNames.DIRECTION_RIGHT:
                rightWall.openWall(); 
                rightNeighbor = neighbor; break;
        }
    }
    public Room getNeighbor(string localDirection)
    {
        switch (localDirection)
        {
            case GraphGameEventNames.DIRECTION_UP: return upNeighbor; 
            case GraphGameEventNames.DIRECTION_DOWN: return downNeighbor; 
            case GraphGameEventNames.DIRECTION_LEFT: return leftNeighbor; 
            case GraphGameEventNames.DIRECTION_RIGHT: return rightNeighbor; 
            default: return null;
        }
    }

    public List<Room> getNeighbors()
    {
        List<Room> neighbors = new List<Room>();
        if (upNeighbor != null)
            neighbors.Add(upNeighbor);
        if (downNeighbor != null)
            neighbors.Add(downNeighbor);
        if (leftNeighbor != null)
            neighbors.Add(leftNeighbor);
        if (rightNeighbor != null)
            neighbors.Add(rightNeighbor);
        return neighbors;
    }

    private void OnMouseEnter()
    {
        //if (getRoomClueState())
        //    clueIcon.SetActive(true);
        //else
            highlight.SetActive(true);
    }

    private void OnMouseExit()
    {
        //if (getRoomClueState())
        //    clueIcon.SetActive(false);
        //else
            highlight.SetActive(false);        
    }

    public void OnMouseDown()
    {
        Debug.Log("any mouse click");
        if (!Actions.GetInstance().getMidLightUp())
        {
            Actions.GetInstance().character(roomID);
            if (getRoomClueState())
            {
                Actions.GetInstance().clueFound(roomID);
            }
            if (getRoomPickUpState())
            {
                Debug.Log("pickup");
                Actions.GetInstance().pickUpGet(roomID, pickUpAmount);
            }
        }
        
    }

    public void showCharacter()
    {
        Debug.Log("Show character");
        character.SetActive(true);
        characterPresent = true;
    }

    public void hideCharacter()
    {
        character.SetActive(false);
        characterPresent = false;
    }

    public bool getCharacterPresent()
    {
        return characterPresent;
    }

    public void SetHintObject(GameObject hint)
    {
        this.hintSprite = hint;
    }
    public void SetPickUpObject(GameObject PickUp)
    {
        this.pickUpSprite = PickUp;
    }

    public void SetCharacterHintOn()
    {
        this.hintSprite.SetActive(true);
    }

    public void SetCharacterHintOff()
    {
        this.hintSprite.SetActive(false);
    }

    public void setRoomClueState(bool state)
    {
        hasClue = state;
    }

    public bool getRoomClueState()
    {
        return hasClue;
    }

    public void setRoomPickUpState(bool state)
    {
        hasPickUp = state;
    }

    public bool getRoomPickUpState()
    {
        return hasPickUp;
    }

    public int getRoomPickUpAmount()
    {
        return pickUpAmount;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("pointer click");
        //throw new System.NotImplementedException();
    }
}
