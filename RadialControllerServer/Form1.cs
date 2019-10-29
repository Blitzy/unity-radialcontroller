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
using Windows.Storage.Streams;
using Windows.Storage;
using Windows.Foundation;
using Windows.Foundation.Metadata;

namespace RadialControllerWinForm
{
    public partial class Form1 : Form
    {
        public const int Port = 27020;
        public const string VersionString = "v0.1.1 (October 28, 2019)";

        private const string Client_SenderId = "RadialControllerUnityReceiver";
        private const string EventId_ServerReady = "server_ready";
        private const string EventId_ControlAcquired = "radial_controller_control_acquired";
        private const string EventId_ControlLost = "radial_controller_control_lost";
        private const string EventId_ButtonClicked = "radial_controller_button_clicked";
        private const string EventId_ButtonPressed = "radial_controller_button_pressed";
        private const string EventId_ButtonHolding = "radial_controller_button_holding";
        private const string EventId_ButtonReleased = "radial_controller_button_released";
        private const string EventId_RotationChanged = "radial_controller_rotation_changed";
        private const string EventId_RotationResolutionInDegrees = "radial_controller_rotation_resolution_in_degrees";
        private const string EventId_AutoHapticFeedback = "radial_controller_auto_haptic_feedback";

        private IntPtr windowHandle;
        private RadialController radialController;
        public LocalUdpClient localUdpClient;

        public Form1()
        {
            InitializeComponent();

            localUdpClient = new LocalUdpClient("RadialControllerServer", Port);
            localUdpClient.ignoreDataFromClient = true;
            localUdpClient.onDataReceived += OnDataReceived;

            this.labelServerVersion.Text = VersionString;
            this.labelServerStatus.Text = "Server is communicating on port " + Port;
            this.labelLastServerMessage.Text = "None";
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);

            ApplicationIdleHelper.OnIdle += OnIdle;

            this.labelRadialOutput.Text = "Waiting to acquire radial controller";

            CreateController();
            SubscribeToControllerCallbacks();
            MenuSuppressed(true);
            SendServerReadyEvent();
        }

        private void OnIdle()
        {
            TimeSpan timespan = TimeSpan.FromMilliseconds(ApplicationIdleHelper.ElapsedTimeMS);
            this.labelRunTime.Text = timespan.ToString();
            this.labelRotationResolution.Text = radialController.RotationResolutionInDegrees.ToString();
            this.labelUseAutoHapticFeedback.Text = radialController.UseAutomaticHapticFeedback.ToString();

        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            ApplicationIdleHelper.OnIdle -= OnIdle;

            UnsubscribeToControllerCallbacks();
            radialController = null;
        }

        private void CreateController()
        {
            //Console.WriteLine("Creating radial controller");
            this.windowHandle = this.Handle;
            IRadialControllerInterop interop = (IRadialControllerInterop)System.Runtime.InteropServices.WindowsRuntime.WindowsRuntimeMarshal.GetActivationFactory(typeof(RadialController));
            Guid guid = typeof(RadialController).GetInterface("IRadialController").GUID;
            radialController = interop.CreateForWindow(this.windowHandle, ref guid);
        }

        private void SubscribeToControllerCallbacks()
        {
            //Console.WriteLine("Subscribing to radial controller callbacks");
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
            //Console.WriteLine("Unsubscribing from radial controller callbacks");
            radialController.ControlLost -= RadialController_ControlLost;
            radialController.ControlAcquired -= RadialController_ControlAcquired;
            radialController.ButtonPressed -= RadialController_ButtonPressed;
            radialController.ButtonReleased -= RadialController_ButtonReleased;
            radialController.ButtonHolding -= RadialController_ButtonHolding;
            radialController.ButtonClicked -= RadialController_ButtonClicked;
            radialController.RotationChanged -= RadialController_RotationChanged;
        }

        private void OnDataReceived(LocalUdpPacket packet)
        {
            Invoke(new Action(() => {
                string msg = "Data received from " + packet.senderId + ":\n";
                msg += "  [data]: " + MiniJSON.Json.Serialize(packet.data);
                this.labelLastServerMessage.Text = msg;

                if (packet.senderId == Client_SenderId)
                {
                    // Process the packet's event data.
                    string eventId = null;

                    if (packet.data.ContainsKey("event_id"))
                    {
                        eventId = packet.data["event_id"] as string;
                    }

                    if (eventId != null)
                    {
                        switch (eventId)
                        {
                            case EventId_RotationResolutionInDegrees:
                                double degrees = Convert.ToDouble(packet.data["degrees"]);
                                radialController.RotationResolutionInDegrees = degrees;
                                break;
                            case EventId_AutoHapticFeedback:
                                bool useAutoHapticFeedback = Convert.ToBoolean(packet.data["use_auto_haptic_feedback"]);
                                radialController.UseAutomaticHapticFeedback = useAutoHapticFeedback;
                                break;
                            default:
                                Console.WriteLine("Event Id " + eventId + " is not implemented.");
                                break;
                        }
                    }
                    else
                    {
                        // This is not a normal radial controller event from the client. Lets just throw this one to the console.
                        Console.WriteLine(msg);
                    }
                }
            }),
                null
            );
        }

