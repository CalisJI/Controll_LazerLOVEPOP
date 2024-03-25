# ControlLazerApp

- [Microsoft.AspNet.WebApi.Owin](https://dotnet.microsoft.com/en-us/apps/aspnet/apis)
- [Microsoft.AspNet.WebApi.OwinSelfHost](https://dotnet.microsoft.com/en-us/apps/aspnet/apis)

# API 
- using RESTFul API to communicate with another application like (Node-red, web API, etc...)
- Using These NuGet: Microsoft.AspNet.WebApi.Owin; Microsoft.Owin.Host.HttpListener; Microsoft.Owin.Hosting; Microsoft.AspNet.WebApi.OwinSelfHost

# Define a selfhost server to listen API requests from clients
```csharp 

// Define a self server host to lister request
var config = new HttpSelfHostConfiguration("http://0.0.0.0:5001"); // open port 5001
config.Routes.MapHttpRoute("API Default", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional, action = RouteParameter.Optional });
var server = new HttpSelfHostServer(config);
server.OpenAsync().Wait();


```
# Define a ResultAction to handle Post Request from client
```csharp
public class OrderController:ApiController
{
    [Route("PostStatus")]
    [HttpPost]
    public IHttpActionResult PostStatus([FromBody] OrderInfor data) 
    {
        if(data != null) 
        {
            Form1.OpenCutingFile(data.File_name, data.Color);
            return Ok();
        }
        else 
        {
            return BadRequest("Null Data");
        }
    }
}
// Object Data using for communication
public class OrderInfor 
{
    public string MO_Code { get; set; }
    public string LP_Code { get; set; }
    public string File_name { get; set; }
    public string Cutting_Code { get; set; }
    public string Machine { get; set; }
    public string Color { get; set; }
    public string Quantity { get; set; }
}
```
# ZeroMQ
- Using to subscribe and publish message through each node applications in local network
- Using this NuGet: [NetMQ](https://netmq.readthedocs.io/en/latest/pub-sub/)
```csharp
//Subscriber --- use in subscriber mode (Workstation mode)
public static void Initialize_Subcriber(string topic) 
{
    Task.Run(() =>
    {
        using (var subscriber = new SubscriberSocket())
        {
            subscriber.Connect("tcp://127.0.0.1:5556");
            subscriber.Subscribe(topic);

            while (true)
            {
                topic = subscriber.ReceiveFrameString();
                var msg = subscriber.ReceiveFrameString();
                Debug.WriteLine("From Publisher: {0} {1}", topic, msg);
            }
        }
    });
}
```
```csharp
//Publisher use in Plan option
/// <summary>
/// Publish message to subscribers in network (Workstation node)
/// </summary>
/// <param name="mQMessage"></param>
public static async Task Publisher_MQ(MQMessage mQMessage) 
{
    using (var publisher = new PublisherSocket())
    {
        publisher.Bind("tcp://*:5556");
        await Task.Delay(500);
        if (mQMessage!=null && mQMessage.Topic!=null && mQMessage.Content!=null) 
        {
            publisher
                .SendMoreFrame(mQMessage.Topic) // Topic
                .SendFrame(mQMessage.Content); // Message 
        }
    }
}
```

# Working with Database
- Using DatabaseExcute_Main

# Necessary User32.DLL for application
```csharp
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
```
# Funtion to get coordinations of LOVEPOP Lazer software
```csharp
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
```

# Move to Position option for instance
```csharp
//Move to Red option then send click
private static void Move_2_Red()
{
    SetCursorPos(X_color, Y_red);
    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_color, Y_red, 0, 0);
}
//Move to row for enter Frequent parameter then send enter input
private static void Move_2_Tanso(string param)
{
    SetCursorPos(X_setting, Y_tanso);
    mouse_event(MOUSEEVENTF_LEFTDOWN | MOUSEEVENTF_LEFTUP, X_setting, Y_tanso, 0, 0);
    SendKeys.SendWait("{ENTER}");
    SendKeys.SendWait(param);
}

// Simulate click open file
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
```
# Reference
[Video-HowKteam](https://howkteam.vn/course/dieu-khien-ung-dung-pc-voi-c-50)
