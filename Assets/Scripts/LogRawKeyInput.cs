using UnityEngine;
using UnityRawInput;

public class LogRawKeyInput : MonoBehaviour
{
    public bool WorkInBackground;
    public bool InterceptMessages;

    private void OnEnable ()
    {
        RawKeyInput.Start(WorkInBackground);
        RawKeyInput.OnKeyUp += LogKeyUp;
        RawKeyInput.OnKeyDown += LogKeyDown;
    }

    private void OnDisable ()
    {
        RawKeyInput.Stop();
        RawKeyInput.OnKeyUp -= LogKeyUp;
        RawKeyInput.OnKeyDown -= LogKeyDown;
    }

    private void OnValidate ()
    {
        // Used for testing purposes, won't work in builds.
        // OnValidate is invoked only in the editor.
        RawKeyInput.InterceptMessages = InterceptMessages;
    }

    private void LogKeyUp (RawKey key)
    {
        Debug.Log("Key Up: " + key);
    }

    private void LogKeyDown (RawKey key)
    {
        Debug.Log("Key Down: " + key);
    }
}
