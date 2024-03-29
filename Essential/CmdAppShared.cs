namespace SunamoCmd.Essential;

public partial class CmdApp
{
    public static void Init()
    {
        AppDomain.CurrentDomain.UnhandledException += UnhandledExceptionTrapper;
    }

    static void UnhandledExceptionTrapper(object sender, UnhandledExceptionEventArgs e)
    {
        string dump = null;
        //dump = YamlHelper.DumpAsYaml(e);
        //dump = SunamoJsonHelper.SerializeObject(e, true);
        dump = RH.DumpAsString(new DumpAsStringArgs { o = e, d = DumpProvider.Reflection });

        ThisApp.Error(e.ExceptionObject.ToString());
        //WriterEventLog.WriteToMainAppLog(dump, System.Diagnostics.EventLogEntryType.Error, Exc.CallingMethod());
    }

    /// <summary>
    /// Dont ask in console, load from Clipboard
    /// </summary>
    public static bool loadFromClipboard = false;
    public static bool waitOnEnd = false;
    public static bool openAndWaitForChangeContentOfInputFile = true;

    public static void EnableConsoleLogging(bool v)
    {
        if (v)
        {
            // because method was called two times 
            ThisApp.StatusSetted -= ThisApp_StatusSetted;
            ThisApp.StatusSetted += ThisApp_StatusSetted;
        }
        else
        {
            ThisApp.StatusSetted -= ThisApp_StatusSetted;
        }
    }

    private static void ThisApp_StatusSetted(TypeOfMessage t, string message)
    {
        TypedConsoleLogger.Instance.WriteLine(t, message);
    }

    /// <summary>
    /// Alternatives are:
    /// InitApp.SetDebugLogger
    /// CmdApp.SetLogger
    /// WpfApp.SetLogger
    /// </summary>
    public static void SetLogger()
    {
        InitApp.Logger = ConsoleLoggerCmd.Instance;
        InitApp.TemplateLogger = ConsoleTemplateLogger.Instance;
        InitApp.TypedLogger = TypedConsoleLogger.Instance;
    }
}
