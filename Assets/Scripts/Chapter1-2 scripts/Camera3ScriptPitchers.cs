using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Camera3ScriptPitchers : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI siblingCountText;
    [SerializeField] AdjacentStateManagerCh1_Pitchers cam3AdjacentManager;
    [SerializeField] Button leftArrow;
    [SerializeField] Button rightArrow;
    [SerializeField] Button cam4LeftArrow;
    [SerializeField] private Button revertButton;
    [SerializeField] GameObject leftArrowGroup;
    

    private int prevStateIndex;
    private int siblingCount;

    void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.TIMELINE_PREVNODE_CLICKED, this.SetCameraState);
        leftArrow.onClick.AddListener(LeftArrowClicked);
        rightArrow.onClick.AddListener(RightArrowClicked);
        revertButton.onClick.AddListener(RevertButtonClicked);
        cam4LeftArrow.onClick.AddListener(ReturnToCam3FromCam4);
    }

    private void OnApplicationQuit()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.TIMELINE_PREVNODE_CLICKED);
    }

    public void SetCameraState(Parameters parameters)
    {

        State_Pitchers prevState;
        prevState = PitcherActionManager.GetInstance().GetPreviousNode(parameters.GetIntExtra("TIMELINE_NODE_INDEX", 0));
        this.prevStateIndex = parameters.GetIntExtra("TIMELINE_NODE_INDEX", 0);
        ToggleLeftArrowGroup();
        SetState(prevState);
        setSiblingCount(prevState.getChildNodes());
        setSiblingText();
    }
    public void setSiblingCount(int count)
    {
        this.siblingCount = count;
    }
    private void setSiblingText()
    {
        this.siblingCountText.SetText("You can visit " + siblingCount + " nodes from here");
    }

    private void SetState(State_Pitchers state)
    {
        this.cam3AdjacentManager.SetState(state);
        //this.cam3Prefab.setCurState(state.getTimeElapsed(), state.getIsLanternLeft(), state.getStrLeft(), state.getStrRight());
    }

    private void RightArrowClicked()
    {
        State_Pitchers prevState;
        prevStateIndex++;
        if (PitcherActionManager.GetInstance().GetPrevStatesCount() == prevStateIndex)
        {
            EventBroadcaster.Instance.PostEvent(GraphGameEventNames.CAM3_TO_CAM4);
        }
        else
        {
            ToggleLeftArrowGroup();
            prevState = PitcherActionManager.GetInstance().GetPreviousNode(prevStateIndex);
            SetState(prevState);
            setSiblingCount(prevState.getChildNodes());
            setSiblingText();
        }
    }

    private void LeftArrowClicked()
    {
        State_Pitchers prevState;
        prevStateIndex--;
        ToggleLeftArrowGroup();
        prevState = PitcherActionManager.GetInstance().GetPreviousNode(prevStateIndex);
        SetState(prevState);
        setSiblingCount(prevState.getChildNodes());
        setSiblingText();

    }

    private void ToggleLeftArrowGroup()
    {
        if (prevStateIndex == 0)
        {
            this.leftArrowGroup.SetActive(false);
            this.leftArrow.gameObject.SetActive(false);
        }

        else
        {
            this.leftArrowGroup.SetActive(true);
            this.leftArrow.gameObject.SetActive(true);
        }
            
    }



    private void RevertButtonClicked()
    {
        Parameters parameters = new Parameters();
        parameters.PutExtra("CAM3_PREVSTATE_INDEX", prevStateIndex);
        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.CAM3_TO_MAINCAM, parameters);
    }

    private void ReturnToCam3FromCam4()
    {
        State_Pitchers prevState;
        prevStateIndex = PitcherActionManager.GetInstance().GetPrevStatesCount()-1;
        ToggleLeftArrowGroup();
        prevState = PitcherActionManager.GetInstance().GetPreviousNode(-1);
        SetState(prevState);
        setSiblingCount(prevState.getChildNodes());
        setSiblingText();

        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.CAM4_TO_CAM3);
    }

}
