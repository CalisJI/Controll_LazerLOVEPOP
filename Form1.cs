using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using Newtonsoft.Json;

namespace ControlLazerApp
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            if (!Directory.Exists(Root_Path)) 
            {
                Directory.CreateDirectory(Root_Path);
            }
            LoadConfig();
            Get_Coordinate_Color();
            Get_Coordinate_Settings();
            ReadSetting();
            //dataGridView1.Enabled = false;
            //Timer_second.Start();

            Publisher.Initialize_Subcriber("node-red");
        }

        

        private static string Root_Path = Directory.GetCurrentDirectory() + @"/Config";
        private static string Fileconfig = Root_Path + "Settings.json";
        private static ConfigApp ConfigApp = new ConfigApp();
        private static System.Windows.Forms.Timer Timer_second = new System.Windows.Forms.Timer();
        #region User32 DLL

        private const int MOUSEEVENTF_LEFTDOWN = 0x02;
        private const int MOUSEEVENTF_LEFTUP = 0x04;
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, uint wMnd, IntPtr wParam, string lParam);
        [DllImport("user32.dll", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr FindWindowEx(IntPtr parentHandle, IntPtr childAfter, string className, string windowTitle);

        [DllImport("user32.dll")]
        static extern bool SetCursorPos(int X, int Y);
        const int BM_CLICK = 0x00f5;
        private const int BN_CLICKED = 245;
        const int WM_SETTEXT = 0X000C;
        const uint LB_SETCURSEL = 0x0186; // Listbox set current selection message
        const int WM_CLOSE = 0x0010;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, int dx, int dy, uint cButtons, uint dwExtraInfo);
        [DllImport("user32.dll")]
        static extern bool PostMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool GetWindowRect(IntPtr hWnd, out RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }

        public static IntPtr FindChildWindow(IntPtr hwndParent, string lpszClass, string lpszTitle)
        {
            return FindChildWindow(hwndParent, IntPtr.Zero, lpszClass, lpszTitle);
        }
        public static IntPtr FindChildWindow(IntPtr hwndParent, IntPtr hwndChildAfter, string lpszClass, string lpszTitle)
        {
            // Try to find a match.
            IntPtr hwnd = FindWindowEx(hwndParent, IntPtr.Zero, lpszClass, lpszTitle);
            if (hwnd == IntPtr.Zero)
            {
                // Search inside the children.
                IntPtr hwndChild = FindWindowEx(hwndParent, IntPtr.Zero, null, null);
                while (hwndChild != IntPtr.Zero && hwnd == IntPtr.Zero)
                {
                    hwnd = FindChildWindow(hwndChild, IntPtr.Zero, lpszClass, lpszTitle);
                    if (hwnd == IntPtr.Zero)
                    {
                        // If we didn't find it yet, check the next child.
                        hwndChild = FindWindowEx(hwndParent, hwndChild, null, null);
                    }
                }
            }
            return hwnd;
        }
        #endregion

        #region Declare


        static int X_color = 1720;
        static int Y_color = 160;
        static int X_setting = 1840;
        static int Y_setting = 328;
        static int X_openfile = 72;
        static int Y_openfile = 62;
        static int Y_red = Y_color;
        static int Y_yellow = Y_color;
        static int Y_green = Y_color;
        static int Y_cyan = Y_color;
        static int Y_blue = Y_color;
        static int Y_pink = Y_color;
        static int Y_black = Y_color;
        static int Y_grey = Y_color;


        static int Y_tanso = Y_setting;
        static int Y_nangluong = Y_setting;
        static int Y_chieudaibuoc = Y_setting;
        static int Y_dotretrunggian = Y_setting;
        static int Y_dotretat = Y_setting;
        static int Y_dotremo = Y_setting;
        static int Y_nangluongucche = Y_setting;
        static int Y_solandanap = Y_setting;
        static int Y_tamdungthoigian = Y_setting;
        static int Y_lanlaplai = Y_setting;
        #endregion
        static DataTable Table_Settings = new DataTable();
        DataTable PLan_Table = new DataTable();
        private static void Reload_coordiante()
        {
            X_color = 1720;
            Y_color = 160;
            X_setting = 1840;
            Y_setting = 328;
            X_openfile = 72;
            Y_openfile = 62;
            Y_red = Y_color;
            Y_yellow = Y_color;
            Y_green = Y_color;
            Y_cyan = Y_color;
            Y_blue = Y_color;
            Y_pink = Y_color;
            Y_black = Y_color;
            Y_grey = Y_color;
            Y_tanso = Y_setting;
            Y_nangluong = Y_setting;
            Y_chieudaibuoc = Y_setting;
            Y_dotretrunggian = Y_setting;
            Y_dotretat = Y_setting;
            Y_dotremo = Y_setting;
            Y_nangluongucche = Y_setting;
            Y_solandanap = Y_setting;
            Y_tamdungthoigian = Y_setting;
            Y_lanlaplai = Y_setting;
        }
        static void Get_Coordinate_Color()
        {
            for (int i = 0; i < 8; i++)
            {
                switch (i)
                {
                    case 0:
                        Y_red = Y_color;
                        break;
                    case 1:
                        Y_yellow = Y_color + i * 16;
                        break;
                    case 2:
                        Y_green = Y_color + i * 16;
                        break;
                    case 3:
                        Y_cyan = Y_color + i * 16;
                        break;
                    case 4:
                        Y_blue = Y_color + i * 16;
                        break;
                    case 5:
                        Y_pink = Y_color + i * 16;
                        break;
                    case 6:
                        Y_black = Y_color + i * 16;
                        break;
                    case 7:
                        Y_grey = Y_color + i * 16;
                        break;
                    default:
                        break;
                }
            }
        }
        static void Get_Coordinate_Settings()
        {
            for (int i = 0; i < 10; i++)
            {
                switch (i)
                {
                    case 0:
                        Y_tanso = Y_setting;
                        break;
                    case 1:
                        Y_nangluong = Y_setting + i * 19;
                        break;
                    case 2:
                        Y_chieudaibuoc = Y_setting + i * 19;
                        break;
                    case 3:
                        Y_dotretrunggian = Y_setting + i * 19;
                        break;
                    case 4:
                        Y_dotretat = Y_setting + i * 19;
                        break;
                    case 5:
                        Y_dotremo = Y_setting + i * 19;
                        break;
                    case 6:
                        Y_nangluongucche = Y_setting + i * 19;
                        break;
                    case 7:
                        Y_solandanap = Y_setting + i * 19;
                        break;
                    case 8:
                        Y_tamdungthoigian = Y_setting + i * 19;
                        break;
                    case 9:
                        Y_lanlaplai = Y_setting + i * 19;
                        break;
                    default:
                        break;
                }
            }
        }
       
        //for testing
        void test()
        {
            IntPtr WindowHandle = FindWindow("TLaserMainFrm", null);

            if (WindowHandle == IntPtr.Zero)
            {
                MessageBox.Show("Ứng dụng không tồn tại");
            }
            else
            {
                SetForegroundWindow(WindowHandle);
                //int x = 0 + Convert.ToInt32(textBox4.Text);
                //int y = 0 + Convert.ToInt32(textBox5.Text);
                //SetCursorPos(X_openfile, Y_openfile);
                //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_openfile, Y_openfile, 0, 0);
                //Thread.Sleep(1000);

                IntPtr widow_open = FindWindow("#32770", "Open");
                if (widow_open != IntPtr.Zero)
                {
                    IntPtr window_edit = FindChildWindow(widow_open, IntPtr.Zero, "Edit", null);
                    SendMessage(window_edit, WM_SETTEXT, IntPtr.Zero, "S1.lms");
                }
                // Assume you want to select the 2nd item (index 1) in the listbox
                //int itemIndexToSelect = 1;
                //r window_edit = FindChildWindow(WindowHandle, IntPtr.Zero, "TlistBox", null);
                //SendMessage(window_edit, LB_SETCURSEL, (IntPtr)itemIndexToSelect, IntPtr.Zero);
                //PostMessage(window_edit, LB_SETCURSEL, itemIndexToSelect, 0);
                //// Post the left mouse button up message to the window
                ////PostMessage(WindowHandle, WM_LBUTTONUP, 0, lParam);
                //// Set your desired coordinates where you want to click

                //SetCursorPos(1720 , 164 );

                //// Perform a left mouse click at the specified coordinates
                //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
                //SetCursorPos(1820, 330);
                //mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, x, y, 0, 0);
                //SendKeys.SendWait("{ENTER}");
                //SendKeys.SendWait("11.00");
                //ShowClickPoint(x, y);
            }
        }
        private static void Move_2_Red()
        {
            SetCursorPos(X_color, Y_red);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_color, Y_red, 0, 0);
        }
        private static void Move_2_Yellow()
        {
            SetCursorPos(X_color, Y_yellow);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_color, Y_yellow, 0, 0);

        }
        private static void Move_2_Cyan()
        {
            SetCursorPos(X_color, Y_cyan);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_color, Y_cyan, 0, 0);
        }
        private static void Move_2_Green()
        {
            SetCursorPos(X_color, Y_green);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_color, Y_green, 0, 0);
        }
        private static void Move_2_Pink()
        {
            SetCursorPos(X_color, Y_pink);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_color, Y_pink, 0, 0);
        }
        private static void Move_2_Grey()
        {
            SetCursorPos(X_color, Y_grey);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_color, Y_grey, 0, 0);
        }
        private static void Move_2_Blue()
        {
            SetCursorPos(X_color, Y_blue);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_color, Y_blue, 0, 0);
        }
        private static void Move_2_Black()
        {
            SetCursorPos(X_color, Y_black);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_color, Y_black, 0, 0);

        }


        private static void Move_2_Tanso(string param)
        {
            SetCursorPos(X_setting, Y_tanso);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_setting, Y_tanso, 0, 0);
            SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(param);
        }
        private static void Move_2_NangLuong(string param)
        {
            SetCursorPos(X_setting, Y_nangluong);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_setting, Y_nangluong, 0, 0);
            //SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(param);
        }
        private static void Move_2_ChieuDaiBuoc(string param)
        {
            SetCursorPos(X_setting, Y_chieudaibuoc);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_setting, Y_chieudaibuoc, 0, 0);
            //SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(param);
        }
        private static void Move_2_DoTreTrungGian(string param)
        {
            SetCursorPos(X_setting, Y_dotretrunggian);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_setting, Y_dotretrunggian, 0, 0);
            //SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(param);
        }
        private static void Move_2_DoTreTat(string param)
        {
            SetCursorPos(X_setting, Y_dotretat);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_setting, Y_dotretat, 0, 0);
            //SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(param);
        }
        private static void Move_2_DoTreMo(string param)
        {
            SetCursorPos(X_setting, Y_dotremo);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_setting, Y_dotremo, 0, 0);
            //SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(param);
        }
        private static void Move_2_NangLuongUcChe(string param)
        {
            SetCursorPos(X_setting, Y_nangluongucche);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_setting, Y_nangluongucche, 0, 0);
            //SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(param);
        }
        private static void Move_2_SolanDanAp(string param)
        {
            SetCursorPos(X_setting, Y_solandanap);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_setting, Y_solandanap, 0, 0);
            //SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(param);
        }
        private static void Move_2_TamDungThoiGian(string param)
        {
            SetCursorPos(X_setting, Y_tamdungthoigian);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_setting, Y_tamdungthoigian, 0, 0);
            //SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(param);
        }
        private static void Move_2_LanLapLai(string param)
        {
            SetCursorPos(X_setting, Y_lanlaplai);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_setting, Y_lanlaplai, 0, 0);
            //SendKeys.SendWait("{ENTER}");
            SendKeys.SendWait(param);
        }

        private static void Move_2_Openfile(string filename)
        {
            IntPtr WindowHandle = FindWindow("TLaserMainFrm", null);
            RECT windowRect = new RECT();
            GetWindowRect(WindowHandle, out windowRect);
            SetForegroundWindow(WindowHandle);
            SetCursorPos(X_openfile + windowRect.Left, Y_openfile + windowRect.Top + 8);
            mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_openfile + windowRect.Left, Y_openfile + windowRect.Top, 0, 0);
            Thread.Sleep(1000);
            IntPtr widow_open = FindWindow("#32770", "Open");
            SetForegroundWindow(widow_open);
            if (widow_open != IntPtr.Zero)
            {
                IntPtr window_edit = FindChildWindow(widow_open, IntPtr.Zero, "Edit", null);
                SendMessage(window_edit, WM_SETTEXT, IntPtr.Zero, ConfigApp.Filepath+@"\"+filename);
                IntPtr window_btn_open = FindChildWindow(widow_open, IntPtr.Zero, "Button", "&Open");
                SendMessage(window_btn_open, BN_CLICKED, IntPtr.Zero, null);
            }
        }
        /// <summary>
        /// Select Option Setting
        /// </summary>
        /// <param name="color">Params type</param>
        private static void Apply_Settings(string color)
        {
            Reload_coordiante();
            IntPtr WindowHandle = FindWindow("TLaserMainFrm", null);
            RECT windowRect = new RECT();
            GetWindowRect(WindowHandle, out windowRect);
            SetForegroundWindow(WindowHandle);
            X_color = X_color + windowRect.Left;
            Y_color = Y_color + windowRect.Top + 8;
            X_setting = X_setting + windowRect.Left;
            Y_setting = Y_setting + windowRect.Top + 8; 
            Get_Coordinate_Color();
            Get_Coordinate_Settings();
            if (WindowHandle == IntPtr.Zero)
            {
                MessageBox.Show("Ứng dụng không tồn tại");
            }
            else
            {
                SetForegroundWindow(WindowHandle);
                switch (color)
                {
                    case "Red":
                        Move_2_Red();
                        Thread.Sleep(200);
                        Move_2_Tanso(Table_Settings.Rows[0][2].ToString());
                        Move_2_NangLuong(Table_Settings.Rows[0][3].ToString());
                        Move_2_ChieuDaiBuoc(Table_Settings.Rows[0][4].ToString());
                        Move_2_DoTreTrungGian(Table_Settings.Rows[0][5].ToString());
                        Move_2_DoTreTat(Table_Settings.Rows[0][6].ToString());
                        Move_2_DoTreMo(Table_Settings.Rows[0][7].ToString());
                        Move_2_NangLuongUcChe(Table_Settings.Rows[0][8].ToString());
                        Move_2_SolanDanAp(Table_Settings.Rows[0][9].ToString());
                        Move_2_TamDungThoiGian(Table_Settings.Rows[0][10].ToString());
                        Move_2_LanLapLai(Table_Settings.Rows[0][11].ToString());
                        Thread.Sleep(400);
                        Click_Apply();
                        break;
                    case "Yellow":
                        Move_2_Yellow();
                        Thread.Sleep(200);
                        Move_2_Tanso(Table_Settings.Rows[1][2].ToString());
                        Move_2_NangLuong(Table_Settings.Rows[1][3].ToString());
                        Move_2_ChieuDaiBuoc(Table_Settings.Rows[1][4].ToString());
                        Move_2_DoTreTrungGian(Table_Settings.Rows[1][5].ToString());
                        Move_2_DoTreTat(Table_Settings.Rows[1][6].ToString());
                        Move_2_DoTreMo(Table_Settings.Rows[1][7].ToString());
                        Move_2_NangLuongUcChe(Table_Settings.Rows[1][8].ToString());
                        Move_2_SolanDanAp(Table_Settings.Rows[1][9].ToString());
                        Move_2_TamDungThoiGian(Table_Settings.Rows[1][10].ToString());
                        Move_2_LanLapLai(Table_Settings.Rows[1][11].ToString());
                        Thread.Sleep(400);
                        Click_Apply();
                        break;
                    case "Green":
                        Move_2_Green();
                        Thread.Sleep(200);
                        Move_2_Tanso(Table_Settings.Rows[2][2].ToString());
                        Move_2_NangLuong(Table_Settings.Rows[2][3].ToString());
                        Move_2_ChieuDaiBuoc(Table_Settings.Rows[2][4].ToString());
                        Move_2_DoTreTrungGian(Table_Settings.Rows[2][5].ToString());
                        Move_2_DoTreTat(Table_Settings.Rows[2][6].ToString());
                        Move_2_DoTreMo(Table_Settings.Rows[2][7].ToString());
                        Move_2_NangLuongUcChe(Table_Settings.Rows[2][8].ToString());
                        Move_2_SolanDanAp(Table_Settings.Rows[2][9].ToString());
                        Move_2_TamDungThoiGian(Table_Settings.Rows[2][10].ToString());
                        Move_2_LanLapLai(Table_Settings.Rows[2][11].ToString());
                        Thread.Sleep(400);
                        Click_Apply();
                        break;
                    case "Cyan":
                        Move_2_Cyan();
                        Thread.Sleep(200);
                        Move_2_Tanso(Table_Settings.Rows[3][2].ToString());
                        Move_2_NangLuong(Table_Settings.Rows[3][3].ToString());
                        Move_2_ChieuDaiBuoc(Table_Settings.Rows[3][4].ToString());
                        Move_2_DoTreTrungGian(Table_Settings.Rows[3][5].ToString());
                        Move_2_DoTreTat(Table_Settings.Rows[3][6].ToString());
                        Move_2_DoTreMo(Table_Settings.Rows[3][7].ToString());
                        Move_2_NangLuongUcChe(Table_Settings.Rows[3][8].ToString());
                        Move_2_SolanDanAp(Table_Settings.Rows[3][9].ToString());
                        Move_2_TamDungThoiGian(Table_Settings.Rows[3][10].ToString());
                        Move_2_LanLapLai(Table_Settings.Rows[3][11].ToString());
                        Thread.Sleep(400);
                        Click_Apply();
                        break;
                    case "Blue":
                        Move_2_Blue();
                        Thread.Sleep(200);
                        Move_2_Tanso(Table_Settings.Rows[4][2].ToString());
                        Move_2_NangLuong(Table_Settings.Rows[4][3].ToString());
                        Move_2_ChieuDaiBuoc(Table_Settings.Rows[4][4].ToString());
                        Move_2_DoTreTrungGian(Table_Settings.Rows[4][5].ToString());
                        Move_2_DoTreTat(Table_Settings.Rows[4][6].ToString());
                        Move_2_DoTreMo(Table_Settings.Rows[4][7].ToString());
                        Move_2_NangLuongUcChe(Table_Settings.Rows[4][8].ToString());
                        Move_2_SolanDanAp(Table_Settings.Rows[4][9].ToString());
                        Move_2_TamDungThoiGian(Table_Settings.Rows[4][10].ToString());
                        Move_2_LanLapLai(Table_Settings.Rows[4][11].ToString());
                        Thread.Sleep(400);
                        Click_Apply();
                        break;
                    case "Pink":
                        Move_2_Pink();
                        Thread.Sleep(200);
                        Move_2_Tanso(Table_Settings.Rows[5][2].ToString());
                        Move_2_NangLuong(Table_Settings.Rows[5][3].ToString());
                        Move_2_ChieuDaiBuoc(Table_Settings.Rows[5][4].ToString());
                        Move_2_DoTreTrungGian(Table_Settings.Rows[5][5].ToString());
                        Move_2_DoTreTat(Table_Settings.Rows[5][6].ToString());
                        Move_2_DoTreMo(Table_Settings.Rows[5][7].ToString());
                        Move_2_NangLuongUcChe(Table_Settings.Rows[5][8].ToString());
                        Move_2_SolanDanAp(Table_Settings.Rows[5][9].ToString());
                        Move_2_TamDungThoiGian(Table_Settings.Rows[5][10].ToString());
                        Move_2_LanLapLai(Table_Settings.Rows[5][11].ToString());
                        Thread.Sleep(400);
                        Click_Apply();
                        break;
                    case "Black":
                        Move_2_Black();
                        Thread.Sleep(200);
                        Move_2_Tanso(Table_Settings.Rows[6][2].ToString());
                        Move_2_NangLuong(Table_Settings.Rows[6][3].ToString());
                        Move_2_ChieuDaiBuoc(Table_Settings.Rows[6][4].ToString());
                        Move_2_DoTreTrungGian(Table_Settings.Rows[6][5].ToString());
                        Move_2_DoTreTat(Table_Settings.Rows[6][6].ToString());
                        Move_2_DoTreMo(Table_Settings.Rows[6][7].ToString());
                        Move_2_NangLuongUcChe(Table_Settings.Rows[6][8].ToString());
                        Move_2_SolanDanAp(Table_Settings.Rows[6][9].ToString());
                        Move_2_TamDungThoiGian(Table_Settings.Rows[6][10].ToString());
                        Move_2_LanLapLai(Table_Settings.Rows[6][11].ToString());
                        Thread.Sleep(400);
                        Click_Apply();
                        break;
                    case "Grey":
                        Move_2_Grey();
                        Thread.Sleep(200);
                        Move_2_Tanso(Table_Settings.Rows[7][2].ToString());
                        Move_2_NangLuong(Table_Settings.Rows[7][3].ToString());
                        Move_2_ChieuDaiBuoc(Table_Settings.Rows[7][4].ToString());
                        Move_2_DoTreTrungGian(Table_Settings.Rows[7][5].ToString());
                        Move_2_DoTreTat(Table_Settings.Rows[7][6].ToString());
                        Move_2_DoTreMo(Table_Settings.Rows[7][7].ToString());
                        Move_2_NangLuongUcChe(Table_Settings.Rows[7][8].ToString());
                        Move_2_SolanDanAp(Table_Settings.Rows[7][9].ToString());
                        Move_2_TamDungThoiGian(Table_Settings.Rows[7][10].ToString());
                        Move_2_LanLapLai(Table_Settings.Rows[7][11].ToString());
                        Thread.Sleep(400);
                        Click_Apply();
                        break;
                    default:
                        MessageBox.Show("Setting invalid");
                        break;
                }
            }
        }

        //Read setting parameter from csv file
        private void ReadSetting()
        {
            try
            {
                //string path = Root_Path + @"\lazerconfig.csv";
                //Table_Settings = ReadCSV(path);
                Table_Settings = DatabaseExcute_Main.LoadConfig();
                dataGridView1.DataSource = Table_Settings;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }

        //static DataTable ReadCSV(string filePath)
        //{
        //    DataTable dataTable = new DataTable();

        //    using (StreamReader reader = new StreamReader(filePath))
        //    {
        //        bool isFirstLine = true;
        //        while (!reader.EndOfStream)
        //        {
        //            string[] data = reader.ReadLine().Split(',');
        //            if (isFirstLine)
        //            {
        //                foreach (string header in data)
        //                {
        //                    dataTable.Columns.Add(new DataColumn(header));
        //                }
        //                isFirstLine = false;
        //            }
        //            else
        //            {
        //                DataRow row = dataTable.NewRow();
        //                row.ItemArray = data;
        //                dataTable.Rows.Add(row);
        //            }
        //        }
        //    }

        //    return dataTable;
        //}

        //void WriteDataTableToCSV(string filePath)
        //{
        //    using (StreamWriter writer = new StreamWriter(filePath))
        //    {
        //        // Write column headers
        //        foreach (DataColumn column in Table_Settings.Columns)
        //        {
        //            writer.Write(column.ColumnName);
        //            if (column.Ordinal < Table_Settings.Columns.Count - 1)
        //            {
        //                writer.Write(",");
        //            }
        //        }
        //        writer.WriteLine();

        //        // Write data rows
        //        foreach (DataRow row in Table_Settings.Rows)
        //        {
        //            for (int i = 0; i < Table_Settings.Columns.Count; i++)
        //            {
        //                writer.Write(row[i].ToString());
        //                if (i < Table_Settings.Columns.Count - 1)
        //                {
        //                    writer.Write(",");
        //                }
        //            }
        //            writer.WriteLine();
        //        }
        //    }
        //}
        static void ShowClickPoint(int x, int y)
        {
            using (Graphics g = Graphics.FromHwnd(IntPtr.Zero)) // Graphics object to draw on the screen
            {
                // Draw a red circle as a marker at the clicked point
                using (Pen pen = new Pen(Color.Red, 2))
                {
                    g.DrawEllipse(pen, x - 5, y - 5, 10, 10); // Adjust the size and shape as needed
                }
            }
        }
        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// After Scan Barcode then call this method to open file
        /// </summary>
        /// <param name="filename">File Name is mentioned in filename field</param>
        /// <param name="color">configuration from paper </param>
        public static void OpenCutingFile(string filename, string color) 
        {
            Task.Run(() =>
            {
                Move_2_Openfile(filename);
                Task.Delay(1000);
                Apply_Settings(color);
            });
           

        }
        private static void Click_Apply(/*IntPtr handle*/)
        {
            IntPtr WindowHandle = FindWindow("TLaserMainFrm", null);
            IntPtr window_btn_open = FindChildWindow(WindowHandle, IntPtr.Zero, "TButton", "应用");
            SendMessage(window_btn_open, BN_CLICKED, IntPtr.Zero, null);
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //string path = Root_Path + @"\lazerconfig.csv";
            //WriteDataTableToCSV(path);
        }

        private void filePathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using(FolderBrowserDialog openFileDialog = new FolderBrowserDialog()) 
            {
                if(openFileDialog.ShowDialog() == DialogResult.OK) 
                {
                    ConfigApp.Filepath = openFileDialog.SelectedPath;
                    UpdateConfig();
                }
            }
        }
        private void LoadConfig()
        {
            try
            {
                if (!File.Exists(Fileconfig))
                {
                    File.Create(Fileconfig).Close();
                    ConfigApp = new ConfigApp();
                    ConfigApp.Filepath = Root_Path;

                    // add some properties
                    string json = JsonConvert.SerializeObject(ConfigApp, Formatting.Indented);
                    File.WriteAllText(Fileconfig, json);
                }
                else
                {
                    string json = File.ReadAllText(Fileconfig);
                    ConfigApp = JsonConvert.DeserializeObject<ConfigApp>(json);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error [LoadConfig]"+Environment.NewLine+ex.Message);
            }
        }
        private void UpdateConfig() 
        {
            try
            {
                string json = JsonConvert.SerializeObject(ConfigApp, Formatting.Indented);
                File.WriteAllText(Fileconfig, json);
            }
            catch (Exception ex)
            {

                MessageBox.Show("Error [UpdateConfig]"+Environment.NewLine+ex.Message);
            }
           
        }

        /// <summary>
        /// Open Lazer Cutting Software 
        /// </summary>
        public static void OpenLazer() 
        {
            var processes = Process.GetProcesses();
            bool lazer_running = false;
            foreach (var item in processes)
            {
                if (item.ProcessName.Equals("el.sunic.chs.exe", StringComparison.CurrentCultureIgnoreCase))
                {
                    lazer_running = true;
                    break;
                }
            }

            if (!lazer_running) 
            {
                if (File.Exists(ConfigApp.LazerApp_Path)) 
                {
                    Process.Start(ConfigApp.LazerApp_Path);
                }
                else
                {
                    MessageBox.Show("Error [Run Lazer Application]"+Environment.NewLine+"Cannot file Application Path");
                }
            }
        }
        private bool Check_Running_Lazer_App() 
        {
            var processes = Process.GetProcesses();
            bool lazer_running = false;
            foreach (var item in processes)
            {
                if (item.ProcessName.Equals("el.sunic.chs.exe", StringComparison.CurrentCultureIgnoreCase))
                {
                    lazer_running = true;
                    break;
                }
            }
            if (!lazer_running) 
            {
                // Show notification
                return false;
            }
            else 
            {
                return true;
            }
        }

        private void openLazerApplicaitonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!Check_Running_Lazer_App()) 
            {
                OpenLazer();
            }
            else 
            {
                MessageBox.Show("Lazer Software is running");
            }
        }

        private void laserPathToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                using (OpenFileDialog openFileDialog = new OpenFileDialog())
                {
                    if (openFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        ConfigApp.LazerApp_Path = openFileDialog.FileName;
                        UpdateConfig();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error [Select Applicaiton]" + Environment.NewLine + ex.Message);
            }
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void import_btn_Click(object sender, EventArgs e)
        {
            //try
            //{
            //    using (OpenFileDialog openFileDialog = new OpenFileDialog())
            //    {
            //        if (openFileDialog.ShowDialog() == DialogResult.OK)
            //        {
            //            PLan_Table = ReadCSV(openFileDialog.FileName);
            //            dataGridView2.DataSource = PLan_Table;
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Import Error: "+ Environment.NewLine + ex.Message);
            //}
        }

        private void apply_btn_Click(object sender, EventArgs e)
        {
            try
            {
                if(PLan_Table == null || PLan_Table.Rows.Count < 1)
                {
                    MessageBox.Show("Plan is not imported");
                }
                else
                {
                    SQLDBController.ApplyTable(PLan_Table);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                MessageBox.Show("Apply Plan Table Error "+ Environment.NewLine+ex.Message);
            }
        }

        private void Edii_setting_btn_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = true;
        }

        private void Ok_setting_btn_Click(object sender, EventArgs e)
        {
            dataGridView1.Enabled = false;
        }
        private void Timer_second_Tick(object sender, EventArgs e)
        {
            

        }
    }

    public class Settings
    {
        public float Tanso { get; set; } //频率 Khz
        public float Nangluong { get; set; }// %
        public float ChieuDaiBuoc { get; set; } //%mm
        public float Dotretrunggian { get; set; }//us
        public float DoTreTat { get; set; }//us
        public float DoTreMo { get; set; }//us
        public float NangLuongUcChe { get; set; }//%
        public int SoLanDanAp { get; set; }
        public float TamDungThoiGian { get; set; }//s
        public int LanLapLai { get; set; }
    }
    public class ConfigApp 
    {
        public string Filepath { get; set; }
        public string LazerApp_Path { get; set; }
    }
}
