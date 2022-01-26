using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverworldLoading : MonoBehaviour
{
    [SerializeField] List<OverworldIcons> icons = new List<OverworldIcons>();

    private void Awake()
    {
    }
    // Start is called before the first frame update
    void Start()
    {
        int n;
        LevelData data = SaveSystem.LoadPlayer();

        if(data != null)
        {
            n = data.currentLevel;

            icons[n].exclamationState(true);
            icons[n].setCollider(true);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
