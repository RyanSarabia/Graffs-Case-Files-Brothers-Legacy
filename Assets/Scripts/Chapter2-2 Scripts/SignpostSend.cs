using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SignpostSend : MonoBehaviour, IPointerClickHandler
{
    private new BoxCollider2D collider;
    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        //if(BridgeGameManager.GetInstance().getPanelFocus())
        //    collider.enabled = false;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("sign click");
        BridgeGameManager.GetInstance().sendNPC();
    }
}
