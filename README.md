## Download package
For Unity 2017.4 and later: [UnityRawInput.unitypackage](https://github.com/Elringus/UnityRawInput/releases/download/v1.0/UnityRawInput.unitypackage).

## Description
Wrapper over [Windows Raw Input API](https://msdn.microsoft.com/en-us/library/windows/desktop/ms645536(v=vs.85).aspx) to hook for the native input events.
Allows to receive input events even when the Unity application is in background (not in focus).
Will only work on Windows platform.

## Usage
Include package namespace.
```csharp
using UnityRawInput;
```
Initialize the input service to start processing native input messages. 
```csharp
RawKeyInput.Start();
```
Optinally, you can specify whether input messages should be handled when the application is not in focus (disabled by default).
```csharp
var workInBackground = true;
RawKeyInput.Start(workInBackground);
```
Add listeners for the input events.
```csharp
RawKeyInput.OnKeyUp += HandleKeyUp;
RawKeyInput.OnKeyDown += HandleKeyDown;

private void HandleKeyUp (RawKey key) { ... }
private void HandleKeyDown (RawKey key) { ... }
```
You can also check whether specific key is currently pressed.
```csharp
if (RawKeyInput.IsKeyDown(key)) { ... }
```
You can stop the service at any time.
```csharp
RawKeyInput.Stop();
```
Don't forget to remove listeners when you no longer need them.
```csharp
private void OnDisable ()
{
    RawKeyInput.OnKeyUp -= HandleKeyUp;
    RawKeyInput.OnKeyDown -= HandleKeyDown;
}
```
