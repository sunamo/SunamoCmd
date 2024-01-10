namespace SunamoCmd;

public class CmdBootStrapArgs
{
    #region Cant be null
    public string appName;
    public IClipboardHelper clipboardHelperWin;
    public Action runInDebug;
    public Func<Dictionary<string, VoidVoid>> AddGroupOfActions;
    // je zároveň definovaný i v SunamoCmdArgs.Cmd. Zde NSN => commented 
    //public Dictionary<string, VoidVoid> allActions;
    public bool askUserIfRelease;
    #endregion

    public Action InitSqlMeasureTime;
}
