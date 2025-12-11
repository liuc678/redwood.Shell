using CefSharp;

namespace redwood.Shell
{
    public class KeyBoardHander : IKeyboardHandler
    {

        public bool OnKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey)
        {
            return false;
        }

        public bool OnPreKeyEvent(IWebBrowser browserControl, IBrowser browser, KeyType type, int windowsKeyCode, int nativeKeyCode, CefEventFlags modifiers, bool isSystemKey, ref bool isKeyboardShortcut)
        {
            const int VK_F5 = 0x74;
            if (modifiers == CefEventFlags.ControlDown)
            {
                switch (windowsKeyCode)
                {
                    case 187://(int)'+':
                        browser.GetHost().SetZoomLevel(browser.GetHost().GetZoomLevel() + 0.2);
                        return true;
                    case 189://  (int)'-':
                        browser.GetHost().SetZoomLevel(browser.GetHost().GetZoomLevel() - 0.2);
                        return true;
                    case (int)'0':
                        browser.SetZoomLevel(0);
                        return true;

                }
                
            }
            if (windowsKeyCode == VK_F5)
            {
                browser.Reload(); //此处可以添加想要实现的代码段
            }
            return false;
        }
    }
}
