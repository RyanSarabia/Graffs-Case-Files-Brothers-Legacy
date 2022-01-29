using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crosshair : MonoBehaviour
{
    [SerializeField] private GameObject image;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite card;
    [SerializeField] private Sprite knife;
    [SerializeField] private bool isCard;

    private int savedTurn = -1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateSprite()
    {
        spriteRenderer.color = Color.white;

        if (isCard)
        {
            spriteRenderer.sprite = card;
            spriteRenderer.transform.localScale = new Vector3(1, 1);
        }
        else
        {
            spriteRenderer.sprite = knife;            
        }
    }
    public void setOn(bool state)
    {
        image.SetActive(state);
    }

    public void turnUpdate(int turn)
    {
        if (turn == savedTurn + 1)
        {
            updateSprite();
        }

        if (savedTurn == -1)
        {
            savedTurn = turn;
        }            
    }
}
