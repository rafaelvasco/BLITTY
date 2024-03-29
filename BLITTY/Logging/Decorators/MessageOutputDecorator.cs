﻿namespace BLITTY.Logging;

public class MessageOutputDecorator : Decorator
{
    public override string Decorate(LogLevel logLevel, string input, string originalMessage, Sink sink)
    {
        var output = originalMessage;

        if (sink is ConsoleSink)
            output = originalMessage.AnsiColorEncodeRGB(255, 255, 255);

        return output;
    }
}
