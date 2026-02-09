using CefSharp;
using CefSharp.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redwood.shell.Handle
{
    public class MyRequestHandler : RequestHandler
    {
        protected override bool OnBeforeBrowse(IWebBrowser chromiumWebBrowser, IBrowser browser, IFrame frame, IRequest request, bool userGesture,
         bool isRedirect)
        {
            // 先调用基类的实现，断点调试
            if (request.Url.StartsWith("file", StringComparison.InvariantCultureIgnoreCase))
            {
                // 阻止导航
                return true;
            }

            // 允许其他导航
            return base.OnBeforeBrowse(chromiumWebBrowser, browser, frame, request, userGesture, isRedirect);
        }

    }
}
