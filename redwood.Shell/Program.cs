using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using CefSharp.WinForms;
using CefSharp;
using System.IO;
using System.Reflection;
using redwood.shell.Handle;

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

                MyCersharpHelp.InitCerSharp();

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

