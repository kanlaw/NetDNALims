using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace IMClient.Native
{
   public class Win32Api
    {
        const int GW_HWNDFIRST = 0; //{同级别 Z 序最上}  
        const int GW_HWNDLAST = 1; //{同级别 Z 序最下}  
        const int GW_HWNDNEXT = 2; //{同级别 Z 序之下}  
        const int GW_HWNDPREV = 3; //{同级别 Z 序之上}  
        const int GW_OWNER = 4; //{属主窗口}  
        const int GW_CHILD = 5; //{子窗口中的最上}  

        [DllImport("user32.dll", EntryPoint = "GetWindow")]//获取窗体句柄，hwnd为源窗口句柄  
                                                           /*wCmd指定结果窗口与源窗口的关系，它们建立在下述常数基础上： 
                                                                 GW_CHILD 
                                                                 寻找源窗口的第一个子窗口 
                                                                 GW_HWNDFIRST 
                                                                 为一个源子窗口寻找第一个兄弟（同级）窗口，或寻找第一个顶级窗口 
                                                                 GW_HWNDLAST 
                                                                 为一个源子窗口寻找最后一个兄弟（同级）窗口，或寻找最后一个顶级窗口 
                                                                 GW_HWNDNEXT 
                                                                 为源窗口寻找下一个兄弟窗口 
                                                                 GW_HWNDPREV 
                                                                 为源窗口寻找前一个兄弟窗口 
                                                                 GW_OWNER 
                                                                 寻找窗口的所有者 
                                                            */
        public static extern int GetWindow(
            int hwnd,
            int wCmd
        );

        [DllImport("user32.dll", EntryPoint = "SetParent")]//设置父窗体  
        public static extern int SetParent(
            int hWndChild,
            int hWndNewParent
        );

        [DllImport("user32.dll", EntryPoint = "GetCursorPos")]//获取鼠标坐标  
        public static extern int GetCursorPos(
            ref POINTAPI lpPoint
        );

        [StructLayout(LayoutKind.Sequential)]//定义与API相兼容结构体，实际上是一种内存转换  
        public struct POINTAPI
        {
            public int X;
            public int Y;
        }

        [DllImport("user32.dll", EntryPoint = "WindowFromPoint")]//指定坐标处窗体句柄  
        public static extern int WindowFromPoint(
            int xPoint,
            int yPoint
        );


        [DllImport("User32")]
        public extern static void mouse_event(int dwFlags, int dx, int dy, int dwData, IntPtr dwExtraInfo);
        [DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);
        [DllImport("User32")]
        public extern static bool GetCursorPos(out POINT p);
        [StructLayout(LayoutKind.Sequential)]
        public struct POINT { public int X; public int Y; }
        public enum MouseEventFlags
        {
            Move = 0x0001,
            LeftDown = 0x0002,
            LeftUp = 0x0004,
            RightDown = 0x0008,
            RightUp = 0x0010,
            MiddleDown = 0x0020,
            MiddleUp = 0x0040,
            Wheel = 0x0800, Absolute = 0x8000
        }
        /// <summary>  
        /// 自动双击事件  
        /// </summary>  
        /// <param name="x">x坐标</param>  
        /// <param name="y">y坐标</param>  
        private void AutoDoubleClick(int x, int y)
        {
            POINT point = new POINT();
            GetCursorPos(out point);
            try
            {
                SetCursorPos(x, y);
                mouse_event((int)(MouseEventFlags.LeftDown | MouseEventFlags.Absolute), 0, 0, 0, IntPtr.Zero);
                mouse_event((int)(MouseEventFlags.LeftUp | MouseEventFlags.Absolute), 0, 0, 0, IntPtr.Zero);
                mouse_event((int)(MouseEventFlags.LeftDown | MouseEventFlags.Absolute), 0, 0, 0, IntPtr.Zero);
                mouse_event((int)(MouseEventFlags.LeftUp | MouseEventFlags.Absolute), 0, 0, 0, IntPtr.Zero);
                mouse_event((int)(MouseEventFlags.LeftDown | MouseEventFlags.Absolute), 0, 0, 0, IntPtr.Zero);
                mouse_event((int)(MouseEventFlags.LeftUp | MouseEventFlags.Absolute), 0, 0, 0, IntPtr.Zero);
            }
            finally
            {
                SetCursorPos(point.X, point.Y);
            }
        }
        /// <summary>  
        /// 自动单机事件  
        /// </summary>  
        /// <param name="x">x坐标</param>  
        /// <param name="y">y坐标</param>  
        private void AutoClick(int x, int y)
        {
            POINT point = new POINT();
            GetCursorPos(out point);
            try
            {
                SetCursorPos(x, y);
                mouse_event((int)(MouseEventFlags.LeftDown | MouseEventFlags.Absolute), 0, 0, 0, IntPtr.Zero);
                mouse_event((int)(MouseEventFlags.LeftUp | MouseEventFlags.Absolute), 0, 0, 0, IntPtr.Zero);
            }
            finally
            {
                SetCursorPos(point.X, point.Y);
            }

        }

        // Find Window  

        // 查找窗体  

        // @para1: 窗体的类名 例如对话框类是"#32770"  

        // @para2: 窗体的标题 例如打开记事本 标题是"无标题 - 记事本" 注意 - 号两侧的空格  

        // return: 窗体的句柄  

        [DllImport("User32.dll", EntryPoint = "FindWindow")]

        public static extern IntPtr FindWindow(string className, string windowName);


        // Find Window Ex  

        // 查找窗体的子窗体  

        // @para1: 父窗体的句柄 如果为null，则函数以桌面窗口为父窗口，查找桌面窗口的所有子窗口  

        // @para2: 子窗体的句柄 如果为null，从@para1的直接子窗口的第一个开始查找  

        // @para3: 子窗体的类名 为""表示所有类  

        // @para4: 子窗体的标题 为""表示要查找的窗体无标题 如空白的textBox控件  

        // return: 子窗体的句柄  

        [DllImport("user32.dll", EntryPoint = "FindWindowEx")]

        public static extern IntPtr FindWindowEx(

            IntPtr hwndParent,

            IntPtr hwndChildAfter,

            string lpszClass,

            string lpszWindow);


        // SendMessage  

        // 向窗体发送消息  

        // @para1: 窗体句柄  

        // @para2: 消息类型  

        // @para3: 附加的消息信息  

        // @para4: 附加的消息信息  

        [DllImport("User32.dll", EntryPoint = "SendMessage")]

        public static extern int SendMessage(

            IntPtr hWnd,

            int Msg,

            IntPtr wParam,

            string lParam);



        // 消息类型（部分）  

        const int WM_GETTEXT = 0x000D;  // 获得窗体文本 如获得对话框标题  

        const int WM_SETTEXT = 0x000C;  // 设置窗体文本 如设置文本框内容  

        const int WM_CLICK = 0x00F5;  // 发送点击消息如调用该窗体（按钮）的"button1_Click();"  

        const int WM_LBUTTONDOWN = 0x0201;

        const int WM_LBUTTONUP = 0x0202;
        //const int WM_LBUTTONDBLCLK = 0x0203;  

        const int WM_MOUSEMOVE = 0x200;
        //  const int WM_LBUTTONDOWN = 0x201;  
        const int WM_RBUTTONDOWN = 0x204;
        const int WM_MBUTTONDOWN = 0x207;
        // const int WM_LBUTTONUP = 0x202;  
        const int WM_RBUTTONUP = 0x205;
        //const int WM_MBUTTONUP = 0x208;  
        //const int WM_LBUTTONDBLCLK = 0x203;  
        const int WM_RBUTTONDBLCLK = 0x206;
        const int WM_MBUTTONDBLCLK = 0x209;
        const int WM_MOUSEWHEEL = 0x020A;
        public const int WM_SYSKEYDOWN = 0x0104;

        public const int WM_SETFOCUS = 0x0007;
        public const int WM_KEYDOWN = 0x0100;


        // 本程序针对指定的另一程序窗体因此声名了如下变量  

        IntPtr txt = new IntPtr(0);// 文本框  


        ///////////////////////////////////////////  

        public const int CS_DropSHADOW = 0x20000;
        public const int GCL_STYLE = (-26);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int SetClassLong(IntPtr hwnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int GetClassLong(IntPtr hwnd, int nIndex);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern bool SendMessage(IntPtr hwnd, int wMsg, int wParam, int IParam);
        public const int WM_SYSCOMMAND = 0x0112;
        public const int SC_MOVE = 0xF010;
        public const int HTCAPTION = 0x0002;

    }
}
