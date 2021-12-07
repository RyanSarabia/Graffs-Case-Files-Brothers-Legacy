using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class NPC : MonoBehaviour
{
    [SerializeField] private int ID;
    [SerializeField] private GameObject npc;
    [SerializeField] private bool leftSide;
    [SerializeField] private bool readyToLeave;

    [SerializeField] private TextMeshProUGUI speedText;
    [SerializeField] private int speed;

    [SerializeField] private Sprite frontSprite;
    [SerializeField] private Sprite backSprite;
    [SerializeField] private GameObject leftIdlePosition;
    [SerializeField] private GameObject leftReadyPosition;
    [SerializeField] private GameObject rightIdlePosition;
    [SerializeField] private GameObject rightReadyPosition;
    private Vector2 prevPos;

    private new BoxCollider2D collider;
    private new SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        renderer = GetComponent<SpriteRenderer>();
        speedText.SetText(speed.ToString());
        prevPos = leftIdlePosition.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        if (BridgeGameManager.GetInstance().getLanternPosition())
        {
            if (leftSide)
                collider.enabled = true;
            else
                collider.enabled = false;
        }
        else
        {
            if (leftSide)
                collider.enabled = false;
            else
                collider.enabled = true;
        }

        if (BridgeGameManager.GetInstance().getPanelFocus())
            collider.enabled = false;
    }
    public bool isLeftSide()
    {
        return leftSide;
    }
    public void setIsLeftSide(bool state)
    {
       leftSide = state;
    }
    public void setReady(bool state)
    {
        readyToLeave = state;
    }

    public int getSpeed()
    {
        return speed;
    }
    public int getID()
    {
        return ID;
    }

    public void setNPCActive(bool state)
    {
        npc.SetActive(state);
    }

    private void OnMouseDown()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
        if (leftSide)
        {          
            BridgeGameManager.GetInstance().leftSelectNPC(ID, readyToLeave);
        }
        else
        {            
            BridgeGameManager.GetInstance().rightSelectNPC(ID, readyToLeave);
        }
            
    }

    public void move()
    {
        if (leftSide)
        {
            if (!readyToLeave)
            {
                this.transform.position = new Vector2(leftReadyPosition.transform.position.x, leftReadyPosition.transform.position.y);
                readyToLeave = true;
            }
            else
            {
                this.transform.position = new Vector2(leftIdlePosition.transform.position.x, leftIdlePosition.transform.position.y);
                readyToLeave = false;
            }            
        }
        else
        {
            if (!readyToLeave)
            {
                this.transform.position = new Vector2(rightReadyPosition.transform.position.x, rightReadyPosition.transform.position.y);
                readyToLeave = true;
            }
            else
            {
                this.transform.position = new Vector2(rightIdlePosition.transform.position.x, rightIdlePosition.transform.position.y);
                readyToLeave = false;
            }            
        }
    }

    public void cross()
    {
        if (leftSide)
        {
            this.transform.position = new Vector2(rightIdlePosition.transform.position.x, rightIdlePosition.transform.position.y);
            renderer.sprite = frontSprite;
        }
        else
        {
            this.transform.position = new Vector2(leftIdlePosition.transform.position.x, leftIdlePosition.transform.position.y);
            renderer.sprite = backSprite;
        }
    }

    public void crossToRight()
    {        
        this.transform.position = new Vector2(rightIdlePosition.transform.position.x, rightIdlePosition.transform.position.y);
        renderer.sprite = frontSprite;
        leftSide = false;
    }
    public void crossToLeft()
    {               
        this.transform.position = new Vector2(leftIdlePosition.transform.position.x, leftIdlePosition.transform.position.y);
        renderer.sprite = backSprite;
        leftSide = true;
    }

    private void OnMouseEnter()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, .5f);
    }

    private void OnMouseExit()
    {
        this.GetComponent<SpriteRenderer>().color = new Color(1f, 1f, 1f, 1f);
    }
}
