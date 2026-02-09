using CefSharp;
using CefSharp.WinForms;
using System;
using System.IO;
using System.Windows.Forms;

namespace redwood.shell.Handle
{
    public static class MyCersharpHelp
    {
        public static ChromiumWebBrowser GetChromiumWeb()
        {
            return null;
        }

        public static bool InitCerSharp()
        {
            //CefSharpSettings.SubprocessExitIfParentProcessClosed = true;


            //var settings = new CefSettings();
            //settings.CefCommandLineArgs.Add("disable-web-security", "1");//关闭同源策略,允许跨域
            //settings.CefCommandLineArgs.Add("disable-site-isolation-trials", "1");//关闭站点隔离策略,允许跨域

            //settings.CachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Cache");

            ////settings.CefCommandLineArgs.Add("allow-running-insecure-content", "1");

            ////settings.CefCommandLineArgs.Add("use-fake-ui-for-media-stream", "1");

            ////settings.CefCommandLineArgs.Add("enable-speech-input", "1");  //语音输入

            ////settings.CefCommandLineArgs.Add("enable-usermedia-screen-capture", "1");                

            ////settings.CefCommandLineArgs.Add("allow-outdated-plugins", "1");


            ////settings.CefCommandLineArgs.Add("always-authorize-plugins", "1");                
            ////settings.CefCommandLineArgs.Add("allow-running-insecure-content", "1");
            ////settings.CefCommandLineArgs.Add("enable-npapi", "1");
            //settings.CefCommandLineArgs.Add("autoplay-policy", "no-user-gesture-required");
            //settings.CefCommandLineArgs.Add("enable-media-stream", "1");
            //settings.CefCommandLineArgs.Add("allow-running-insecure-content", "1");
            //settings.CefCommandLineArgs.Add("enable-speech-input", "1");

            //Cef.Initialize(settings, performDependencyCheck: true, browserProcessHandler: null);
            var settings = new CefSettings()
            {
                Locale = "zh-CN",
                AcceptLanguageList = "zh-CN,zh;q=0.8",
                PersistSessionCookies = true,
                UserAgent = "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/89.0 Safari/537.36",
                IgnoreCertificateErrors = true,
                LogSeverity = LogSeverity.Disable, //禁用日志
                //为空则将以“隐身模式”创建浏览器
                //CachePath = AppDomain.CurrentDomain.BaseDirectory + "cef_cache",
                //Windows上用户“Local Settings\Application Data\CEF\User Data”目录
                RootCachePath = CachePath,
                
                //RootCachePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CefSharp\\Temp")                                                
            };
            settings.CachePath = settings.RootCachePath + "\\cache";
            settings.CefCommandLineArgs.Add("disable-gpu", "1"); // 禁用gpu
            settings.CefCommandLineArgs.Add("no-proxy-server", "1"); //禁用代理
            settings.CefCommandLineArgs.Add("proxy-auto-detect", "0"); //禁用代理
                                                                       
            settings.CefCommandLineArgs.Add("--disable-web-security", "1"); //允许跨域
            settings.CefCommandLineArgs.Add("--ignore-urlfetcher-cert-requests", "1");//忽略安全证书
            settings.CefCommandLineArgs.Add("--ignore-certificate-errors", "1");//忽略安全证书
            settings.CefCommandLineArgs.Add("enable-media-stream", "1"); //允许webRTC
            //settings.CefCommandLineArgs["enable-system-flash"] = "1";
            //settings.CefCommandLineArgs.Add("ppapi-flash-version", "32.0.0.171");
            //settings.CefCommandLineArgs.Add("ppapi-flash-path", @"plugins\pepflashplayer.dll");

            //Cef.EnableHighDPISupport();
            //CefSharpSettings.ConcurrentTaskExecution = true;
            //CefSharpSettings.LegacyJavascriptBindingEnabled = true;
            //CefSharpSettings.WcfEnabled = true;

            return Cef.Initialize(settings);
        }

        public static string CachePath
        {
            get
            {
                return AppDomain.CurrentDomain.BaseDirectory + "cef_cache";
            }
        }
    }
}
