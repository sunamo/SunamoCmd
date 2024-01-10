namespace SunamoCmd.Args;

public class WriteProgressBarArgs
{
    public static WriteProgressBarArgs Default = new WriteProgressBarArgs();

    public WriteProgressBarArgs()
    {

    }

    public WriteProgressBarArgs(bool update) : this()
    {
        this.update = update;
    }

    public WriteProgressBarArgs(bool update, double actual, double overall) : this(update)
    {
        this.actual = actual;
        this.overall = overall;
        writePieces = true;
    }

    public bool update = false;
    public bool writePieces = false;
    public double actual = 0;
    public double overall = 0;
}
