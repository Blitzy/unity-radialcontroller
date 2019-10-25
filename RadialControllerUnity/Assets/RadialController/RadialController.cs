using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RadialController {
    public class RadialController : MonoBehaviour {

        private IRadialControllerPlatformBridge _bridge;

        private void OnEnable() {
            _bridge = PlatformBridgeFactory.CreateBridge();

            if (_bridge != null) {
                Debug.LogFormat("[RadialController] Created platform bridge {0}.", _bridge.GetType().Name);
             } else {
                Debug.LogWarningFormat("[RadialController] Current platform does not have a bridge implemented. Radial Controller will not be usable.");
            }
        }

        private void OnDisable() {
            if (_bridge != null) {
                Debug.LogFormat("[RadialController] Disposing of platform bridge {0}", _bridge.GetType().Name);
                _bridge.Dispose();
                _bridge = null;
            }
        }

        private void Update() {
            if (_bridge != null) {
                _bridge.Update();
            }
        }

        public void SendTestMessage() {
            var windowsBridge = (RadialControllerWindowsBridge)_bridge;
            windowsBridge.localUdpClient.SendMessage("hello from unity!");
        }

        private static IRadialControllerPlatformBridge CreatePlatformBridge() {
            return null;
        }
    }
}