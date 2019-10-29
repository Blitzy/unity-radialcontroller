namespace RadialController {
    public static class PlatformBridgeFactory {
        public static IRadialControllerPlatformBridge CreateBridge(RadialController radialController) {
            #if UNITY_STANDALONE_WIN
            return new RadialControllerWindowsBridge(radialController);
            #else
            return null;
            #endif
        }
    }
}