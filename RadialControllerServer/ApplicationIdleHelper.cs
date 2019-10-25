using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RadialControllerWinForm
{
    public static class ApplicationIdleHelper
    {
        public static double ElapsedTimeMS { get; private set; }
        public static int FrameCount { get; private set; }
        public static event System.Action OnIdle;

        private static DateTime _startTime;

        static ApplicationIdleHelper()
        {
            Application.Idle += Application_Idle;
            _startTime = DateTime.Now;
        }

        private static void Application_Idle(object sender, EventArgs e)
        {
            while (IsApplicationIdle())
            {
                DateTime now = DateTime.Now;
                TimeSpan timeSpan = now - _startTime;
                ElapsedTimeMS = timeSpan.TotalMilliseconds;

                FrameCount++;

                if (OnIdle != null)
                    OnIdle();
            }
        }

        private static bool IsApplicationIdle()
        {
            NativeMessage result;
            return PeekMessage(out result, IntPtr.Zero, (uint)0, (uint)0, (uint)0) == 0;
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct NativeMessage
        {
            public IntPtr Handle;
            public uint Message;
            public IntPtr WParameter;
            public IntPtr LParameter;
            public uint Time;
            public Point Location;
        }

        [DllImport("user32.dll")]
        private static extern int PeekMessage(out NativeMessage message, IntPtr window, uint filterMin, uint filterMax, uint remove);
    }
}
