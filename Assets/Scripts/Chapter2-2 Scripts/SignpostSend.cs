using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignpostSend : MonoBehaviour
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
        if(BridgeGameManager.GetInstance().getPanelFocus())
            collider.enabled = false;
    }

    private void OnMouseDown()
    {
        BridgeGameManager.GetInstance().sendNPC();
        SFXScript.GetInstance().ClickGoSignSFX();
    }
}
