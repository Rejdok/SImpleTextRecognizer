using System;
using System.Drawing;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace SimpleTextRecognizer
{

    public delegate void formCallBack(Tuple<Point, Size> rez);
    public partial class CaptureRegion : Form
    {
        IntPtr _hook;
        MouseHook.CBTProc _callback;
        bool shoudChangeSize = false;
        formCallBack fcb;
        public CaptureRegion(formCallBack fcb)
        {
            this.fcb = fcb;
            InitializeComponent();
            this.TopMost = true;
            this.Hide();
            this.BackColor = Color.Tan;
            this.TransparencyKey = Color.Tan;
            _callback = this.MouseMoveCallBack;
            _hook = MouseHook.SetWindowsHookEx(MouseHook.HookType.WH_MOUSE_LL, _callback, MouseHook.GetModuleHandle("user32"), 0);
            if (_hook == IntPtr.Zero) throw new System.ComponentModel.Win32Exception();

        }

        private void MoveWindow(int x, int y)
        {
            this.Top = y;
            this.Left = x;
        }
        private IntPtr MouseMoveCallBack(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0)
            {
                var WM_Code = (MouseHook.WM_Codes)wParam;
                var mouseInfo = Marshal.PtrToStructure<MouseHook.MOUSEHOOKSTRUCT>(lParam);
                if (WM_Code == MouseHook.WM_Codes.WM_MOUSEMOVE)
                {
                    OnMouseMove(mouseInfo);
                }
                if (WM_Code == MouseHook.WM_Codes.WM_LBUTTONDOWN)
                {
                    OnLBtnClick();
                }
                if (WM_Code == MouseHook.WM_Codes.WM_LBUTTONUP)
                {
                    OnLBtnUp();
                }
            }
            return MouseHook.CallNextHookEx(IntPtr.Zero, code, wParam, lParam);
        }

        private void OnLBtnUp()
        {
            fcb(new Tuple<Point, Size>(new Point(this.Left, this.Top), Size));
            MouseHook.UnhookWindowsHookEx(_hook);
            this.Close();
        }

        private void OnLBtnClick()
        {
            shoudChangeSize = true;
            this.Show();
        }

        private void OnMouseMove(MouseHook.MOUSEHOOKSTRUCT mouseInfo)
        {
            if (shoudChangeSize)
            {
                ChangeWindowSize(mouseInfo);
            }
            else
            {
                MoveWindow(mouseInfo.pt.X, mouseInfo.pt.Y);
            }
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
        private void ChangeWindowSize(MouseHook.MOUSEHOOKSTRUCT mouseInfo)
        {
            int X = 0, Y = 0;
            if (mouseInfo.pt.Y > this.Top)
            {
                Y = mouseInfo.pt.Y - this.Top;
            }
            else
            {
                Y = this.Top - mouseInfo.pt.Y;
            }
            if (mouseInfo.pt.X > this.Left)
            {
                X = mouseInfo.pt.X - this.Left;
            }
            else
            {
                X = this.Left - mouseInfo.pt.X;
            }
            this.Size = new Size(X, Y);
        }

    }
}

