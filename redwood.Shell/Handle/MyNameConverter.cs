using CefSharp.JavascriptBinding;
using System.Reflection;

namespace redwood.shell.Handle
{
    public class MyNameConverter : IJavascriptNameConverter
    {
        public string ConvertReturnedObjectPropertyAndFieldToNameJavascript(MemberInfo memberInfo)
        {
            return ConvertToJavascript(memberInfo);
        }

        public string ConvertToJavascript(MemberInfo memberInfo)
        {
            if ("CefCardReader".Equals(memberInfo.DeclaringType.Name))
            {
                string name = memberInfo.Name;
                if (name.Length == 1)
                {
                    return name;
                }
                //if (name.All(char.IsUpper))
                //{
                //    return name;
                //}
                var firstHalf = name.Substring(0, 1);
                var remainingHalf = name.Substring(1);
                return firstHalf.ToLowerInvariant() + remainingHalf;
            }
            return memberInfo.Name;
        }
    }
}
