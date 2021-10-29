using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldIcons : MonoBehaviour
{

    [SerializeField] private bool mainIconEnabled = true;
    [SerializeField] private bool exclamationEnabled = true;
    [SerializeField] private UnityEngine.UI.RawImage mainIcon;
    [SerializeField] private UnityEngine.UI.RawImage exclamationPoint;

    void Start()
    {
        if (!mainIconEnabled)
            mainIcon.gameObject.SetActive(false);
        if (!exclamationEnabled)
            exclamationPoint.gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
