using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Naninovel;

public class VNInitialization : MonoBehaviour
{
    // Start is called before the first frame update
    private async void Start()
    {
        await RuntimeInitializer.InitializeAsync();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void Awake()
    {
        if (Engine.Initialized) DoMyCustomWork();
        else Engine.OnInitializationFinished += DoMyCustomWork;
    }

    private async void DoMyCustomWork()
    {
        // Engine is initialized here, it's safe to use the APIs.
        var scriptPlayer = Engine.GetService<IScriptPlayer>();
        await scriptPlayer.PreloadAndPlayAsync("ch1-1");
    }
}
