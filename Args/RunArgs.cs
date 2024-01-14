namespace SunamoCmd.Args;

public class RunArgs
{
    public AIInitArgs aiInitArgs;
    public string appName; public Func<IClipboardHelper> createInstanceClipboardHelper; public
#if ASYNC
Func<Task>
#else
Action
#endif 
        runInDebug; public Func<Dictionary<string, TaskVoid>> AddGroupOfActions; public Dictionary<string, VoidVoid> pAllActions; public bool? askUserIfRelease; public Action InitSqlMeasureTime; public Action customInit; public Action assingSearchInAll; public Action applyCryptData; public Action assignJsSerialization; public string[] args; public Action psInit; public Dictionary<string, object> groupsOfActionsFromProgramCommon; public Action javascriptSerializationInitUtf8json; public string eventLogNameFromEventLogNames; public Func<IDatabasesConnections> dbConns; public Action<ICrypt> rijndaelBytesInit; public ICrypt cryptDataWrapperRijn;
    public CreateAppFoldersIfDontExistsArgs createAppFoldersIfDontExistsArgs;

    public Dictionary<string, TaskVoid> pAllActionsAsync;
    public bool isNotUt = true;
    public Func<Func<char, bool>> BitLockerHelperInit;
    public bool IsDebug;
}
