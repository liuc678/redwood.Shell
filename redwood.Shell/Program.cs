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
                //JsEvent.WriteLog(Environment.Is64BitProcess ? "x64" : "x86");

                //Monitor parent process exit and close subprocesses if parent process exits first
                //This will at some point in the future becomes the default
                CefSharpSettings.SubprocessExitIfParentProcessClosed = true;

                //For Windows 7 and above, best to include relevant app.manifest entries as well
                //Cef.EnableHighDPISupport();


                var settings = new CefSettings();
                {
                    //By default CefSharp will use an in-memory cache, you need to specify a Cache Folder to persist data
                    // CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache")

                };
                settings.BrowserSubprocessPath = Path.Combine(AppDomain.CurrentDomain.SetupInformation.ApplicationBase,
                                                  Environment.Is64BitProcess ? "x64" : "x86",
                                                  "CefSharp.BrowserSubprocess.exe");
                settings.CefCommandLineArgs.Add("disable-web-security", "1");//关闭同源策略,允许跨域
                settings.CefCommandLineArgs.Add("disable-site-isolation-trials", "1");//关闭站点隔离策略,允许跨域

                settings.CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache");
                //Example of setting a command line argument
                //Enables WebRTC

                //settings.CefCommandLineArgs.Add("allow-running-insecure-content", "1");

                //settings.CefCommandLineArgs.Add("use-fake-ui-for-media-stream", "1");

                //settings.CefCommandLineArgs.Add("enable-speech-input", "1");  //语音输入

                //settings.CefCommandLineArgs.Add("enable-usermedia-screen-capture", "1");                

                //settings.CefCommandLineArgs.Add("allow-outdated-plugins", "1");

                //settings.CefCommandLineArgs.Add("always-authorize-plugins", "1");                
                //settings.CefCommandLineArgs.Add("allow-running-insecure-content", "1");
                //settings.CefCommandLineArgs.Add("enable-npapi", "1");
                settings.CefCommandLineArgs.Add("autoplay-policy", "no-user-gesture-required");
                settings.CefCommandLineArgs.Add("enable-media-stream", "1");
                settings.CefCommandLineArgs.Add("allow-running-insecure-content", "1");
                settings.CefCommandLineArgs.Add("enable-speech-input", "1");
                //Perform dependency check to make sure all relevant resources are in our output directory.
                Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);


                //var browser = new BrowserForm();
                //Application.Run(browser);
                Application.Run(BrowserForm.Current);

               RenameFile("红森软件ERP_1.exe", "红森软件ERP.exe");                
               RenameFile("BaseClass_1.dll", "BaseClass.dll");
            }
            catch (Exception e)
            {
                JsEvent.WriteLog(e.Message + "\n" + e.Source + "\n" + e.StackTrace);
                MessageBox.Show("系统运行异常，详情查看log.txt,当前程序运行在：" + (Environment.Is64BitProcess ? "x64" : "x86"), "提示信息");
            }            
            Cef.Shutdown();
        }

        static bool RenameFile(string srcFileName,string destFileName)
        {
            string autofilename_new = Path.Combine(Application.StartupPath, srcFileName);
            if (File.Exists(autofilename_new))
            {
                string autofilename_old = Path.Combine(Application.StartupPath, destFileName);
                try
                {
                    File.Copy(autofilename_new, autofilename_old, true);
                    File.Delete(autofilename_new);
                }
                catch (Exception E)
                {
                    JsEvent.WriteLog("更新【红森软件ERP.exe】失败" + E.Message + E.StackTrace);
                    return false;
                }
            }
            return true;
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
                //JsEvent.WriteLog(assemblyName + "\n" + archSpecificPath);
                return File.Exists(archSpecificPath)
                           ? Assembly.LoadFile(archSpecificPath)
                           : null;
            }

            return null;
        }
    }
}

