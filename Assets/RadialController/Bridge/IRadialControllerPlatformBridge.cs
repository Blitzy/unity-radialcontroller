namespace RadialController {
    /// <summary>
    /// Common interface that all radial controller platform bridges implement.
    /// </summary>
    public interface IRadialControllerPlatformBridge {
        
        /// <summary>
        /// Regular frame update from unity, managed by the radial controller component that creates this platform bridge.
        /// </summary>
        void Update();
        
        /// <summary>
        /// Tell the platform bridge to dispose of itself.
        /// The bridge should cleanup any objects and unmanaged hooks it has created.
        /// </summary>
        void Dispose();
    }
}