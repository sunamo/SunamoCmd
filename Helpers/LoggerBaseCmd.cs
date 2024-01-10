using SunamoStringJoin;

namespace SunamoCmd.Helpers;

/// <summary>
/// Zbylé metody jež nemůžou být v cl, protože mají deps
///
/// </summary>
public class LoggerBaseCmd : LoggerBase
{
    public void DumpObject(string name, object o, DumpProvider d, params string[] onlyNames)
    {
        var dump = RH.DumpAsString(new DumpAsStringArgs { name = name, o = o, d = d, onlyNames = onlyNames.ToList() });//  , o, d, onlyNames);
        WriteLine(dump);
        WriteLine(AllStrings.space);
    }

    public void DumpObjects(string name, IList o, DumpProvider d, params string[] onlyNames)
    {
        int i = 0;
        foreach (var item in o)
        {
            DumpObject(name + " #" + i, item, d, onlyNames);
            i++;
        }
    }

    public void WriteArgs(params string[] args)
    {
        _writeLineDelegate.Invoke(SHJoin.JoinPairs(args), EmptyArrays.Strings);
    }
}
