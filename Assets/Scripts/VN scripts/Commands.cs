using System.Collections;
using System.Collections.Generic;
using Naninovel;
using Naninovel.Commands;
using UnityEngine;
using UnityEngine.SceneManagement;

[CommandAlias("changeScene")]
public class Commands : Command
{
    public StringParameter Name;

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        if (Assigned(Name))
        {
            SceneManager.LoadScene(Name);
        }
        else
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("Overworld Map");
        }

        return UniTask.CompletedTask;
    }
}
