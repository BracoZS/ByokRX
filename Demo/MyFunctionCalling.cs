using System.ComponentModel;
using System.Diagnostics;

public static class MyFunctionCalling
{
    [Description("Returns the current time")]
    public static string GetCurrentTime()
    {
        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
    }
}
