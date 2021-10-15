using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace SimpleHeartBeatService
{
    [Flags]
    public enum ExitWindows : uint
    {
        // ONE of the following:
        LogOff = 0x00,
        ShutDown = 0x01,
        Reboot = 0x02,
        PowerOff = 0x08,
        RestartApps = 0x40,
        // plus AT MOST ONE of the following two:
        Force = 0x04,
        ForceIfHung = 0x10,
    }

    [Flags]
    public enum ShutdownReason : uint
    {
        None = 0,

        MajorApplication = 0x00040000,
        MajorHardware = 0x00010000,
        MajorLegacyApi = 0x00070000,
        MajorOperatingSystem = 0x00020000,
        MajorOther = 0x00000000,
        MajorPower = 0x00060000,
        MajorSoftware = 0x00030000,
        MajorSystem = 0x00050000,

        MinorBlueScreen = 0x0000000F,
        MinorCordUnplugged = 0x0000000b,
        MinorDisk = 0x00000007,
        MinorEnvironment = 0x0000000c,
        MinorHardwareDriver = 0x0000000d,
        MinorHotfix = 0x00000011,
        MinorHung = 0x00000005,
        MinorInstallation = 0x00000002,
        MinorMaintenance = 0x00000001,
        MinorMMC = 0x00000019,
        MinorNetworkConnectivity = 0x00000014,
        MinorNetworkCard = 0x00000009,
        MinorOther = 0x00000000,
        MinorOtherDriver = 0x0000000e,
        MinorPowerSupply = 0x0000000a,
        MinorProcessor = 0x00000008,
        MinorReconfig = 0x00000004,
        MinorSecurity = 0x00000013,
        MinorSecurityFix = 0x00000012,
        MinorSecurityFixUninstall = 0x00000018,
        MinorServicePack = 0x00000010,
        MinorServicePackUninstall = 0x00000016,
        MinorTermSrv = 0x00000020,
        MinorUnstable = 0x00000006,
        MinorUpgrade = 0x00000003,
        MinorWMI = 0x00000015,

        FlagUserDefined = 0x40000000,
        FlagPlanned = 0x80000000
    }

    public class WindowsHelper
    {

        readonly Color bgColor = Color.Orange;
        readonly Brush foreColor = Brushes.DarkOrange;
        static Form form;
        static Graphics graph;
        static Font font;
        static Point xy;



        public WindowsHelper()
        {
            CreateForm();
            CreateGraphic();
        }

        private void CreateGraphic()
        {
            graph = form.CreateGraphics();
            font = new Font("Arial", 20);
            xy = new Point();
            xy.X = xy.Y = 0;
        }

        private void CreateForm()
        {
            form = new Form();
            form.BackColor = bgColor;
            form.TransparencyKey = bgColor;
            form.TopMost = true;
            form.StartPosition = FormStartPosition.Manual;
            form.Left = form.Top = 5;
            form.Width = form.Height = 50;
            form.FormBorderStyle = FormBorderStyle.None;
            form.ShowIcon = false;            
            form.Show();
        }

        [DllImport("user32.dll", SetLastError = true)]
        static extern bool ExitWindowsEx(uint uFlags, uint dwReason);
        
        public static void Logout()
        {
            Console.WriteLine("Logout");
            ExitWindowsEx((uint)(ExitWindows.LogOff|ExitWindows.Force), 10);
        }

        public void DrawTextOnScreen(string text)
        {
            graph.Clear(bgColor);
            graph.DrawString(text, font, foreColor, xy);

        }


    }
}
