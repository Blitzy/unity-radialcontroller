#if UNITY_STANDALONE_WIN

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace Blitzy.UnityRadialController {
    public class RadialControllerWindowsBridge : IRadialControllerPlatformBridge
    {
        #if UNITY_EDITOR
        /// <summary>
        /// Show the debug server process window when running in Unity Editor?
        /// If true, the server process will start minimized in the taskbar. If false, the server process will be hidden.
        /// 
        /// NOTE: If the server process window is shown, Unity needs to restart the server process every time it regains focus in order for the Radial Controller to function.
        /// This is technically a feature of the Radial Controller API in that it only works on windows that are in focus, and when showing the server process window it gains focus even
        /// when minimized.
        /// </summary>
        public static bool EditorDebug_ShowServerWindow = false;
        #endif

        public const int Port = 27020;

        private const string Server_SenderId = "RadialControllerServer";

        private const string EventId_ServerReady = "server_ready";
        private const string EventId_ControlAcquired = "radial_controller_control_acquired";
        private const string EventId_ControlLost = "radial_controller_control_lost";
        private const string EventId_ButtonClicked = "radial_controller_button_clicked";
        private const string EventId_ButtonPressed = "radial_controller_button_pressed";
        private const string EventId_ButtonHolding = "radial_controller_button_holding";
        private const string EventId_ButtonReleased = "radial_controller_button_released";
        private const string EventId_RotationChanged = "radial_controller_rotation_changed";
        private const string EventId_RotationResolutionInDegrees = "radial_controller_rotation_resolution_in_degrees";
        private const string EventId_AutoHapticFeedback = "radial_controller_auto_haptic_feedback";

        private Process _serverProc;
        private Queue<LocalUdpPacket> _packetQueue;
        private LocalUdpClient _localUdpClient;
        private RadialController _radialController;

        public string Name { get { return "Radial Controller Windows Bridge"; } } 

        public string Version { get { return "0.2.2"; } }

        public event Action onBridgeReady;
        public event Action onButtonClicked;
        public event Action onButtonPressed;
        public event Action onButtonReleased;
        public event Action onButtonHolding;
        public event Action<float> onRotationChanged;
        public event Action onControlAcquired;
        public event Action onControlLost;

        public RadialControllerWindowsBridge(RadialController radialController) {
            _radialController = radialController;
            _packetQueue = new Queue<LocalUdpPacket>();

            _localUdpClient = new LocalUdpClient("RadialControllerUnityReceiver", Port);
            _localUdpClient.ignoreDataFromClient = true;
            _localUdpClient.onDataReceived += OnDataReceived;

            StartServerProcess();
        }

        private void StartServerProcess() {
            UnityEngine.Debug.Log("[RadialControllerWindowsBridge] Starting server process");
            var runningProcesses = Process.GetProcessesByName("RadialControllerServer");

            if (runningProcesses == null || runningProcesses.Length == 0) {
                // Start the radial controller process.
                var startInfo = new System.Diagnostics.ProcessStartInfo();
                var exePath = System.IO.Path.Combine(Application.streamingAssetsPath, "RadialControllerServer.exe");
                startInfo.FileName = exePath;

                #if UNITY_EDITOR
                startInfo.WindowStyle = EditorDebug_ShowServerWindow ? ProcessWindowStyle.Minimized : ProcessWindowStyle.Hidden;
                #else
                startInfo.WindowStyle = ProcessWindowStyle.Hidden;
                #endif

                _serverProc = Process.Start(startInfo);
            } else {
                // Use one of the already running radial controller processes.
                _serverProc = runningProcesses[0];
            }
        }

        private void KillServerProcess() {
            if (_serverProc != null) {
                try {
                    _serverProc.Kill();
                    _serverProc.WaitForExit();
                } catch {
                    UnityEngine.Debug.Log("[RadialControllerWindowsBridge] Tried killing server process but it was already killed.");
                }
            }
        }

        private void OnDataReceived(LocalUdpPacket packet) {
            // string msg = "Data received from " + packet.senderId + ":\n";
            // msg += "  [data]: " + MiniJSON.Json.Serialize(packet.data);
            // UnityEngine.Debug.Log(msg);

            if (packet.senderId == Server_SenderId) {
                // Queue the packet up to be process on the main thread in the next update frame.
                _packetQueue.Enqueue(packet);
            }
        }

        public void Update() {
            if (_packetQueue.Count > 0) {
                while(_packetQueue.Count > 0) {
                    LocalUdpPacket packet = _packetQueue.Dequeue();

                    // Process the packet's event data.
                    string eventId = null;

                    if (packet.data.ContainsKey("event_id")) {
                        eventId = packet.data["event_id"] as string;
                    }

                    if (eventId != null) {
                        switch(eventId) {
                            case EventId_ServerReady:
                                if (onBridgeReady != null) {
                                    onBridgeReady();
                                }
                                break;
                            case EventId_ControlAcquired:
                                if (onControlAcquired != null) {
                                    onControlAcquired();
                                }
                                break;
                            case EventId_ControlLost:
                                if (onControlLost != null) {
                                    onControlLost();
                                }
                                break;
                            case EventId_RotationChanged: 
                                double deltaDegrees = System.Convert.ToDouble(packet.data["delta_degrees"]);
                                if (onRotationChanged != null) {
                                    onRotationChanged((float)deltaDegrees);
                                }
                                break;
                            case EventId_ButtonPressed:
                                if (onButtonPressed != null) {
                                    onButtonPressed();
                                }
                                break;
                            case EventId_ButtonHolding:
                                if (onButtonHolding != null) {
                                    onButtonHolding();
                                }
                                break;
                            case EventId_ButtonReleased:
                                if (onButtonReleased != null) {
                                    onButtonReleased();
                                }
                                break;
                            case EventId_ButtonClicked:
                                if (onButtonClicked != null) {
                                    onButtonClicked();
                                }
                                break;
                            default:
                                UnityEngine.Debug.LogWarning("[RadialControllerWindowsBridge] Event Id " + eventId + " is not implemented.");
                                break;
                        }
                    } else {
                        // This is not a normal radial controller event from the server. Lets just throw this one to the Unity debug console.
                        string msg = "Extra data received from " + packet.senderId + ":\n";
                        msg += "  [data]: " + MiniJSON.Json.Serialize(packet.data);
                        UnityEngine.Debug.Log(msg);
                    }
                }
            }
        }

        public void OnApplicationFocus(bool focused) {
            if (focused) {
                if (_serverProc != null) {
                    // Unity application has gained focus, this causes the radial controller server application to stop
                    // receiving events from Windows. Restart the server process to get radial controller input working again.

                    // TODO: It would be much better to give the already running process focus again, but this will be
                    // the quick and dirty workaround for now.
                    KillServerProcess();
                }
                StartServerProcess();
            }
        }

        public void OnApplicationPause(bool paused){
        }

        public void SetRotationResolutionInDegrees(double degrees) {
            var data = new Dictionary<string, object>();
            data["event_id"] = EventId_RotationResolutionInDegrees;
            data["degrees"] = degrees;

            _localUdpClient.Send(data);
        }

        public void SetUseAutoHapticFeedback(bool useAutoHapticFeedback) {
            var data = new Dictionary<string, object>();
            data["event_id"] = EventId_AutoHapticFeedback;
            data["use_auto_haptic_feedback"] = useAutoHapticFeedback;

            _localUdpClient.Send(data);
        }

        public void Dispose() {
            KillServerProcess();

            if (_localUdpClient != null){
                _localUdpClient.onDataReceived -= OnDataReceived;
                _localUdpClient.Dispose();
            }
        }
    }
}

#endif