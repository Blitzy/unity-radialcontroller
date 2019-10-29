# Radial Controller for Unity

# Changelog

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