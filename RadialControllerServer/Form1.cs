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
        public const string VersionString = "v0.1.0 (October 28, 2019 1:00PM EST)";

        private const string EventId_ControlAcquired = "radial_controller_control_acquired";
        private const string EventId_ControlLost = "radial_controller_control_lost";
        private const string EventId_ButtonClicked = "radial_controller_button_clicked";
        private const string EventId_ButtonPressed = "radial_controller_button_pressed";
        private const string EventId_ButtonHolding = "radial_controller_button_holding";
        private const string EventId_ButtonReleased = "radial_controller_button_released";
        private const string EventId_RotationChanged = "radial_controller_rotation_changed";

        public RadialControllerInterface radialInterface;
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

            radialInterface = new RadialControllerInterface(this.Handle);
            radialInterface.onButtonClicked += OnRadialButtonClicked;
            radialInterface.onButtonPressed += OnRadialButtonPressed;
            radialInterface.onButtonReleased += OnRadialButtonReleased;
            radialInterface.onButtonHolding += OnRadialButtonHolding;
            radialInterface.onControlAcquired += OnRadialControlAcquired;
            radialInterface.onControlLost += OnRadialControlLost;
            radialInterface.onRotationChanged += OnRadialRotationChanged;
        }

        private void OnIdle()
        {
            TimeSpan timespan = TimeSpan.FromMilliseconds(ApplicationIdleHelper.ElapsedTimeMS);
            this.labelRunTime.Text = timespan.ToString();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            radialInterface.onButtonClicked -= OnRadialButtonClicked;
            radialInterface.onButtonPressed -= OnRadialButtonPressed;
            radialInterface.onButtonReleased -= OnRadialButtonReleased;
            radialInterface.onButtonHolding -= OnRadialButtonHolding;
            radialInterface.onControlAcquired -= OnRadialControlAcquired;
            radialInterface.onControlLost -= OnRadialControlLost;
            radialInterface.onRotationChanged -= OnRadialRotationChanged;

            radialInterface.Dispose();
            radialInterface = null;
        }

        private void OnDataReceived(LocalUdpPacket packet)
        {
            Invoke(new Action(() => {
                string msg = "Data received from " + packet.senderId + ":\n";
                msg += "  [data]: " + MiniJSON.Json.Serialize(packet.data);
                this.labelLastServerMessage.Text = msg;
                Console.WriteLine(msg);
            }),
                null
            );
        }

        private void OnRadialRotationChanged(double deltaDegrees)
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            this.labelRadialOutput.Text = string.Format("[{0}] rotation changed: {1}", timestamp, deltaDegrees);

            var data = new Dictionary<string, object>();
            data["event_id"] = EventId_RotationChanged;
            data["delta_degrees"] = deltaDegrees;
            localUdpClient.Send(data);
        }

        private void OnRadialControlLost()
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            this.labelRadialOutput.Text = string.Format("[{0}] control lost", timestamp);

            var data = new Dictionary<string, object>();
            data["event_id"] = EventId_ControlLost;
            localUdpClient.Send(data);
        }

        private void OnRadialControlAcquired()
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            this.labelRadialOutput.Text = string.Format("[{0}] control acquired", timestamp);

            var data = new Dictionary<string, object>();
            data["event_id"] = EventId_ControlAcquired;
            localUdpClient.Send(data);
        }

        private void OnRadialButtonReleased()
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            this.labelRadialOutput.Text = string.Format("[{0}] button released", timestamp);

            var data = new Dictionary<string, object>();
            data["event_id"] = EventId_ButtonReleased;
            localUdpClient.Send(data);
        }

        private void OnRadialButtonHolding()
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            this.labelRadialOutput.Text = string.Format("[{0}] button holding", timestamp);

            var data = new Dictionary<string, object>();
            data["event_id"] = EventId_ButtonHolding;
            localUdpClient.Send(data);
        }

        private void OnRadialButtonPressed()
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            this.labelRadialOutput.Text = string.Format("[{0}] button pressed", timestamp);

            var data = new Dictionary<string, object>();
            data["event_id"] = EventId_ButtonPressed;
            localUdpClient.Send(data);
        }

        private void OnRadialButtonClicked()
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            this.labelRadialOutput.Text = string.Format("[{0}] button clicked", timestamp);

            var data = new Dictionary<string, object>();
            data["event_id"] = EventId_ButtonClicked;
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
