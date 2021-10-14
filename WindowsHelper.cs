using System;
using System.Collections.Generic;
using System.Drawing;
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
        [DllImport("user32.dll", SetLastError = true)]
        static extern IntPtr GetDC(IntPtr hwnd);

        public static void Logout()
        {
            Console.WriteLine("Logout");

            //ExitWindowsEx(0, 0);

        }

        public static void DrawTextOnScreen(string text)
        {
            var hwnd = GetDC(IntPtr.Zero);
            using (Graphics g = Graphics.FromHdc(hwnd))
            {
                PointF point = new PointF();
                point.X = point.Y  =50f;
                Font font = new Font("Arial", 20);

                g.DrawString(text,font , Brushes.Yellow, point);
            }
        }


    }
}