        private void RadialController_ControlAcquired(RadialController sender, RadialControllerControlAcquiredEventArgs args)
        {
            //Console.WriteLine("control acquired");
            Invoke(new Action(() => {
                var timestamp = DateTime.Now.ToLongTimeString();
                this.labelRadialOutput.Text = string.Format("[{0}] control acquired", timestamp);

                var data = new Dictionary<string, object>();
                data["event_id"] = EventId_ControlAcquired;
                localUdpClient.Send(data);
            }));
        }

        private void RadialController_ControlLost(RadialController sender, object args)
        {
            //Console.WriteLine("control lost");
            Invoke(new Action(() => {
                var timestamp = DateTime.Now.ToLongTimeString();
                this.labelRadialOutput.Text = string.Format("[{0}] control lost", timestamp);

                var data = new Dictionary<string, object>();
                data["event_id"] = EventId_ControlLost;
                localUdpClient.Send(data);
            }));
        }

        private void RadialController_RotationChanged(RadialController sender, RadialControllerRotationChangedEventArgs args)
        {
            //Console.WriteLine("rotation changed: " + args.RotationDeltaInDegrees);
            Invoke(new Action(() => {
                var timestamp = DateTime.Now.ToLongTimeString();
                this.labelRadialOutput.Text = string.Format("[{0}] rotation changed: {1}", timestamp, args.RotationDeltaInDegrees);

                var data = new Dictionary<string, object>();
                data["event_id"] = EventId_RotationChanged;
                data["delta_degrees"] = args.RotationDeltaInDegrees;
                localUdpClient.Send(data);
            }));
        }
        private void RadialController_ButtonPressed(RadialController sender, RadialControllerButtonPressedEventArgs args)
        {
            //Console.WriteLine("button pressed");
            Invoke(new Action(() => {
                var timestamp = DateTime.Now.ToLongTimeString();
                this.labelRadialOutput.Text = string.Format("[{0}] button pressed", timestamp);

                var data = new Dictionary<string, object>();
                data["event_id"] = EventId_ButtonPressed;
                localUdpClient.Send(data);
            }));
        }

        private void RadialController_ButtonReleased(RadialController sender, RadialControllerButtonReleasedEventArgs args)
        {
            //Console.WriteLine("button released");
            Invoke(new Action(() => {
                var timestamp = DateTime.Now.ToLongTimeString();
                this.labelRadialOutput.Text = string.Format("[{0}] button released", timestamp);

                var data = new Dictionary<string, object>();
                data["event_id"] = EventId_ButtonReleased;
                localUdpClient.Send(data);
            }));
        }

        private void RadialController_ButtonHolding(RadialController sender, RadialControllerButtonHoldingEventArgs args)
        {
            //Console.WriteLine("button holding");
            Invoke(new Action(() => {
                var timestamp = DateTime.Now.ToLongTimeString();
                this.labelRadialOutput.Text = string.Format("[{0}] button holding", timestamp);

                var data = new Dictionary<string, object>();
                data["event_id"] = EventId_ButtonHolding;
                localUdpClient.Send(data);
            }));
        }

        private void RadialController_ButtonClicked(RadialController sender, RadialControllerButtonClickedEventArgs args)
        {
            //Console.WriteLine("button clicked");
            Invoke(new Action(() => {
                var timestamp = DateTime.Now.ToLongTimeString();
                this.labelRadialOutput.Text = string.Format("[{0}] button clicked", timestamp);

                var data = new Dictionary<string, object>();
                data["event_id"] = EventId_ButtonClicked;
                localUdpClient.Send(data);
            }));
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
            //Console.WriteLine("[RadialControllerInterface] menu suppressed: " + suppressed);
            var config = GetConfig();
            config.ActiveControllerWhenMenuIsSuppressed = radialController;
            config.IsMenuSuppressed = suppressed;
        }

        private void SendServerReadyEvent()
        {
            var data = new Dictionary<string, object>();
            data["event_id"] = EventId_ServerReady;
            localUdpClient.Send(data);
        }

        private void buttonSendTestMsg_Click(object sender, EventArgs e)
        {
            var data = new Dictionary<string, object>();
            data["message"] = "hello from the server";
            data["time"] = DateTime.Now.ToString();

            localUdpClient.Send(data);
        }
    }
}
