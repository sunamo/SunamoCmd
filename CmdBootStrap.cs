using SunamoDebugIO;
using SunamoExceptions.OnlyInSE;
using SunamoI18N;
using SunamoPlatformUwpInterop.AppData;
using SunamoShared.Loaders;
using SunamoShared.win.Powershell;

namespace SunamoCmd;

public class CmdBootStrap
{
    public static CLProgressBar clpb = new CLProgressBar();

    public static void AddToAllActions(string v, Dictionary<string, VoidVoid> actions, Dictionary<string, object> allActions)
    {
        string key = null;

        foreach (var item in actions)
        {
            key = v + AllStrings.swd + item.Key;

            if (allActions.ContainsKey(key))
            {
                break;
            }

            if (item.Key != "None")
            {
                allActions.Add(key, item.Value);
            }
        }
    }

    /// <summary>
    /// Nevrací nikdy null. Buď result z CL.AskUser (pokud se má uživatele ptát) nebo SE.
    /// </summary>
    public static
#if ASYNC
    async Task<string>
#else
    string
#endif
 Run(AIInitArgs aiInitArgs, string appName, Func<IClipboardHelper> createInstanceClipboardHelper,
#if ASYNC
    Func<Task>
#else
Action
#endif
 runInDebug, Func<Dictionary<string, TaskVoid>> AddGroupOfActions, Dictionary<string, VoidVoid> pAllActions, bool? askUserIfRelease, Action InitSqlMeasureTime, Action customInit, Action assingSearchInAll,
        Action applyCryptData, Action assignJsSerialization, String[] args, Action psInit, Dictionary<string, object> groupsOfActionsFromProgramCommon, Action javascriptSerializationInitUtf8json, string eventLogNameFromEventLogNames, Func<IDatabasesConnections> dbConns, Action<ICrypt> rijndaelBytesInit,
        ICrypt cryptDataWrapperRijn, CreateAppFoldersIfDontExistsArgs createAppFoldersIfDontExistsArgs, Dictionary<string, TaskVoid> pAllActionsAsync, bool isNotUt)
    {
        return
#if ASYNC
    await
#endif
 Run2(new RunArgs
 {
     aiInitArgs = aiInitArgs,
     appName = appName,
     createInstanceClipboardHelper = createInstanceClipboardHelper,
     runInDebug = runInDebug,
     AddGroupOfActions = AddGroupOfActions,
     pAllActions = pAllActions,
     askUserIfRelease = askUserIfRelease,
     InitSqlMeasureTime = InitSqlMeasureTime,
     customInit = customInit,
     assingSearchInAll = assingSearchInAll,
     applyCryptData = applyCryptData,
     assignJsSerialization = assignJsSerialization,
     args = args,
     psInit = psInit,
     groupsOfActionsFromProgramCommon = groupsOfActionsFromProgramCommon,
     javascriptSerializationInitUtf8json = javascriptSerializationInitUtf8json,
     eventLogNameFromEventLogNames = eventLogNameFromEventLogNames,
     dbConns = dbConns,
     rijndaelBytesInit = rijndaelBytesInit,
     cryptDataWrapperRijn = cryptDataWrapperRijn,
     createAppFoldersIfDontExistsArgs = createAppFoldersIfDontExistsArgs,
     pAllActionsAsync = pAllActionsAsync,
     isNotUt = isNotUt
 });
    }

