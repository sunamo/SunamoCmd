namespace SunamoCmd.Essential;

public partial class CmdApp
{


    public static TypedLoggerBase ConsoleOrDebugTyped()
    {
        //        // toto mi fungovalo při projectreference. ale při packagereference v release není žádný TypedDebugLogger
        //#if DEBUG
        //        return TypedDebugLogger.Instance;
        //#elif !DEBUG
        //        return TypedConsoleLogger.Instance;
        //#endif

        return TypedConsoleLogger.Instance;
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
 WaitForSaving(string myPositionsHtmlFile, Func<string, Task> openVsCode)
    {
        if (openAndWaitForChangeContentOfInputFile)
        {
            await openVsCode(myPositionsHtmlFile);
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
