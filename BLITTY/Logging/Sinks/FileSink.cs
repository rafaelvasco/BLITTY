namespace BLITTY.Logging;

public class FileSink : StreamSink
{
    public FileSink(string filePath)
        : base(new FileStream(
            filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.Read
        ))
    {
    }
}