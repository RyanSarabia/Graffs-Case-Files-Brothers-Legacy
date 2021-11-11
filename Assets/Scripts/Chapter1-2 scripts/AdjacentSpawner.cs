using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdjacentSpawner : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        EventBroadcaster.Instance.AddObserver(GraphGameEventNames.ADJACENT_LISTED, this.SpawnStates);
    }

    private void OnDestroy()
    {
        EventBroadcaster.Instance.RemoveObserver(GraphGameEventNames.ADJACENT_LISTED);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SpawnStates()
    {

    }
}
