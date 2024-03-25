using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Http.SelfHost;
using System.Web.Http;
using System.Windows.Forms;


namespace ControlLazerApp
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            var config = new HttpSelfHostConfiguration("http://0.0.0.0:5001");
            config.Routes.MapHttpRoute("API Default", "api/{controller}/{action}/{id}", new { id = RouteParameter.Optional, action = RouteParameter.Optional });
            var server = new HttpSelfHostServer(config);
            server.OpenAsync().Wait();
            Application.Run(new Form1());
        }
    }
}
