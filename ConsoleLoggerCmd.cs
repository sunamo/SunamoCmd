namespace cmd;

/// <summary>
/// Musí tu být se svými 3 řádky pro CmdApp.SetLogger
/// Udělat InitApp.Logger jako ILoggerBase nejde, protože ConsoleLogger ty metody nemá, ty má jen ConsoleLoggerCmd protože ten je odvozený od LoggerBase
/// 
/// </summary>
public class ConsoleLoggerCmd : LoggerBase, ILoggerBase
{
    public static ConsoleLoggerCmd Instance = new ConsoleLoggerCmd(CLCmd.WriteLine);

    public ConsoleLoggerCmd(VoidStringParamsObjects writeLineHandler) : base(writeLineHandler)
    {

    }
}
