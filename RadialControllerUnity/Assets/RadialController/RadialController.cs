using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace RadialController {
    public class RadialController : MonoBehaviour {
        [Tooltip("Default behaviour of the radial controller has the clicked event being sent even if the holding is activated. This flag allows you to alter that behaviour.")]
        public bool sendClickIfHolding = true;
        public float rotationResolutionInDegrees = 10.0f;
        public bool useAutoHapticFeedback = true;

        public UnityEvent onButtonClicked;
        public UnityEvent onButtonPressed;
        public UnityEvent onButtonReleased;
        public UnityEvent onButtonHolding;
        public FloatEvent onRotationChanged;
        public UnityEvent onControlAcquired;
        public UnityEvent onControlLost;

        private float _prevRotationResolutionInDegrees;
        private bool _prevUseAutoHapticFeedback;

        private IRadialControllerPlatformBridge _bridge;
        private bool _allowClickEvent = true;

        private void Awake() {
        }

        private void OnEnable() {
            enabled = true;
            _bridge = PlatformBridgeFactory.CreateBridge(this);

            if (_bridge != null) {
                Debug.LogFormat("[RadialController] Created platform bridge {0} {1}.", _bridge.Name, _bridge.Version);

                _bridge.onBridgeReady += OnBridgeReady;
                _bridge.onButtonClicked += OnButtonClicked;
                _bridge.onButtonPressed += OnButtonPressed;
                _bridge.onButtonReleased += OnButtonReleased;
                _bridge.onButtonHolding += OnButtonHolding;
                _bridge.onRotationChanged += OnRotationChanged;
                _bridge.onControlAcquired += OnControlAcquired;
                _bridge.onControlLost += OnControlLost;

                CheckRotationResolutionInDegrees(true);
                CheckUseAutoHapticFeedback(true);
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
                CheckRotationResolutionInDegrees();
                CheckUseAutoHapticFeedback();

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
            if (!sendClickIfHolding) {
                // If 'send click if holding' is disabled and the holding event was received, do not send out the click event.
                _allowClickEvent = false;
            }

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
            // Reset the allow click event flag every time the Pressed event is received.
            _allowClickEvent = true;

            if (onButtonPressed != null) {
                onButtonPressed.Invoke();
            }
        }

        private void OnBridgeReady() {
            // Once the bridge is ready, send this components current settings to it to sync up.
            if (_bridge != null) {
                _bridge.SetRotationResolutionInDegrees(rotationResolutionInDegrees);
                _bridge.SetUseAutoHapticFeedback(useAutoHapticFeedback);
            }
        }

        private void OnButtonClicked() {
            if (_allowClickEvent) { 
                if (onButtonClicked != null) {
                    onButtonClicked.Invoke();
                }
            }
        }

        private void OnApplicationFocus(bool focused) {
            if (_bridge != null) {
                _bridge.OnApplicationFocus(focused);
            }
        }

        private void OnApplicationPause(bool paused) {
            if (_bridge != null) {
                _bridge.OnApplicationPause(paused);
            }
        }

        private void CheckRotationResolutionInDegrees(bool force = false) {
            bool hasChanged = _prevRotationResolutionInDegrees != rotationResolutionInDegrees;
            if (hasChanged || force) {
                if (_bridge != null) {
                    _bridge.SetRotationResolutionInDegrees((double)rotationResolutionInDegrees);
                }
                _prevRotationResolutionInDegrees = rotationResolutionInDegrees;
            }
        }

        private void CheckUseAutoHapticFeedback(bool force = false) {
            bool hasChanged = _prevUseAutoHapticFeedback != useAutoHapticFeedback;
            if (hasChanged || force) {
                if (_bridge != null) {
                    _bridge.SetUseAutoHapticFeedback(useAutoHapticFeedback);
                }
                _prevUseAutoHapticFeedback = useAutoHapticFeedback;
            }
        }
    }

    [System.Serializable]
    public class FloatEvent : UnityEvent<float> {
    }

}