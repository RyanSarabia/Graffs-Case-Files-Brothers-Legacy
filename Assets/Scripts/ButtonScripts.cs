using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonScripts : MonoBehaviour
{

    [SerializeField] private Button graphDeviceButton;
    [SerializeField] private Button returnToDefaultButton;
    [SerializeField] private Button confirmButton;
    [SerializeField] private Button cam2MainBtn;
    [SerializeField] private Button cam3MainBtn;
    [SerializeField] private Button cam3Timeline;
    [SerializeField] private Button cam4MainBtn;
    [SerializeField] private Button cam4Timeline;
    [SerializeField] GameObject cam4LeftArrowGroup;
    [SerializeField] GameObject lastNode;
    [SerializeField] GameObject cam2Canvas;

    [SerializeField] private Camera mainCam;
    [SerializeField] private Camera cam2;
    [SerializeField] private Camera cam3;
    [SerializeField] private Camera cam4;

    

    private int prevStatesCount;

    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CLICKED, ActivateConfirmButton);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.TIMELINE_PREVNODE_CLICKED, TimelinePrevNodeClicked);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.TIMELINE_CURNODE_CLICKED, TimelineCurNodeClicked);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.GRAPH_DEVICE_CONFIRMED, cam4ToMainCam);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.CAM4_TO_CAM3, cam4ToCam3);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.CAM3_TO_CAM4, cam3ToCam4);
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.CAM3_TO_MAINCAM, cam3ToMainCam);
        graphDeviceButton.onClick.AddListener(GraphDeviceClicked);
        returnToDefaultButton.onClick.AddListener(ReturnClicked);
        confirmButton.onClick.AddListener(ConfirmClicked);
        cam4MainBtn.onClick.AddListener(NavbarMainClicked);
        cam4Timeline.onClick.AddListener(NavbarTimelineClicked);

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
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.CAM4_TO_CAM3);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.CAM3_TO_CAM4);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.CAM3_TO_MAINCAM);
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.GRAPH_DEVICE_CONFIRMED);
    }

    private void GraphDeviceClicked()
    {
        SetAllCamZero();
        cam2On();
        SFXScript.GetInstance().ClickGraphDevice();
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

    private void cam4ToCam3()
    {
        SetAllCamZero();
        cam3On();
    }

    private void cam4ToMainCam(Parameters parameters)
    {
        SetAllCamZero();
        mainCamOn();
    }

    private void cam3ToMainCam(Parameters parameters)
    {
        SetAllCamZero();
        mainCamOn();
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

    private void ToggleCam4LeftArrowGroup()
    {
        if (this.prevStatesCount < 1)
        {
            this.cam4LeftArrowGroup.SetActive(false);

        }
        else
            this.cam4LeftArrowGroup.SetActive(true);
    }

    private void SetAllCamZero()
    {
        mainCam.depth = -1;
        cam2.depth = -1;
        cam3.depth = -1;
        cam4.depth = -1;
        mainCam.gameObject.SetActive(false);
        cam2.gameObject.SetActive(false);
        cam2Canvas.SetActive(false);
        cam3.gameObject.SetActive(false);
        cam4.gameObject.SetActive(false);
        cam4LeftArrowGroup.SetActive(false);
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
        cam2.transform.position = new Vector3(this.lastNode.transform.position.x - 1.05f, cam2.transform.position.y, 0);
        cam2Canvas.SetActive(true);
    }

    private void cam3On()
    {
        cam3.depth = 99;
        cam3.gameObject.SetActive(true);
        cam4LeftArrowGroup.SetActive(false);
    }

    private void cam4On()
    {
        cam4.depth = 99;
        cam4.gameObject.SetActive(true);
        ToggleCam4LeftArrowGroup();
    }

    public void SetPrevStatesCount(int prevStatesCount)
    {
        this.prevStatesCount = prevStatesCount;
    }

}
