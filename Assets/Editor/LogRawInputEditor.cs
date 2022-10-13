using UnityEditor;
using UnityRawInput;

[CustomEditor(typeof(LogRawInput))]
public class LogRawInputEditor : Editor
{
    private string text;
    public override void OnInspectorGUI ()
    {
        base.OnInspectorGUI();
        EditorGUILayout.LabelField("Pressed Keys", GetKeys());
        text = EditorGUILayout.TextField(RawKey.FromString(text).ToString(), text);
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
