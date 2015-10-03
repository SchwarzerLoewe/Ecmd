using System;
using System.Runtime.InteropServices;

namespace WinApiPlugin
{
    internal class API
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string className, string windowText);
        [DllImport("user32.dll")]
        private static extern int ShowWindow(IntPtr hwnd, int command);

        [DllImport("kernel32.dll")]
        static extern IntPtr GetConsoleWindow();

        private const int SW_HIDE = 0;
        private const int SW_SHOW = 1;

        public static void HideTasktbar()
        {
            IntPtr hwnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwnd, SW_HIDE);
        }
        public static void ShowTasktbar()
        {
            IntPtr hwnd = FindWindow("Shell_TrayWnd", "");
            ShowWindow(hwnd, SW_SHOW);
        }

        public static void ShowConsole()
        {
            IntPtr hwnd = GetConsoleWindow();
            ShowWindow(hwnd, SW_SHOW);
        }


        public static void HideConsole()
        {
            IntPtr hwnd = GetConsoleWindow();
            ShowWindow(hwnd, SW_HIDE);
        }

    }
}