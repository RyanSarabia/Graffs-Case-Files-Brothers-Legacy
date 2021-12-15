using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Camera3Script : MonoBehaviour
{

    [SerializeField] TextMeshProUGUI siblingCountText;
    [SerializeField] State_Bridge cam3Prefab;
    private int siblingCount;

    void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.CAMERA_3_CALLED, SetCameraState);
    }

    private void OnApplicationQuit()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.CAMERA_3_CALLED);

    }

    private void SetCameraState(Parameters parameters)
    {
        SetState(BridgeGameManager.GetInstance().GetPreviousNode(parameters.GetIntExtra("Previous node index", 0)));
        setSiblingCount(cam3Prefab.getChildNodes());
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

    private void SetState(State_Bridge state)
    {
        this.cam3Prefab.setCurState(state.getTimeElapsed(), state.getIsLanternLeft(), state.getStrLeft(), state.getStrRight());
    }
}
