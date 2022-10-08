using UnityEngine;
using UnityRawInput;

public class LogRawInput : MonoBehaviour
{
    public bool WorkInBackground;
    public bool InterceptMessages;
    public RawKey DisableInterceptKey = RawKey.Escape;

    private void OnEnable()
    {
        RawInput.Start(WorkInBackground);
        RawInput.OnKeyUp += LogKeyUp;
        RawInput.OnKeyDown += LogKeyDown;
        RawInput.OnKeyDown += DisableIntercept;
    }

    private void OnDisable()
    {
        RawInput.Stop();
        RawInput.OnKeyUp -= LogKeyUp;
        RawInput.OnKeyDown -= LogKeyDown;
        RawInput.OnKeyDown -= DisableIntercept;
    }

    private void OnValidate()
    {
        // Used for testing purposes, won't work in build.
        // OnValidate is invoked only in the editor.
        RawInput.InterceptMessages = InterceptMessages;
    }

    private void LogKeyUp(RawKey key)
    {
        Debug.Log("Key Up: " + key);
    }

    private void LogKeyDown(RawKey key)
    {
        Debug.Log("Key Down: " + key);
    }

    private void DisableIntercept(RawKey key)
    {
        if (RawInput.InterceptMessages && key == DisableInterceptKey)
            RawInput.InterceptMessages = InterceptMessages = false;
    }
}

#if UNITY_EDITOR
[UnityEditor.CustomEditor(typeof(LogRawInput))]
public class LogRawInputEditor : UnityEditor.Editor
{
    public override bool RequiresConstantRepaint() => true;
    public override void OnInspectorGUI()
    {
        if (RawInput.IsRunning)
            UnityEditor.EditorGUILayout.LabelField(RawInput.ToString());
        base.OnInspectorGUI();
    }
}
#endif
