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
        RadialControllerInterface radialInterface;

        public Form1()
        {
            InitializeComponent();

            this.labelServerStatus.Text = "Server has not started.";
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

            this.labelFrameCount.Text = ApplicationIdleHelper.FrameCount.ToString();
        }

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            Application.Idle -= Application_Idle;

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

        private void Application_Idle(object sender, EventArgs e)
        {
        }

        private void OnRadialRotationChanged(double deltaDegrees)
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            this.labelRadialOutput.Text = string.Format("[{0}] rotation changed: {1}", timestamp, deltaDegrees);
        }

        private void OnRadialControlLost()
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            this.labelRadialOutput.Text = string.Format("[{0}] control lost", timestamp);
        }

        private void OnRadialControlAcquired()
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            this.labelRadialOutput.Text = string.Format("[{0}] control acquired", timestamp);
        }

        private void OnRadialButtonReleased()
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            this.labelRadialOutput.Text = string.Format("[{0}] button released", timestamp);
        }

        private void OnRadialButtonHolding()
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            this.labelRadialOutput.Text = string.Format("[{0}] button holding", timestamp);
        }

        private void OnRadialButtonPressed()
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            this.labelRadialOutput.Text = string.Format("[{0}] button pressed", timestamp);
        }

        private void OnRadialButtonClicked()
        {
            var timestamp = DateTime.Now.ToLongTimeString();
            this.labelRadialOutput.Text = string.Format("[{0}] button clicked", timestamp);
        }
    }
}
