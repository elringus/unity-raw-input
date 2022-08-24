## Description

Wrapper over [Windows Raw Input API](https://msdn.microsoft.com/en-us/library/windows/desktop/ms645536(v=vs.85).aspx) to hook for the native input events. Allows receiving input events even when the Unity application is in background (not in focus).

Will only work on Windows platform.

Only keyboard input is currently supported. 

## Installation

Download and import the package: [UnityRawInput.unitypackage](https://github.com/Elringus/UnityRawInput/raw/master/UnityRawInput.unitypackage).

Be aware that you don't need to clone the whole repository in order to use the extension in your project. Either download package from the link above or extract `Assets/UnityRawInput` folder from the repository project – it contains all the required assets.

## Usage

Include package namespace.

```csharp
using UnityRawInput;
```

Initialize the input service to start processing native input messages. 

```csharp
RawKeyInput.Start();
```

Optionally, you can specify whether input messages should be handled when the application is not in focus (disabled by default).

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
