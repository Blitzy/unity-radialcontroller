using System;

namespace RadialController {
    /// <summary>
    /// Common interface that all radial controller platform bridges implement.
    /// </summary>
    public interface IRadialControllerPlatformBridge {
        event Action onBridgeReady;
        event Action onButtonClicked;
        event Action onButtonPressed;
        event Action onButtonReleased;
        event Action onButtonHolding;
        event Action<float> onRotationChanged;
        event Action onControlAcquired;
        event Action onControlLost;

        /// <summary>
        /// Name of the platform bridge.
        /// </summary>
        string Name { get; }
        
        /// <summary>
        /// Version of the platform bridge.
        /// </summary>
        string Version { get; }
        
        /// <summary>
        /// Regular frame update from unity, managed by the radial controller component that creates this platform bridge.
        /// </summary>
        void Update();
        
        /// <summary>
        /// Tell the platform bridge to dispose of itself.
        /// The bridge should cleanup any objects and unmanaged hooks it has created.
        /// </summary>
        void Dispose();

        /// <summary>
        /// Tell the platform bridge when the application has gained or lost focus.
        /// </summary>
        void OnApplicationFocus(bool focused);

        /// <summary>
        /// Tell the platform bridge when the application has been paused or resumed.
        /// </summary>
        void OnApplicationPause(bool paused);

        /// <summary>
        /// Tell the platform bridge the minimum rotational value required for the RadialController object to fire a RotationChanged event.
        /// </summary>
        void SetRotationResolutionInDegrees(double degrees);

        void SetUseAutoHapticFeedback(bool useAutoHapticFeedback);
    }
}