using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseClass
{
    public static class Log
    {
        public static string FileName;

        public static void Write(string msg)
        {            
            File.AppendAllText(FileName, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "------------\n" + msg + "\r\n");
        }

        public static void Write(Exception E,string msg = "")
        {
            var txt = new System.Text.StringBuilder();
            txt.AppendLine(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"));
            if(!string.IsNullOrEmpty(msg))
            {
                txt.AppendLine(msg);
            }
           
            txt.AppendLine(E.Message);            
            txt.AppendLine(E.Source);
            txt.AppendLine(E.StackTrace);
            txt.AppendLine();
            File.AppendAllText(FileName,txt.ToString());
        }
    }
}
