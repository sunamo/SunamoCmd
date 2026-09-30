namespace SunamoCmd.Essential;

public class TypedConsoleLogger : TypedLoggerBase
{
    public static TypedConsoleLogger Instance = new TypedConsoleLogger();

    private TypedConsoleLogger() : base(CL.ChangeColorOfConsoleAndWrite)
    {

    }


}