    /// <summary>
    /// If user cannot select, A4,5 can be empty
    /// askUserIfRelease = null - ask user even in debug
    ///
    /// Nevrací nikdy null. Buď result z CL.AskUser (pokud se má uživatele ptát) nebo SE.
    /// pAllActions must be from ProgramShared
    /// </summary>
    /// <param name="appName"></param>
    /// <param name="clipboardHelperWin"></param>
    /// <param name="runInDebug"></param>
    /// <param name="AddGroupOfActions"></param>
    /// <param name="pAllActions"></param>
    public static
#if ASYNC
    async Task<string>
#else
    string
#endif
 Run2(RunArgs a)
    {
        AI.Init(a.aiInitArgs);

        var appName = a.appName;
        var createInstanceClipboardHelper = a.createInstanceClipboardHelper;
        var runInDebug = a.runInDebug;
        var AddGroupOfActions = a.AddGroupOfActions;
        var pAllActions = a.pAllActions;
        var askUserIfRelease = a.askUserIfRelease;
        var InitSqlMeasureTime = a.InitSqlMeasureTime;
        var customInit = a.customInit;
        var assingSearchInAll = a.assingSearchInAll;
        var applyCryptData = a.applyCryptData;
        var assignJsSerialization = a.assignJsSerialization;
        var args = a.args;
        var psInit = a.psInit;
        var groupsOfActionsFromProgramCommon = a.groupsOfActionsFromProgramCommon;
        var javascriptSerializationInitUtf8json = a.javascriptSerializationInitUtf8json;
        var eventLogNameFromEventLogNames = a.eventLogNameFromEventLogNames;
        var dbConns = a.dbConns;
        var rijndaelBytesInit = a.rijndaelBytesInit;
        var cryptDataWrapperRijn = a.cryptDataWrapperRijn;
        var createAppFoldersIfDontExistsArgs = a.createAppFoldersIfDontExistsArgs;
        var pAllActionsAsync = a.pAllActionsAsync;
        var isNotUt = a.isNotUt;
        var bitLockerHelperInit = a.BitLockerHelperInit;

        if (bitLockerHelperInit != null)
        {
            ThrowEx.IsLockedByBitLocker = bitLockerHelperInit();
        }

        ThisApp.EventLogName = eventLogNameFromEventLogNames;

        CL.i18n = sess.i18n;

        if (rijndaelBytesInit != null && cryptDataWrapperRijn != null)
        {
            rijndaelBytesInit(cryptDataWrapperRijn);
        }

        if (dbConns != null)
        {
            dbConns();
        }

        if (javascriptSerializationInitUtf8json != null)
        {
            javascriptSerializationInitUtf8json.Invoke();
        }

        if (assingSearchInAll != null)
        {
            assingSearchInAll();
        }

        if (applyCryptData != null)
        {
            applyCryptData();
        }

        if (assignJsSerialization != null)
        {
            assignJsSerialization();
        }

        if (psInit != null)
        {
            psInit();
        }

        ThisApp.Name = appName;

        AppData.ci.CreateAppFoldersIfDontExists(a.createAppFoldersIfDontExistsArgs);

        if (InitSqlMeasureTime != null)
        {
            InitSqlMeasureTime();
        }

        CmdApp.Init();
        CmdApp.EnableConsoleLogging(true);

        if (createInstanceClipboardHelper != null)
        {
            var instance = createInstanceClipboardHelper();
            InitApp.Clipboard = instance;
            CL.ClipboardHelper = instance;
        }

        InitApp.Logger = ConsoleLoggerCmd.Instance;
        InitApp.TemplateLogger = ConsoleTemplateLogger.Instance;
        InitApp.TypedLogger = TypedConsoleLogger.Instance;

        BasePathsHelper.Init();

        XlfResourcesHSunamo.SaveResouresToRLSunamo(LocalizationLanguagesLoader.Load());

        bool askUser = false;

        string arg = string.Empty;

        if (!askUserIfRelease.HasValue)
        {
            askUser = true;
        }

        if (customInit != null)
        {
            customInit();
        }

        #region Copied from Initialize.cs
        ProgramShared.CreatePathToFiles(AppData.ci.GetFileString);

        #region #2 Specific initialization which is not in CmdBootStrap
        //NetHelperSunamo.NEVER_EAT_POISON_Disable_CertificateValidation();

        ////SunamoCzAdminHelper.InitializeStaticTables();

        //// must be false, otherwise will raise errors that is not allowed i18n in asp.net
        //Exc.aspnet = false;
        //ThrowExceptions.writeServerError = WriterEventLog.WriteException;

        //CryptHelper.ApplyCryptData(CryptHelper.RijndaelBytes.Instance, CryptDataWrapper.rijn);
        //_.DatabasesConnections.Reload();
        //_.DatabasesConnections.SetConnToMSDatabaseLayer(Databases.SunamoCzLocal, null);
        #endregion

        #region #3 Init SunamoCzAdmin
        clpb.isNotUt = isNotUt;
        clpb.Init();


        PowershellRunner.ci.clpb = clpb;
        // To tu je zakomentované jen aby se překopírovalo tam kde to potřebuji. 
        // Jinými slovy, je to seznam kde všude je clpb

        // Pokud nějaká třída přestane existovat, číslo už ji zůstane
        //XmlDocumentsCache.clpb = clpb; // 1
        //SunamoCzAdminHelper.clpb = clpb; // 2
        //KaraokeTextyHelper.clpb = clpb; // 3
        //LyricsHelper.clpb = clpb; // 4
        //HttpRequestHelper.clpb = clpb; // 5
        //MigrateDataHelper.clpb = clpb; // 6
        //Program.clpb = clpb; // 7
        //Impl.clpb = clpb; // 8
        #endregion
        #endregion

#if !DEBUG
        askUser = askUserIfRelease.Value;
#elif DEBUG

#if ASYNC
        await
#endif
        runInDebug();
        //askUser = true;
#endif
        if (AddGroupOfActions != null && pAllActions != null)
        {
            if (args.Length != 0)
            {
                CLCmd.WriteLine($"Was entered some args, askUser was setted from {askUser} to false");
                askUser = false;
            }

            arg =
     // 
#if ASYNC
     await
#endif
 CLCmd.AskUser(askUser, AddGroupOfActions, pAllActions, pAllActionsAsync, groupsOfActionsFromProgramCommon);

            if (askUser)
            {
                CLCmd.WriteLine("App finished its running");
                // Když se mi toto pouštělo ve Win a ne ve VS tak se okno automaticky nezavírá a zbytečně to zdržovalo
                //CL.ReadLine();
            }
        }



        return arg;
    }
}
