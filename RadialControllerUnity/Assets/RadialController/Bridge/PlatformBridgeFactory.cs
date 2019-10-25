namespace RadialController {
    public static class PlatformBridgeFactory {
        public static IRadialControllerPlatformBridge CreateBridge() {
            #if UNITY_STANDALONE_WIN
            return new RadialControllerWindowsBridge();
            #else
            return null;
            #endif
        }
    }
}