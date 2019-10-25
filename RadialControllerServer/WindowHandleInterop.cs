using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RadialControllerWinForm
{
    public static class WindowHandleInterop
    {
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern System.IntPtr GetActiveWindow();

        public static System.IntPtr GetWindowHandle()
        {
            return GetActiveWindow();
        }
    }
}
