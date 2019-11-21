# Radial Controller for Unity

# Changelog

## v0.2.2

### Date: 11/21/2019

### Changes
- Bug Fixes
    - Fixed UnityEvents on `RadialController` being null if instanced at runtime.
    - Fixed `RadialControllerWindowsBridge.StartServerProcess` not being invoked if the `RadialController` component is instanced at runtime.

## v0.2.1

### Date: 11/20/2019

### Changes
- Bug Fixes
    - Changed `RadialController` namespace to `Blitzy.UnityRadialController`. This fixes the same name conflict between the `RadialController` namespace and the `RadialController` class.

## v0.2.0

### Date: 11/20/2019

### Changes

- Improvements
    - Cleaned up project structure, moved all radial controller library and demo files underneath the RadialController folder.
    - Added `Name` and `Version` properties to `IRadialControllerPlatormBridge`. These are printed to the console when the bridge is created.
    - `RadialControllerWindowsBridge` server process now runs hidden.
        - Making the server process run hidden by default solves the Unity/Server process focus fighting problem. Running the process hidden, Windows seem to allow the server process to continue capturing the Radial Controller input while Unity still technically has focus.
        - If you want to see the server window while running in the Unity editor, you can set the `RadialControllerWindowsBridge.EditorDebug_ShowServerWindow` to `true`.
- Bug Fixes
    - Reset demo Radial Controller settings to defaults.

## v0.1.1

### Date: 10/28/2019

### Changes

- Improvements
    - Simplified code base by removing `RadialControllerInterface` abstraction and moving all core functionality to the `Form1` class.
    - Server now sends emits a "Server Ready" event.
    - Can now set these Radial Controller options from the Client:
        - `rotationResolutionInDegrees` - How far the radial must travel before emitting the onRotationChanged event.
        - `useAutoHapticFeedback` - Enable/Disable the default automatic haptic feedback behaviour of the Radial Controller.

## v0.1.0

### Date: 10/28/2019

### Changes

- Features
    - Server application launched and managed by `RadialController` component that pipes events for Radial Controller to Unity through Socket.
    - If Unity regains focus, it will automatically kill and relaunch the Radial Controller server application so that the server can continue to receive radial events from Windows and pipe them to Unity.
    - Core Radial Controller input events are implemented:
        - `onButtonClicked`
        - `onButtonPressed`
        - `onButtonReleased`
        - `onButtonHolding`
        - `onRotationChanged`
        - `onControlAcquired`
        - `onControlLost`
    - Created Unity scene called "Demo" that shows an example of how to use the `RadialController` component to interact with a Unity object.