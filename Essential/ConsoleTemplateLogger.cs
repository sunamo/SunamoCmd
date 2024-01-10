namespace SunamoCmd.Essential;

public class ConsoleTemplateLogger : TemplateLoggerBase
{
    public static ConsoleTemplateLogger Instance = new ConsoleTemplateLogger();

    private ConsoleTemplateLogger() : base(ConsoleLogger.WriteMessage)
    {

    }
}
