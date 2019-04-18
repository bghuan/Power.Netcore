using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Power
{
    /// <summary>
    /// 控制台帮助类
    /// </summary>
    public static class ConsoleHelper
    {
        [DllImport("user32.dll", SetLastError = true)]
        private static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll", SetLastError = true)]
        private static extern bool ShowWindow(IntPtr hWnd, uint nCmdShow);
        public static void hideConsole(string ConsoleTitle = "")
        {
            ConsoleTitle = String.IsNullOrEmpty(ConsoleTitle) ? Console.Title : ConsoleTitle;
            IntPtr hWnd = FindWindow("ConsoleWindowClass", ConsoleTitle);
            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, 0);
            }
        }
        public static void showConsole(string ConsoleTitle = "")
        {
            ConsoleTitle = String.IsNullOrEmpty(ConsoleTitle) ? Console.Title : ConsoleTitle;
            IntPtr hWnd = FindWindow("ConsoleWindowClass", ConsoleTitle);
            if (hWnd != IntPtr.Zero)
            {
                ShowWindow(hWnd, 1);
            }
        }
        [DllImport("user32.dll")]
        public static extern void keybd_event(byte bVk, byte bScan, int dwFlags, int dwExtraInfo);
        public static void Send(string s)
        {
            byte[] bytes = System.Text.Encoding.Default.GetBytes(s);
            foreach (byte b in bytes)
            {
                if (b >= 48 && b <= 57)//0-9
                {
                    keybd_event(b, 0, 0, 0);
                    keybd_event(b, 0, 2, 0);
                }
                else if (b >= 65 && b <= 90)//A-Z
                {
                    keybd_event(16, 0, 0, 0);
                    keybd_event(b, 0, 0, 0);
                    keybd_event(b, 0, 2, 0);
                    keybd_event(16, 0, 2, 0);
                }
                else if (b >= 97 && b <= 122)//a-z
                {
                    keybd_event((byte)((int)b - 32), 0, 0, 0);
                    keybd_event((byte)((int)b - 32), 0, 2, 0);
                }
                else
                {

                }
            }
            keybd_event(13, 0, 0, 0);
        }
        static  string Sendlink = "https://www.cnblogs.com/rosesmall/p/5759804.html";
    }

}
