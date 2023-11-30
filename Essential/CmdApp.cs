public partial class CmdApp
{


    public static TypedLoggerBase ConsoleOrDebugTyped()
    {
#if DEBUG
        return TypedDebugLogger.Instance;
#elif !DEBUG
        return TypedConsoleLogger.Instance;
#endif
    }

    /// <summary>
    /// Create in class where are you calling method without A2 openVsCode
    /// </summary>
    /// <param name="myPositionsHtmlFile"></param>
    /// <param name="openVsCode"></param>
    public static
#if ASYNC
    async Task<string>
#else
    string  
#endif
 WaitForSaving(string myPositionsHtmlFile, Action<string> openVsCode)
    {
        if (openAndWaitForChangeContentOfInputFile)
        {
            openVsCode(myPositionsHtmlFile);
            CLCmd.WriteLine($"Waiting for insert html to {FS.GetFileName(myPositionsHtmlFile)}, press enter to continue");
            CLCmd.ReadLine();
        }
        return
#if ASYNC
    await
#endif
 TF.ReadAllText(myPositionsHtmlFile);
    }

    public static void WaitOnEnd()
    {
#if DEBUG
        if (waitOnEnd)
        {
            CLCmd.ReadLine();
        }
#endif
    }
}
