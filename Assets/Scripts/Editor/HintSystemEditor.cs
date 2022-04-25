using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(HintSystem))]
public class HintSystemEditor : Editor
{
    SerializedProperty hintLvl;
    SerializedProperty disabled;
    SerializedProperty hintBtn;
    SerializedProperty hintBtnDefault;
    SerializedProperty hintBtnAlt;
    SerializedProperty img;
    SerializedProperty textBox;
    SerializedProperty prevBtn;
    SerializedProperty nextBtn;
    SerializedProperty doneBtn;
    SerializedProperty pics;
    SerializedProperty texts;

    void OnEnable()
    {
        hintLvl = serializedObject.FindProperty("hintLvl");
        disabled = serializedObject.FindProperty("disabled");
        hintBtn = serializedObject.FindProperty("hintBtn");
        hintBtnDefault = serializedObject.FindProperty("hintBtnDefault");
        hintBtnAlt = serializedObject.FindProperty("hintBtnAlt");
        img = serializedObject.FindProperty("img");
        textBox = serializedObject.FindProperty("textBox");
        prevBtn = serializedObject.FindProperty("prevBtn");
        nextBtn = serializedObject.FindProperty("nextBtn");
        doneBtn = serializedObject.FindProperty("doneBtn");
        pics = serializedObject.FindProperty("pics");
        texts = serializedObject.FindProperty("texts");
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();
        EditorGUILayout.PropertyField(hintLvl);
        EditorGUILayout.PropertyField(disabled);
        EditorGUILayout.PropertyField(hintBtn);
        EditorGUILayout.PropertyField(hintBtnDefault);
        EditorGUILayout.PropertyField(hintBtnAlt);
        EditorGUILayout.PropertyField(img);
        EditorGUILayout.PropertyField(textBox);
        EditorGUILayout.PropertyField(prevBtn);
        EditorGUILayout.PropertyField(nextBtn);
        EditorGUILayout.PropertyField(doneBtn);
        EditorGUILayout.PropertyField(pics);
        EditorGUILayout.PropertyField(texts);
        serializedObject.ApplyModifiedProperties();


        if (EditorApplication.isPlaying)
            DrawDefaultInspector();

        HintSystem script = (HintSystem)target;
        if (GUILayout.Button("Reset Hint Lvl"))
        {
            script.resetHintLvl();
        }
    }
}