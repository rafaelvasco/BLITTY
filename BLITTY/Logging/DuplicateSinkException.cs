namespace BLITTY.Logging;

public class DuplicateSinkException : BlittyException
{
    public Type SinkType { get; }

    internal DuplicateSinkException(Type sinkType)
        : base("Sink of this type already exists.")
    {
        SinkType = sinkType;
    }
}
