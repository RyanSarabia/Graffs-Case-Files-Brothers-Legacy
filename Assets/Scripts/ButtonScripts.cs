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
    [SerializeField] private Button cam2MainBtn;
    [SerializeField] private Button cam3MainBtn;
    [SerializeField] private Button cam3Timeline;
    [SerializeField] private Button cam4MainBtn;
    [SerializeField] private Button cam4Timeline;

    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera cam2;
    [SerializeField] private Camera cam3;
    [SerializeField] private Camera cam4;

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CLICKED, ActivateConfirmButton);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.TIMELINE_PREVNODE_CLICKED, TimelinePrevNodeClicked);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.TIMELINE_CURNODE_CLICKED, TimelineCurNodeClicked);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.CAM3_TO_CAM4, cam3ToCam4);
        graphDeviceButton.onClick.AddListener(GraphDeviceClicked);
        returnToDefaultButton.onClick.AddListener(ReturnClicked);
        confirmButton.onClick.AddListener(ConfirmClicked);

        cam2MainBtn.onClick.AddListener(NavbarMainClicked);
        cam3MainBtn.onClick.AddListener(NavbarMainClicked);
        cam3Timeline.onClick.AddListener(NavbarTimelineClicked);
        // wala pang cam 4 navbar
        //cam4MainBtn.onClick.AddListener(NavbarMainClicked);
        //cam4Timeline.onClick.AddListener(NavbarTimelineClicked);
    }

    // Update is called once per frame
    void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.GRAPH_DEVICE_CLICKED);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.TIMELINE_PREVNODE_CLICKED);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.TIMELINE_CURNODE_CLICKED);
    }

    private void GraphDeviceClicked()
    {
        SetAllCamZero();
        cam2On();
    }

    public void TimelinePrevNodeClicked(Parameters parameters)
    {
        SetAllCamZero();
        cam3On();
    }
    public void TimelineCurNodeClicked()
    {
        SetAllCamZero();
        cam4On();

    }

    public void NavbarMainClicked()
    {
        SetAllCamZero();
        mainCamOn();
        //cam2.gameObject.SetActive(false);
        //cam3.gameObject.SetActive(false);
        //cam4.gameObject.SetActive(false);
        //mainCam.depth = 2;
    }
    public void NavbarTimelineClicked()
    {
        SetAllCamZero();
        cam2On();
    }

    private void cam3ToCam4()
    {
        SetAllCamZero();
        cam4On();
    }

    private void ReturnClicked()
    {
        SetAllCamZero();
        mainCamOn();
        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.GRAPH_DEVICE_RETURN_CLICKED);
        
    }

    private void ActivateConfirmButton()
    {
        confirmButton.gameObject.SetActive(true);
    }

    private void ConfirmClicked()
    {
        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.GRAPH_DEVICE_CONFIRM_OCCURRED);
        SetAllCamZero();
        mainCamOn();
        cam4.gameObject.SetActive(false);
        confirmButton.gameObject.SetActive(false);
    }

    private void SetAllCamZero()
    {
        mainCam.depth = -1;
        cam2.depth = -1;
        cam3.depth = -1;
        cam4.depth = -1;
        mainCam.gameObject.SetActive(false);
        cam2.gameObject.SetActive(false);
        cam3.gameObject.SetActive(false);
        cam4.gameObject.SetActive(false);
    }



    private void mainCamOn()
    {
        mainCam.depth = 99;
        mainCam.gameObject.SetActive(true);
    }

    private void cam2On()
    {
        cam2.depth = 99;
        cam2.gameObject.SetActive(true);
    }

    private void cam3On()
    {
        cam3.depth = 99;
        cam3.gameObject.SetActive(true);
    }

    private void cam4On()
    {
        cam4.depth = 99;
        cam4.gameObject.SetActive(true);
    }
}
