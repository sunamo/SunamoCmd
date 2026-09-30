using SunamoLogger.Logger.TypedLoggerBaseNS;

namespace SunamoCmd.Helpers;

public class CLProgressBar : ProgressState, IProgressBar
{
    int _writeOnlyDividableBy = 0;
    bool isWriteOnlyDividableBy = false;
    public bool isNotUt = false;
    public int writeOnlyDividableBy
    {
        get
        {
            return _writeOnlyDividableBy;
        }
        set
        {
            _writeOnlyDividableBy = value;
            isWriteOnlyDividableBy = value != 0;
        }
    }

    public void Init(bool isNotUt = false)
    {
        this.isNotUt = isNotUt;
        if (isNotUt)
        {
            Init(LyricsHelper_OverallSongs, LyricsHelper_AnotherSong, LyricsHelper_WriteProgressBarEnd);
        }

    }

    public void LyricsHelper_WriteProgressBarEnd()
    {
        if (isNotUt)
        {
            Set();
            if (isWriteOnlyDividableBy)
            {
                CLCmd.ClearCurrentConsoleLine();
                CLCmd.WriteLine(n + " Finished");
            }
            else
            {
                CLCmd.WriteProgressBarEnd();
            }
            Unset();
        }
    }

    private static void Unset()
    {
        CLCmd.inClpb = false;
        CLCmd.src = ClSources.z;
    }

    private static void Set()
    {
        CLCmd.inClpb = true;
        CLCmd.src = ClSources.a;
    }

    PercentCalculator pc = null;

    public void LyricsHelper_OverallSongs(int obj)
    {
        if (isNotUt)
        {
            Set();
            n = 0;
            if (isWriteOnlyDividableBy)
            {
                CLCmd.WriteLine("Starting...");
            }
            else
            {
                pc = new PercentCalculator(obj);
                CLCmd.WriteProgressBar(0);
            }
            Unset();
        }
    }

    /// <summary>
    /// A1 is to increment done items after really finished async operation. Can be any.
    /// </summary>
    /// <param name="asyncResult"></param>
    public void LyricsHelper_AnotherSong(object asyncResult)
    {
        if (isNotUt)
        {
            Set();
            n++;
            LyricsHelper_AnotherSong(n);
            Unset();
        }
    }

    public void LyricsHelper_AnotherSong(int i)
    {
        if (isNotUt)
        {
            Set();
            if (isWriteOnlyDividableBy)
            {
                if (i % writeOnlyDividableBy == 0)
                {
                    CLCmd.ClearCurrentConsoleLine();
                    TypedSunamoLogger.Instance.Information(i.ToString());
                }
            }
            else
            {
                pc.AddOnePercent();
                CLCmd.WriteProgressBar((int)pc.last, new WriteProgressBarArgs(true, i, pc._overallSum));
            }
            Unset();
        }
    }

    /// <summary>
    /// private coz should be called only in this class
    /// </summary>
    /// <returns></returns>
    private bool IsDividable()
    {
        if (isNotUt)
        {
            if (isWriteOnlyDividableBy)
            {
                return n % writeOnlyDividableBy == 0;

            }
        }
        {
        }
        return true;
    }

    public void Init()
    {
        Init(isNotUt);
    }

    public void LyricsHelper_AnotherSong()
    {
        LyricsHelper_AnotherSong(null);
    }
}
