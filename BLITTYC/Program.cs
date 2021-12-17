using PowerArgs;

namespace BLITTYC;

internal class Program
{
    static void Main(string[] args)
    {
        Args.InvokeAction<ArgsExecutor>(args);
    }
}
