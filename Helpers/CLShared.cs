using SunamoI18N;
using SunamoI18N.Values;

namespace SunamoCmd.Helpers;

public partial class CLCmd : CL
{













    /// <param name = "actions"></param>
    /// <param name = "vyzva"></param>
    public static void SelectFromVariants(Dictionary<string, EmptyHandler> actions)
    {
        string appeal = sess.i18n(XlfKeys.SelectAction) + ":";
        int i = 0;
        foreach (KeyValuePair<string, EmptyHandler> kvp in actions)
        {
            CLCmd.WriteLine(AllStrings.lsqb + i + AllStrings.rsqb + "  " + kvp.Key);
            i++;
        }

        int entered = UserMustTypeNumber(appeal, actions.Count - 1);
        if (entered == -1)
        {
            OperationWasStopped();
            return;
        }

        i = 0;
        string operation = null;
        foreach (string var in actions.Keys)
        {
            if (i == entered)
            {
                operation = var;
                break;
            }

            i++;
        }
        var act = actions[operation];
        act.Invoke();
    }


    public static void OperationWasStopped()
    {
        ConsoleTemplateLogger.Instance.OperationWasStopped();
    }



    /// <summary>
    /// First I must ask which is always from console - must prepare user to load data to clipboard. 
    /// </summary>
    /// <param name="format"></param>
    /// <param name="textFormat"></param>
    public static string LoadFromClipboardOrConsoleInFormat(string format, TextFormatData textFormat)
    {
        string s = null;

        if (!CmdApp.loadFromClipboard)
        {
            s = UserMustTypeInFormat(format, textFormat);
        }
        else
        {
            s = ClipboardHelper.GetText();
        }

        return s;
    }

    /// <summary>
    /// Will ask before getting data
    /// First I must ask which is always from console - must prepare user to load data to clipboard. 
    /// </summary>
    /// <param name="what"></param>
    public static string LoadFromClipboardOrConsole(string what, string prefix = "")
    {
        string imageFile = @"";

        if (!CmdApp.loadFromClipboard)
        {
            imageFile = CLCmd.UserMustType(what, prefix);
        }
        else
        {
            CL.AskForEnterWrite(what, true);
            CLCmd.WriteLine(sess.i18n(XlfKeys.PressEnterWhenDataWillBeInClipboard));
            CLCmd.ReadLine();
            imageFile = ClipboardHelper.GetText();
        }

        if (imageFile.Trim() == "")
        {
            imageFile = LoadFromClipboardOrConsole(what, "Entered text was empty or only whitespace. ");
        }

        return imageFile;
    }


    /// <summary>
    /// Return null when user force stop 
    /// </summary>
    /// <param name = "what"></param>
    /// <param name = "textFormat"></param>
    public static string UserMustTypeInFormat(string what, TextFormatData textFormat)
    {
        return UserMustType(what);

        #region Must be repaired first. DateToShort in ConsoleApp1 failed while parsing.
        //string entered = "";
        //while (true)
        //{
        //    entered = UserMustType(what);
        //    if (entered == null)
        //    {
        //        return null;
        //    }

        //    if (SH.HasTextRightFormat(entered, textFormat))
        //    {
        //        return entered;
        //    }
        //    else
        //    {
        //        ConsoleTemplateLogger.Instance.UnfortunatelyBadFormatPleaseTryAgain();
        //    }
        //}

        //return null; 
        #endregion
    }

    public static Browsers SelectFromBrowsers(Action addBrowser)
    {
        throw new NotImplementedException();
    }

    public static string AskForFolder(string folderDbg)
    {
        string folder = null;

#if DEBUG
        folder = folderDbg;
#else
        folder = LoadFromClipboardOrConsole("folder");
#endif

        return folder;
    }

    public static List<string> AskForFolderMascRecFiles(string folderDbg, string mascDbg, bool recDbg)
    {
        var (folder, masc, rec) = CLCmd.AskForFolderMascRec(folderDbg, mascDbg, recDbg);
        return Directory.GetFiles(folder, masc, rec.Value ? SearchOption.AllDirectories : SearchOption.TopDirectoryOnly).ToList();
    }

    public static (string folder, string masc, bool? rec) AskForFolderMascRec(string folderDbg, string mascDbg, bool? recDbg)
    {
        string folder = null;
        string masc = null;
        bool? rec = false;

#if DEBUG
        folder = folderDbg;
        masc = mascDbg;
        rec = recDbg;
#else
        folder = LoadFromClipboardOrConsole("folder");
        masc = CL.UserMustType("masc");
        recDbg = CL.UserMustTypeYesNo("recursive");
#endif

        return (folder, masc, rec);
    }
}
