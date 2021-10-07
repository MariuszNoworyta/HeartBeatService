using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;

namespace SimpleHeartBeatService
{
    public class WindowsHelper
    {
        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ExitWindowsEx(uint uFlags, uint dwReason);

        public static void Logout()
        {
            Console.WriteLine("Logout");

            ExitWindowsEx(0, 0);

        }


    }
}
