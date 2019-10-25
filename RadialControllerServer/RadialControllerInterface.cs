using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using Windows.UI.Input;
using Windows.UI.Input.Core;
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.Foundation;
using Windows.Foundation.Metadata;
using Windows.System;

namespace RadialControllerWinForm
{
    public class RadialControllerInterface : IDisposable
    {
        private IntPtr windowHandle;
        private RadialController radialController;

        public event Action onButtonClicked;
        public event Action onButtonPressed;
        public event Action onButtonReleased;
        public event Action onButtonHolding;
        public event Action<double> onRotationChanged;
        public event Action onControlAcquired;
        public event Action onControlLost;

        public RadialControllerInterface(IntPtr windowHandle)
        {
            this.windowHandle = windowHandle;

            CreateController();
            SubscribeToControllerCallbacks();
            MenuSuppressed(true);
        }

        private void CreateController()
        {
            Console.WriteLine("[RadialControllerInterface] Creating radial controller");
            IRadialControllerInterop interop = (IRadialControllerInterop)System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMarshal.GetActivationFactory(typeof(RadialController));
            Guid guid = typeof(RadialController).GetInterface("IRadialController").GUID;
            radialController = interop.CreateForWindow(this.windowHandle, ref guid);
        }

        private void SubscribeToControllerCallbacks()
        {
            Console.WriteLine("[RadialControllerInterface] Subscribing to radial controller callbacks");
            radialController.ControlLost += RadialController_ControlLost;
            radialController.ControlAcquired += RadialController_ControlAcquired;
            radialController.ButtonPressed += RadialController_ButtonPressed;
            radialController.ButtonReleased += RadialController_ButtonReleased;
            radialController.ButtonHolding += RadialController_ButtonHolding;
            radialController.ButtonClicked += RadialController_ButtonClicked;
            radialController.RotationChanged += RadialController_RotationChanged;
        }

        private void UnsubscribeToControllerCallbacks()
        {
            Console.WriteLine("[RadialControllerInterface] Unsubscribing from radial controller callbacks");
            radialController.ControlLost -= RadialController_ControlLost;
            radialController.ControlAcquired -= RadialController_ControlAcquired;
            radialController.ButtonPressed -= RadialController_ButtonPressed;
            radialController.ButtonReleased -= RadialController_ButtonReleased;
            radialController.ButtonHolding -= RadialController_ButtonHolding;
            radialController.ButtonClicked -= RadialController_ButtonClicked;
            radialController.RotationChanged -= RadialController_RotationChanged;
        }

        private void RadialController_ControlAcquired(RadialController sender, RadialControllerControlAcquiredEventArgs args)
        {
            Console.WriteLine("[RadialControllerInterface] control acquired");
            if (onControlAcquired != null)
                onControlAcquired();
        }

        private void RadialController_ControlLost(RadialController sender, object args)
        {
            Console.WriteLine("[RadialControllerInterface] control lost");
            if (onControlLost != null)
                onControlLost();
        }

        private void RadialController_RotationChanged(RadialController sender, RadialControllerRotationChangedEventArgs args)
        {
            Console.WriteLine("[RadialControllerInterface] rotation changed: " + args.RotationDeltaInDegrees);
            if (onRotationChanged != null)
                onRotationChanged(args.RotationDeltaInDegrees);
        }
        private void RadialController_ButtonPressed(RadialController sender, RadialControllerButtonPressedEventArgs args)
        {
            Console.WriteLine("[RadialControllerInterface] button pressed");
            if (onButtonPressed != null)
                onButtonPressed();
        }

        private void RadialController_ButtonReleased(RadialController sender, RadialControllerButtonReleasedEventArgs args)
        {
            Console.WriteLine("[RadialControllerInterface] button released");
            if (onButtonReleased != null)
                onButtonReleased();
        }

        private void RadialController_ButtonHolding(RadialController sender, RadialControllerButtonHoldingEventArgs args)
        {
            Console.WriteLine("[RadialControllerInterface] button holding");
            if (onButtonHolding != null)
                onButtonHolding();
        }

        private void RadialController_ButtonClicked(RadialController sender, RadialControllerButtonClickedEventArgs args)
        {
            Console.WriteLine("[RadialControllerInterface] button clicked");
            if (onButtonClicked != null)
                onButtonClicked();
        }

        private RadialControllerConfiguration GetConfig()
        {
            RadialControllerConfiguration radialControllerConfig;
            IRadialControllerConfigurationInterop radialControllerConfigInterop = (IRadialControllerConfigurationInterop)System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMarshal.GetActivationFactory(typeof(RadialControllerConfiguration));
            Guid guid = typeof(RadialControllerConfiguration).GetInterface("IRadialControllerConfiguration").GUID;

            radialControllerConfig = radialControllerConfigInterop.GetForWindow(this.windowHandle, ref guid);
            return radialControllerConfig;
        }

        private void MenuSuppressed(bool suppressed)
        {
            Console.WriteLine("[RadialControllerInterface] menu suppressed: " + suppressed);
            var config = GetConfig();
            config.ActiveControllerWhenMenuIsSuppressed = radialController;
            config.IsMenuSuppressed = suppressed;
        }

        public void Dispose()
        {
            UnsubscribeToControllerCallbacks();
        }
    }
}
