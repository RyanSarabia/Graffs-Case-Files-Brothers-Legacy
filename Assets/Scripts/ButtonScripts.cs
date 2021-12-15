using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScripts : MonoBehaviour
{

    [SerializeField] private Button graphDeviceButton;
    [SerializeField] private Button returnToDefaultButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button revertButton;

    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera graphDeviceCam;

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CLICKED, ActivateConfirmButton);
        graphDeviceButton.onClick.AddListener(GraphDeviceClicked);
        returnToDefaultButton.onClick.AddListener(ReturnClicked);
        confirmButton.onClick.AddListener(ConfirmClicked);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.GRAPH_DEVICE_CLICKED);
    }

    private void GraphDeviceClicked()
    {
        mainCam.depth = 0;
        graphDeviceCam.gameObject.SetActive(true);
    }

    private void ReturnClicked()
    {
        mainCam.depth = 2;
        graphDeviceCam.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.GRAPH_DEVICE_RETURN_CLICKED);
    }

    private void ActivateConfirmButton()
    {
        confirmButton.gameObject.SetActive(true);
    }

    private void ConfirmClicked()
    {
        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.GRAPH_DEVICE_CONFIRM_OCCURRED);
        mainCam.depth = 2;
        graphDeviceCam.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
    }

    
}
