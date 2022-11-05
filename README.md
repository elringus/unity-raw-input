## Description

Wrapper over [Windows Raw Input API](https://msdn.microsoft.com/en-us/library/windows/desktop/ms645536(v=vs.85).aspx) to hook for the native input events. Allows receiving input events even when the Unity application is in background (not in focus).

Will only work on Windows platform.

Only keyboard and mouse input is detected.

## Installation

Download and import the package: [UnityRawInput.unitypackage](https://github.com/Elringus/UnityRawInput/raw/master/UnityRawInput.unitypackage).

## Usage

Enable `Run In Background` in project's player settings; if not enabled, expect severe mouse slowdown when the application is not in focus https://github.com/Elringus/UnityRawInput/issues/19#issuecomment-1227462101.

![](https://i.gyazo.com/9737f66dafa9c705601521b82f40fc5a.png)

Initialize the service to start processing input messages:

```csharp
RawInput.Start();
```

Optionally, configure whether input messages should be handled when the application is not in focus and whether handled messages should be intercepted (both disabled by default):

```csharp
RawInput.WorkInBackground = true;
RawInput.InterceptMessages = false;
```

Add listeners to handle input events:

```csharp
RawInput.OnKeyUp += HandleKeyUp;
RawInput.OnKeyDown += HandleKeyDown;

private void HandleKeyUp (RawKey key) { ... }
private void HandleKeyDown (RawKey key) { ... }
```

Check whether specific key is currently pressed:

```csharp
if (RawInput.IsKeyDown(key)) { ... }
```

Stop the service:

```csharp
RawInput.Stop();
```

Don't forget to remove listeners when you no longer need them:

```csharp
private void OnDisable ()
{
    RawInput.OnKeyUp -= HandleKeyUp;
    RawInput.OnKeyDown -= HandleKeyDown;
}
```

Find usage example in the project: https://github.com/Elringus/UnityRawInput/blob/master/Assets/Runtime/LogRawInput.cs.

List of the raw keys with descriptions: https://docs.microsoft.com/en-us/windows/win32/inputdev/virtual-key-codes.
