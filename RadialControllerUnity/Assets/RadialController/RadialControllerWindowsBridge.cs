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

        private Process _serverProc;
        public LocalUdpClient localUdpClient;

        public RadialControllerWindowsBridge() {
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
        }

        public void Update() {
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