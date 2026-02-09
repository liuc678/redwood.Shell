using CefSharp;
using CefSharp.Structs;
using redwood.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace redwood.shell.Handle
{
    public class CustomFindHandler : IFindHandler
    {

        public BrowserForm Form;   

        public void OnFindResult(IWebBrowser chromiumWebBrowser, IBrowser browser, int identifier, int count, Rect selectionRect, int activeMatchOrdinal, bool finalUpdate)
        {
            this.Form.SetSearchResult(count, activeMatchOrdinal);
        }
    }
}
