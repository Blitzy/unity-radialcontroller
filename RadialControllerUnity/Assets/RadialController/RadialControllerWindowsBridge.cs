#if UNITY_STANDALONE_WIN

using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;

namespace RadialController {
    public class RadialControllerWindowsBridge : IRadialControllerPlatformBridge
    {
        private Process _serverProc;

        public RadialControllerWindowsBridge() {
            var runningProcesses = Process.GetProcessesByName("RadialControllerServer");

            if (runningProcesses == null || runningProcesses.Length == 0) {
                // Start the radial controller process.
                var startInfo = new System.Diagnostics.ProcessStartInfo();
                var exePath = System.IO.Path.Combine(Application.streamingAssetsPath, "RadialControllerServer.exe");
                startInfo.FileName = exePath;

                _serverProc = Process.Start(startInfo);
            } else {
                // Use one of the already running radial controller processes.
                _serverProc = runningProcesses[0];
            }
        }

        public void Update() {
        }

        public void Dispose() {
            if (_serverProc != null) {
                _serverProc.Kill();
            }
        }
    }
}

#endif