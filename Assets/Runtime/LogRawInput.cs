using UnityEngine;
using UnityRawInput;

public class LogRawInput : MonoBehaviour
{
    public bool WorkInBackground;
    public bool InterceptMessages;

    private void OnEnable ()
    {
        RawInput.WorkInBackground = WorkInBackground;
        RawInput.InterceptMessages = InterceptMessages;

        RawInput.OnKeyUp += LogKeyUp;
        RawInput.OnKeyDown += LogKeyDown;
        RawInput.OnKeyDown += DisableIntercept;

        RawInput.Start();
    }

    private void OnDisable ()
    {
        RawInput.Stop();

        RawInput.OnKeyUp -= LogKeyUp;
        RawInput.OnKeyDown -= LogKeyDown;
        RawInput.OnKeyDown -= DisableIntercept;
    }

    private void OnValidate ()
    {
        // Apply options when toggles are clicked in editor.
        // OnValidate is invoked only in the editor (won't affect build).
        RawInput.InterceptMessages = InterceptMessages;
        RawInput.WorkInBackground = WorkInBackground;
    }

    private void LogKeyUp (RawKey key)
    {
        Debug.Log("Key Up: " + key);
    }

    private void LogKeyDown (RawKey key)
    {
        Debug.Log("Key Down: " + key);
    }

    private void DisableIntercept (RawKey key)
    {
        if (RawInput.InterceptMessages && key == RawKey.Escape)
            RawInput.InterceptMessages = InterceptMessages = false;
    }
}
