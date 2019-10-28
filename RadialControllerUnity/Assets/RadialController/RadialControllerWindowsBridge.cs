#if UNITY_STANDALONE_WIN

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net;
using System.Net.Sockets;
using System.Text;
using UnityEngine;

namespace RadialController {
    public class RadialControllerWindowsBridge : IRadialControllerPlatformBridge
    {
        public const int Port = 27020;

        private const string Server_SenderId = "RadialControllerServer";
        private const string EventId_ControlAcquired = "radial_controller_control_acquired";
        private const string EventId_ControlLost = "radial_controller_control_lost";
        private const string EventId_ButtonClicked = "radial_controller_button_clicked";
        private const string EventId_ButtonPressed = "radial_controller_button_pressed";
        private const string EventId_ButtonHolding = "radial_controller_button_holding";
        private const string EventId_ButtonReleased = "radial_controller_button_released";
        private const string EventId_RotationChanged = "radial_controller_rotation_changed";

        private Process _serverProc;
        private Queue<LocalUdpPacket> _packetQueue;
        public LocalUdpClient localUdpClient;

        public event Action onButtonClicked;
        public event Action onButtonPressed;
        public event Action onButtonReleased;
        public event Action onButtonHolding;
        public event Action<double> onRotationChanged;
        public event Action onControlAcquired;
        public event Action onControlLost;

        public RadialControllerWindowsBridge() {
            _packetQueue = new Queue<LocalUdpPacket>();

            StartServerProcess();
            localUdpClient = new LocalUdpClient("RadialControllerUnityReciever", Port);
            localUdpClient.ignoreDataFromClient = true;
            localUdpClient.onDataReceived += OnDataReceived;
        }

        private void StartServerProcess() {
            var runningProcesses = Process.GetProcessesByName("RadialControllerServer");

            if (runningProcesses == null || runningProcesses.Length == 0) {
                // Start the radial controller process.
                var startInfo = new System.Diagnostics.ProcessStartInfo();
                var exePath = System.IO.Path.Combine(Application.streamingAssetsPath, "RadialControllerServer.exe");
                startInfo.FileName = exePath;
                startInfo.WindowStyle = ProcessWindowStyle.Minimized;

                _serverProc = Process.Start(startInfo);
            } else {
                // Use one of the already running radial controller processes.
                _serverProc = runningProcesses[0];
            }
        }

        private void OnDataReceived(LocalUdpPacket packet) {
            string msg = "Data received from " + packet.senderId + ":\n";
            msg += "  [data]: " + MiniJSON.Json.Serialize(packet.data);
            UnityEngine.Debug.Log(msg);

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
                    string eventId = (string)packet.data["event_id"];
                    switch(eventId) {
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
                                onRotationChanged(deltaDegrees);
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
                }
            }
        }

        public void Dispose() {
            if (_serverProc != null) {
                _serverProc.Kill();
            }

            if (localUdpClient != null){
                localUdpClient.onDataReceived -= OnDataReceived;
                localUdpClient.Dispose();
            }
        }
    }
}

#endif