using CefSharp;
using redwood.Shell;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace redwood.shell.Handle
{
    public class MenuCommand
    {
        public const CefMenuCommand Cache = (CefMenuCommand)1530;
        public const CefMenuCommand Cache_OpenPath = (CefMenuCommand)1531;
        public const CefMenuCommand Cache_Clear = (CefMenuCommand)1532;
    }

    public class MenuHandler : IContextMenuHandler
    {
        public MenuHandler(BrowserForm form)
        {
            this.Form = form;
        }
        public BrowserForm Form;

        public void OnBeforeContextMenu(IWebBrowser browserControl, IBrowser browser, IFrame frame, IContextMenuParams parameters, IMenuModel model)
        {
            model.Clear(); // 清空默认菜单
            model.AddItem(CefMenuCommand.Cut, "剪切");
            model.AddItem(CefMenuCommand.Copy, "复制");
            model.AddItem(CefMenuCommand.Paste, "粘贴");
            model.AddSeparator();
            model.AddItem(CefMenuCommand.Reload, "刷新");
            model.AddSeparator();

            var cache = model.AddSubMenu(MenuCommand.Cache, "缓存");
            cache.AddItem(MenuCommand.Cache_Clear, "清空Cookies");
            cache.AddItem(MenuCommand.Cache_OpenPath, "打开目录");           

            model.AddSeparator();
            model.AddItem((CefMenuCommand)26511, "放大");
            model.AddItem((CefMenuCommand)26512, "缩小");
            var sfbl = model.AddSubMenu((CefMenuCommand)26515, "缩放比例");
            sfbl.AddItem((CefMenuCommand)26516, "80%");
            sfbl.AddItem((CefMenuCommand)26513, "100%");
            sfbl.AddItem((CefMenuCommand)26514, "150%");
            model.AddSeparator();
            model.AddItem((CefMenuCommand)26520, "搜索");
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
                case (CefMenuCommand)26511:
                    browser.GetHost().SetZoomLevel(browser.GetHost().GetZoomLevel() + 0.2);
                    break;
                case (CefMenuCommand)26512:
                    browser.GetHost().SetZoomLevel(browser.GetHost().GetZoomLevel() - 0.2);
                    break;
                case (CefMenuCommand)26513:
                    browser.SetZoomLevel(0);
                    break;
                case (CefMenuCommand)26514:
                    browser.SetZoomLevel(0.5);
                    break;
                case (CefMenuCommand)26516:
                    browser.SetZoomLevel(-0.8);
                    break;
                case (CefMenuCommand)26520:
                    this.Form.ShowFind();
                    break;
                case MenuCommand.Cache_Clear:
                    try
                    {
                        Cef.GetGlobalCookieManager().DeleteCookies();
                        var x = Cef.GetGlobalRequestContext().CachePath;
                        MessageBox.Show("清空完成,若要完整清除所有缓存，请打开目录，删除所有文件及子目录", "提示信息");
                    }
                    catch(Exception E)
                    {
                        MessageBox.Show("删除失败，请手动删除", "提示信息");
                    }
                    break;
                case MenuCommand.Cache_OpenPath:
                    var filePath = MyCersharpHelp.CachePath;
                    Process.Start("Explorer.exe", filePath);
                    break;
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
