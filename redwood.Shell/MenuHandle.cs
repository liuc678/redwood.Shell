using CefSharp;
using CefSharp.WinForms;

namespace redwood.Shell
{
    public class MenuHandler : IContextMenuHandler
    {
        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            model.Clear(); // 清空默认菜单
            model.AddItem(CefMenuCommand.Cut, "剪切");
            model.AddItem( CefMenuCommand.Copy, "复制");
            model.AddItem(CefMenuCommand.Paste, "粘贴");
            model.AddSeparator();
            model.AddItem(CefMenuCommand.Reload, "刷新");
            model.AddSeparator();
            model.AddItem((CefMenuCommand)26501, "打开开发者工具");
            //model.AddItem((CefMenuCommand)26502, "关闭开发者工具");

        }

        public bool OnContextMenuCommand(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, CefMenuCommand commandId, CefEventFlags eventFlags)
        {

            if (commandId == (CefMenuCommand)26501)
            {
                browser.GetHost().ShowDevTools();
                return true;
            }
            if (commandId == (CefMenuCommand)26502)
            {
                browser.GetHost().CloseDevTools();
                return true;
            }
            switch (commandId)
            {
                case CefMenuCommand.Reload:
                    browser.Reload();
                    return true;                
                case CefMenuCommand.Cut:
                    browser.Cut();
                    return true;
                case CefMenuCommand.Copy:
                    browser.Copy();
                    return true;
                case CefMenuCommand.Paste:
                    browser.Paste();
                    return true;               
            }
            return false;
        }

        public void OnContextMenuDismissed(IWebBrowser browserControl, IBrowser browser, IFrame frame) { }

        public bool RunContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model, IRunContextMenuCallback callback)
        {
            return false; // 返回 false 以显示自定义菜单
        }
    }
}
