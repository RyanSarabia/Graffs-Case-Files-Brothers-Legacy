using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnergySelector : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ClickPlus()
    {
        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.PLUS_BUTTON_CLICK);
    }
    public void ClickMinus()
    {
        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.MINUS_BUTTON_CLICK);
    }
}
