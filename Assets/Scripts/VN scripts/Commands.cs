using System.Collections;
using System.Collections.Generic;
using Naninovel;
using Naninovel.Commands;
using UnityEngine;
using UnityEngine.SceneManagement;

[CommandAlias("saveLevel")]
public class Commands : Command
{
    public IntegerParameter num;

    public override UniTask ExecuteAsync(AsyncToken asyncToken = default)
    {
        if (Assigned(num))
        {
            SaveSystem.SavePlayer(num);
        }
        else
        {
            SceneManager.UnloadSceneAsync(SceneManager.GetActiveScene().buildIndex);
            SceneManager.LoadScene("Overworld Map");
        }

        return UniTask.CompletedTask;
    }
}
