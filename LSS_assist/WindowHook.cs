using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Forms;

namespace LSS_assist
{
    class WindowHook
    {
        [DllImport("user32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern IntPtr PostMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);
        [DllImport("User32.dll")]
        public static extern Int32 SetForegroundWindow(int hWnd);

        #region Abandoned
        private static void SendKeyManaged(Key k)
        {
            if (k == Key.Enter)
                sendKeystroke(k);
        }

        private static void sendKeystroke(Key k)
        {
            const uint WM_KEYDOWN = 0x100;
            const uint WM_KEYUP = 0x101;
            const uint WM_SYSCOMMAND = 0x018;
            const uint SC_CLOSE = 0x053;

            IntPtr WindowToFind = FindWindow(null, "LSS");
            //SetForegroundWindow(WindowToFind.ToInt32());

            //IntPtr result3 = SendMessage(WindowToFind, WM_KEYDOWN, ((IntPtr)k), (IntPtr)0);
            //result3 = SendMessage(WindowToFind, WM_KEYUP, ((IntPtr)k), (IntPtr)0);
            IntPtr result3 = PostMessage(WindowToFind, WM_KEYDOWN, ((IntPtr)k), (IntPtr)0);
            result3 = PostMessage(WindowToFind, WM_KEYUP, ((IntPtr)k), (IntPtr)0);

        }
        private static void sendUsingSendKeys(string key)
        {
            IntPtr WindowToFind = FindWindow(null, "LSS");
            SetForegroundWindow(WindowToFind.ToInt32());

            SendKeys.SendWait(key);

        }
#endregion

        public static void SendMouseManaged(string desc)
        {
            switch (desc.ToUpper())
            {
                case "EARN":
                    sendUsingMouseSim(57, 33);
                    break;
                case "GIVE":
                    sendUsingMouseSim(313, 386);
                    break;

            }
        }
        public static void sendUsingMouseSim(int xPos, int yPos)
        {
            const int WM_LBUTTONDOWN = 0x201;
            const int WM_LBUTTONUP = 0x202;
            const int MK_LBUTTON = 0x0001;

            IntPtr WindowToFind = FindWindow(null, "LSS");
            if (WindowToFind.ToInt32() == 0)
                return;// Do nothing if we did not find window

            IntPtr result3 = PostMessage(WindowToFind, WM_LBUTTONDOWN, (IntPtr)MK_LBUTTON, (IntPtr)(xPos + (yPos << 16)));
            result3 = PostMessage(WindowToFind, WM_LBUTTONUP, ((IntPtr)0), (IntPtr)(xPos + (yPos << 16)));
        }

    }

}