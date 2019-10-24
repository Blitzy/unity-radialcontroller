#if UNITY_STANDALONE_WIN

using System;
using System.Runtime.InteropServices;
using System.Collections;
using System.Collections.Generic;

namespace RadialController {
    public class RadialControllerWindowsBridge : IRadialControllerPlatformBridge
    {
        public void Update() {
        }

        public void Dispose() {
        }
    }
}

#endif