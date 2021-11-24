using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightButton : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnBFSButtonClicked()
    {
        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.BFS_BUTTON_CLICK);

    }

    public void OnDFSButtonClicked()
    {
        EventBroadcaster.Instance.PostEvent(GraphGameEventNames.DFS_BUTTON_CLICK);

    }
}