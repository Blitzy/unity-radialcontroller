using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RadialController {
    public class RadialController : MonoBehaviour {

        private IRadialControllerPlatformBridge _bridge;

        public UnityEvent onButtonClicked;
        public UnityEvent onButtonPressed;
        public UnityEvent onButtonReleased;
        public UnityEvent onButtonHolding;
        public FloatEvent onRotationChanged;
        public UnityEvent onControlAcquired;
        public UnityEvent onControlLost;

        private void OnEnable() {
            _bridge = PlatformBridgeFactory.CreateBridge();

            if (_bridge != null) {
                Debug.LogFormat("[RadialController] Created platform bridge {0}.", _bridge.GetType().Name);

                _bridge.onButtonClicked += OnButtonClicked;
                _bridge.onButtonPressed += OnButtonPressed;
                _bridge.onButtonReleased += OnButtonReleased;
                _bridge.onButtonHolding += OnButtonHolding;
                _bridge.onRotationChanged += OnRotationChanged;
                _bridge.onControlAcquired += OnControlAcquired;
                _bridge.onControlLost += OnControlLost;
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

        private void OnControlLost() {
            if (onControlLost != null) {
                onControlLost.Invoke();
            }
        }

        private void OnControlAcquired() {
            if (onControlAcquired != null) {
                onControlAcquired.Invoke();
            }
        }

        private void OnRotationChanged(float deltaDegrees) {
            if (onRotationChanged != null) {
                onRotationChanged.Invoke(deltaDegrees);
            }
        }

        private void OnButtonHolding() {
            if (onButtonHolding != null) {
                onButtonHolding.Invoke();
            }
        }

        private void OnButtonReleased() {
            if (onButtonReleased != null) {
                onButtonReleased.Invoke();
            }
        }

        private void OnButtonPressed() {
            if (onButtonPressed != null) {
                onButtonPressed.Invoke();
            }
        }

        private void OnButtonClicked() {
            if (onButtonClicked != null) {
                onButtonClicked.Invoke();
            }
        }

        public void SendTestMessage() {
            var windowsBridge = (RadialControllerWindowsBridge)_bridge;

            var data = new Dictionary<string, object>();
            data["message"] = "hello from unity!";
            data["time"] = System.DateTime.Now.ToString();

            windowsBridge.localUdpClient.Send(data);
        }

        private static IRadialControllerPlatformBridge CreatePlatformBridge() {
            return null;
        }
    }

    [System.Serializable]
    public class FloatEvent : UnityEvent<float> {
    }

}