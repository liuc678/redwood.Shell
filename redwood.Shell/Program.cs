using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using System.IO;
using System.Reflection;

namespace redwood.Shell
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        public static void Main()
        {
            try
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);

                AppDomain.CurrentDomain.AssemblyResolve += Resolver;


                //Monitor parent process exit and close subprocesses if parent process exits first
                //This will at some point in the future becomes the default
                CefSharpSettings.SubprocessExitIfParentProcessClosed = true;

                //For Windows 7 and above, best to include relevant app.manifest entries as well
                //Cef.EnableHighDPISupport();


                var settings = new CefSettings();
                //{
                //    //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                //    CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")

                //};
                settings.BrowserSubprocessPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                  Environment.Is64BitProcess ? "x64" : "x86",
                                                  "CefSharp.BrowserSubprocess.exe");

                settings.CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache");
                //Example of setting a command line argument
                //Enables WebRTC
                //settings.CefCommandLineArgs.Add("enable-media-stream", "1");

                //Perform dependency check to make sure all relevant resources are in our output directory.
                Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
            }
            catch (Exception e)
            {
                JsEvent.WriteLog(e.Message + "\n" + e.Source + "\n" + e.StackTrace);
                // MessageBox.Show(e.Message);                
            }
            var browser = new BrowserForm();
            Application.Run(browser);

            Cef.Shutdown();
        }
       

        // Will attempt to load missing assembly from either x86 or x64 subdir
        private static Assembly Resolver(object sender, ResolveEventArgs args)
        {
            if (args.Name.StartsWith("CefSharp"))
            {
                string assemblyName = args.Name.Split(new[] { ',' }, 2)[0] + ".dll";
                string archSpecificPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                       Environment.Is64BitProcess ? "x64" : "x86",
                                                       assemblyName);

                return File.Exists(archSpecificPath)
                           ? Assembly.LoadFile(archSpecificPath)
                           : null;
            }

            return null;
        }
    }
}

