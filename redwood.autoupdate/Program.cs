using BaseClass;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace redwood.autoupdate
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] arg)
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Log.FileName = Path.Combine(Application.StartupPath, "log.txt");
            if (arg.Length > 0 && arg[0] == "admin")
            //if(true)
            {

                Application.Run(new frmAdmin());
            }
            else
            {
                Application.Run(new Form1());
            }
        }
    }
}
