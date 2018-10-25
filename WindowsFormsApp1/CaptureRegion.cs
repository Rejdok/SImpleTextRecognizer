using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;


namespace WindowsFormsApp1
{
    public struct POINT
    {
        public int X;
        public int Y;
    }


    public delegate void formCallBack(Tuple<Point, Size> rez);
    public partial class CaptureRegion : Form
    {
        IntPtr _hook;
        NativeMethods.CBTProc _callback;
        bool shoudChangeSize = false;
        formCallBack fcb;
        public CaptureRegion(formCallBack fcb)
        {
            this.fcb = fcb;
            InitializeComponent();
            this.TopMost = true;
            this.Hide();
            _callback = this.MouseMoveCallBack;
            _hook = NativeMethods.SetWindowsHookEx(NativeMethods.HookType.WH_MOUSE_LL, _callback, NativeMethods.GetModuleHandle("user32"), 0);
            if (_hook == IntPtr.Zero) throw new System.ComponentModel.Win32Exception();
            
        }

        private void CaptureRegion_Paint(object sender, PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(new Pen(Color.Red, 3),
                                     this.DisplayRectangle);
        }
        private void CaptureRegion_MouseClick(object sender, MouseEventArgs e)
        {
            this.Close();
        }

        private IntPtr MouseMoveCallBack(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0)
            {
                var WM_Code = (NativeMethods.WM_Codes)wParam;
                var mouseInfo = Marshal.PtrToStructure<NativeMethods.MOUSEHOOKSTRUCT>(lParam);
                if (WM_Code == NativeMethods.WM_Codes.WM_MOUSEMOVE) {
                    if (shoudChangeSize)
                    {
                        int X=0, Y=0;
                        if (mouseInfo.pt.Y > this.Top)
                        {
                            Y = mouseInfo.pt.Y - this.Top;
                        }
                        else
                        {
                            Y = this.Top - mouseInfo.pt.Y;
                        }
                        if (mouseInfo.pt.X> this.Left)
                        {
                            X = mouseInfo.pt.X - this.Left;
                        }
                        else
                        {
                            X = this.Left - mouseInfo.pt.X;
                        }
                        this.Size =new Size(X,Y);
                    }
                    else
                    {
                        this.Top = mouseInfo.pt.Y;
                        this.Left = mouseInfo.pt.X;
                    }
                }
                if(WM_Code == NativeMethods.WM_Codes.WM_LBUTTONDOWN)
                {

                    shoudChangeSize = true;
                    this.Show();
                }
                if(WM_Code == NativeMethods.WM_Codes.WM_LBUTTONUP)
                {
                    fcb(new Tuple<Point, Size>(new Point(this.Left, this.Top), Size));
                    NativeMethods.UnhookWindowsHookEx(_hook);
                    this.Close();
                }
            }
            return NativeMethods.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);

            
            
        }

        private static class NativeMethods
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct MOUSEHOOKSTRUCT
            {
                public POINT pt; // Can't use System.Windows.Point because that has X,Y as doubles, not integer
                public IntPtr hwnd;
                public uint wHitTestCode;
                public IntPtr dwExtraInfo;
                public override string ToString()
                {
                    return $"({pt.X,4},{pt.Y,4})";
                }
            }

#pragma warning disable 649 // CS0649: Field 'MainWindow.NativeMethods.POINT.Y' is never assigned to, and will always have its default value 0
            public struct POINT
            {
                public int X;
                public int Y;
            }
#pragma warning restore 649

            // from WinUser.h
            public enum HookType
            {
                WH_MIN = (-1),
                WH_MSGFILTER = (-1),
                WH_JOURNALRECORD = 0,
                WH_JOURNALPLAYBACK = 1,
                WH_KEYBOARD = 2,
                WH_GETMESSAGE = 3,
                WH_CALLWNDPROC = 4,
                WH_CBT = 5,
                WH_SYSMSGFILTER = 6,
                WH_MOUSE = 7,
                WH_HARDWARE = 8,
                WH_DEBUG = 9,
                WH_SHELL = 10,
                WH_FOREGROUNDIDLE = 11,
                WH_CALLWNDPROCRET = 12,
                WH_KEYBOARD_LL = 13,
                WH_MOUSE_LL = 14
            }
            public enum HookCodes
            {
                HC_ACTION = 0,
                HC_GETNEXT = 1,
                HC_SKIP = 2,
                HC_NOREMOVE = 3,
                HC_NOREM = HC_NOREMOVE,
                HC_SYSMODALON = 4,
                HC_SYSMODALOFF = 5
            }
            public enum CBTHookCodes
            {
                HCBT_MOVESIZE = 0,
                HCBT_MINMAX = 1,
                HCBT_QS = 2,
                HCBT_CREATEWND = 3,
                HCBT_DESTROYWND = 4,
                HCBT_ACTIVATE = 5,
                HCBT_CLICKSKIPPED = 6,
                HCBT_KEYSKIPPED = 7,
                HCBT_SYSCOMMAND = 8,
                HCBT_SETFOCUS = 9
            }
            public enum WM_Codes
            {
                WM_LBUTTONDOWN = 0x0201,
                WM_LBUTTONUP = 0x0202,
                WM_MOUSEMOVE = 0x0200,
                WM_MOUSEWHEEL = 0x020A,
                WM_RBUTTONDOWN = 0x0204,
                WM_RBUTTONUP = 0x0205
            }
            public delegate IntPtr CBTProc(int code, IntPtr wParam, IntPtr lParam);

            [DllImport("user32.dll")]
            [return: MarshalAs(UnmanagedType.Bool)]
            public static extern bool UnhookWindowsHookEx(IntPtr hookPtr);

            [DllImport("user32.dll")]
            public static extern IntPtr CallNextHookEx(IntPtr hookPtr, int nCode, IntPtr wordParam, IntPtr longParam);

            [DllImport("user32.dll")]
            public static extern IntPtr SetWindowsHookEx(HookType hookType, CBTProc hookProc, IntPtr instancePtr, uint threadID);

            [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
            public static extern IntPtr GetModuleHandle(string lpModuleName);

        }
    }
}

