﻿using UnityEditor;
using UnityRawInput;

[CustomEditor(typeof(LogRawInput))]
public class LogRawInputEditor : Editor
{
    public override void OnInspectorGUI ()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Pressed Keys", GetKeys());
        EditorGUILayout.HelpBox("Press Esc to disable intercept in play mode.", MessageType.Info);
    }

    private void OnEnable ()
    {
        RawInput.OnKeyDown += HandleKeyEvent;
        RawInput.OnKeyUp += HandleKeyEvent;
    }

    private void OnDisable ()
    {
        RawInput.OnKeyDown -= HandleKeyEvent;
        RawInput.OnKeyUp -= HandleKeyEvent;
    }

    private void HandleKeyEvent (RawKey _)
    {
        Repaint();
    }

    private string GetKeys ()
    {
        return string.Join(" + ", RawInput.PressedKeys);
    }
}
