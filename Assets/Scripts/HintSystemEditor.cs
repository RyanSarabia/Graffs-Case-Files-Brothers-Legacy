using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HintSystem))]
public class HintSystemEditor : Editor
{
    // Start is called before the first frame update
    public override void OnInspectorGUI()
    {
        if (EditorApplication.isPlaying)
            DrawDefaultInspector();

        HintSystem script = (HintSystem)target;
        if (GUILayout.Button("Reset Hint Lvl"))
        {
            script.resetHintLvl();
        }
    }
}