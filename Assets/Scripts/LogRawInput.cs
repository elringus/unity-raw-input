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

    private GUIStyle style;
    private void OnGUI()
    {
        // Ensure there's a camera to render to, create dummy camera otherwise
        if (Camera.main == null)
        {
            var obj = new GameObject("Camera") { hideFlags = HideFlags.HideAndDontSave, tag = "MainCamera" };
            var cam = obj.AddComponent<Camera>();
            cam.clearFlags = CameraClearFlags.SolidColor;
            cam.backgroundColor = Color.black;
        }

        GUILayout.Label(RawInput.ToString(), style ?? (style = new GUIStyle(GUI.skin.label) { fontSize = 32 }), GUILayout.Width(Screen.width), GUILayout.Height(Screen.height));
    }

    private void DrawBox(int row)
    {
        GUI.Box(new Rect(GUIUtility.ScreenToGUIPoint(Vector2.zero) + (Vector2.up * 50f * row), new Vector2(500f, 50f)), GUIContent.none);
    }
}
