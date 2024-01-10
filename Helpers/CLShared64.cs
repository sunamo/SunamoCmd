namespace SunamoCmd.Helpers;

/// <summary>
/// Později převést i toto do cl ať nemusím nikde psát postfix Cmd
/// </summary>
public partial class CLCmd
{
    /// <summary>
    /// Return None if !A1
    /// If allActions will be null, will not automatically run action
    /// </summary>
    /// <param name="askUser"></param>
    /// <param name="AddGroupOfActions"></param>
    /// <param name="allActions"></param>
    public static
#if ASYNC
    // nevím proč jsem to zakomentoval, příště si to tu zapsat
    async Task<string>
    //string
#else
    string
#endif
 AskUser(bool askUser, Func<Dictionary<string, TaskVoid>> AddGroupOfActions, Dictionary<string, VoidVoid> allActions, Dictionary<string, TaskVoid> allActionsAsync, Dictionary<string, object> groupsOfActionsFromProgramCommon)
    {
        string mode = null;
        // must be called in all cases!!
        var d = AddGroupOfActions();

        /*
groupsOfActionsFromProgramCommon bude po novu null
        proto tento kód zakomentuji

        ale to nejde, protože ho potřebuji niže 
        přes AsyncHelperSE.InvokeTaskVoidOrVoidVoid potřebuji naplnit allActions a allActionsAsync
        */

        foreach (var item in d)
        {
            groupsOfActionsFromProgramCommon.Add(item.Key, item.Value);
        }

        if (askUser)
        {
            bool? loadFromClipboard = false;
            if (ThisApp.Name != "AllProjectsSearch")
            {
                loadFromClipboard = CLCmd.UserMustTypeYesNo(i18n(XlfKeys.DoYouWantLoadDataOnlyFromClipboard) + " " + i18n(XlfKeys.MultiLinesTextCanBeLoadedOnlyFromClipboardBecauseConsoleAppRecognizeEndingWhitespacesLikeEnter));
            }

            CmdApp.loadFromClipboard = loadFromClipboard.Value;

            if (loadFromClipboard.HasValue)
            {
                var whatUserNeed = "format";
                // na začátku zadám fulltextový řetězec co chci nebo -1 abych měl možnost vybrat ze všech možností
                whatUserNeed = CLCmd.UserMustType("you need or enter -1 for select from all groups");

                if (whatUserNeed == "-1")
                {
                    CL.WriteLine("Nechám uživatele vybrat ze všech možností (zadal -1), perform je: " + perform);
                    CL.WriteLine("Zatím jsem to zakomentoval, mám teď jiné věci na řešení");

                    //CLCmd.PerformActionAsync(groupsOfActionsFromProgramCommon);
                }
                else
                {
                    //
                    perform = false;
                    //AddGroupOfActions();

                    foreach (var item in groupsOfActionsFromProgramCommon)
                    {

#if ASYNC
                        await
#endif
                        AsyncHelperSE.InvokeTaskVoidOrVoidVoid(item.Value);
                    }

                    Dictionary<string, VoidVoid> potentiallyValid = new Dictionary<string, VoidVoid>();
                    Dictionary<string, TaskVoid> potentiallyValidAsync = new Dictionary<string, TaskVoid>();
                    foreach (var item in allActions)
                    {
                        if (SH.Contains(item.Key, whatUserNeed, SearchStrategy.AnySpaces, false))
                        {
                            potentiallyValid.Add(item.Key, item.Value);
                        }
                    }

                    foreach (var item in allActionsAsync)
                    {
                        if (SH.Contains(item.Key, whatUserNeed, SearchStrategy.AnySpaces, false))
                        {
                            potentiallyValidAsync.Add(item.Key, item.Value);
                        }
                    }

                    if (potentiallyValid.Count == 0 && potentiallyValidAsync.Count == 0)
                    {
                        Information(i18n(XlfKeys.NoActionWasFound));
                    }
                    else
                    {
                        //if (potentiallyValid.Any())
                        //{
                        //    mode = CLCmd.PerformAction(potentiallyValid);
                        //}
                        //else
                        //{
                        //mode = CLCmd.PerformActionAsync(potentiallyValidAsync);
                        //}


                        // je zajímave že při tomhle se vypíše to co je v potentiallyValid
                        // není, on to prostě vypíše a čeká
                        // musím to tu zkombinovat!

                        var actionsMerge = AsyncHelperSE.MergeDictionaries(potentiallyValid, potentiallyValidAsync);

                        mode =
#if ASYNC
    await
#endif
 CLCmd.PerformActionAsync(actionsMerge);
                    }
                }
            }
            return mode;
        }
        else
        {
            /*
Zde vůbec nevím co se děje
            To je tím že jsem si nepsal žádné komentáře
            ale na 99%, nechá mě to vybrat skupinu (Dating, Other, atd.)
            ze které později vyberu akci
            */

            var before = CLCmd.perform;
            CLCmd.perform = false;
            foreach (var item in d)
            {

#if ASYNC
                await
#endif

                AsyncHelperSE.InvokeTaskVoidOrVoidVoid(item.Value);
            }
            CLCmd.perform = before;

            return mode;
        }
    }

    /// <summary>
    /// for compatibility with CL.WriteLine 
    /// </summary>
    /// <param name = "what"></param>
    public static void WriteLine(object what)
    {
        if (what != null)
        {
            // musí tu být CL, protože this.WriteLine bere object takže mi zavolá sebe sama 
            // jinak ale CLCmd dědí od CL kde je WriteLine
            CL.WriteLine(SH.ListToString(what));
        }
    }

    #region Progress bar
    const char _block = '■';
    const string _back = "\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b\b";
    static int _backL = 0;
    const string _twirl = "-\\|/";





    /// <summary>
    /// 1
    /// </summary>
    public static void WriteProgressBarInit()
    {
        _backL = _back.Length;
    }

    /// <summary>
    /// 2
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="a"></param>
    public static void WriteProgressBar(double percent, WriteProgressBarArgs a = null)
    {
        WriteProgressBar((int)percent, a);
    }

    /// <summary>
    /// 3
    /// Usage:
    /// WriteProgressBar(0);
    /// WriteProgressBar(i, true);
    /// </summary>
    /// <param name="percent"></param>
    /// <param name="update"></param>
    public static void WriteProgressBar(int percent, WriteProgressBarArgs a = null)
    {
        if (a == null)
        {
            a = WriteProgressBarArgs.Default;
        }

        if (a.update)
        {
            sbToClear.Clear();

            //sbToClear.Append( string.Empty.PadRight(s.Length, '\b'));

            sbToClear.Append(_back);
            sbToClear.Append(string.Empty.PadRight(s.Length - _backL, '\b'));

            var ts = sbToClear.ToString();

            CLCmd.Write(ts);
        }

        CLCmd.Write("[");
        var p = (int)((percent / 10f) + .5f);
        for (var i = 0; i < 10; ++i)
        {
            if (i >= p)
                CLCmd.Write(' ');
            else
                CLCmd.Write(_block);
        }

        if (a.writePieces)
        {
            s = "] {0,3:##0}%" + $" {a.actual} / {a.overall}";
        }
        else
        {
            s = "] {0,3:##0}%";
        }

        string fr = string.Format(s, percent);

        CLCmd.Write(fr);
    }

    private static void Write(char v)
    {
        CL.Write(v);
    }

    /// <summary>
    /// 4
    /// </summary>
    public static void WriteProgressBarEnd()
    {
        WriteProgressBar(100, new WriteProgressBarArgs(true));
        CLCmd.WriteLine();
    }


    #endregion
}
