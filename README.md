# Radial Controller for Unity
This projects enables the use of the [Microsoft Radial Controller](https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Input.RadialController) inside of a non-UWP Unity Windows Desktop application.


[View the Changelog](CHANGELOG.md) for details changes/improvements in each version.

## What is this for?
I needed to get close to native functionality of the [Microsoft Surface Dial](https://docs.microsoft.com/en-us/windows/uwp/design/input/windows-wheel-interactions) working inside of a Unity application that is **NOT** built for [UWP](https://docs.microsoft.com/en-us/windows/uwp/get-started/universal-application-platform-guide). 

The Microsoft Surface Dial API provided by Microsoft is intended for UWP apps but building a UWP app in Unity is laborious and includes lots of unnecessary overhead for the average Unity project. This project's original goal was to provide Surface Dial controls to a Unity Windows Desktop application without the need to make the Unity application a UWP app.

## Quick Start

The `RadialControllerUnity` folder is a functioning example project with all necessary components to get a Radial Controller working in Unity.

To get started using a Radial Controller in your own Unity Project do the following:

1. Copy this folder to your project's Asset folder:
    - `RadialControllerUnity/RadialController`
2. Copy this executable to your project's StreamingAssets folder.
    - `RadialControllerUnity/StreamingAssets/RadialControllerServer.exe`
3. In your Unity scene, attach the `RadialController` component to a GameObject.
4. Hook up event handlers to the various `RadialController` components UnityEvents and do all your Unity game logic from there.

## How does it work
When you play your Unity project, the `RadialController` Unity component will attempt to create a bridge to communicate with the Radial Controller device depending on the platform being run on.

> NOTE: Currently Windows 10 is the only supported platform.

### Windows
1. `RadialControllerWindowsBridge` is created by the `RadialController` component to communicate with the Radial Controller device on Windows. 
2. The windows bridge starts the side-process application `RadialControllerServer.exe`. This is a simple Windows desktop application that registers itself with the Radial Controller through the official Microsoft Windows Radial Controller API for Windows 10. 
3. The server application pipes events received from the official Windows API to Unity through a local socket connection using the .NET `UDPClient`. The `RadialControllerWindowsBridge` receives the events from the server and processes them and turns them into UnityEvents that are easy to use inside of any Unity application.

## Contribute
If you wish to contribute to this project feel free to open up a pull request and I will review it for inclusion. The Unity-side is designed as such that it should be easy to extend its capability to other platforms by extending from `IRadialControllerPlatformBridge` and providing platform specific functionality.

Same goes for bug fixes, if you find any, feel free to open up an Issue here on the GitHub repo or fix it yourself and create a pull request so we may all benefit.

## Third Party Credits
 - [MiniJSON](https://gist.github.com/darktable/1411710) - Calvin Rien ([darktable](https://gist.github.com/darktable/1411710))