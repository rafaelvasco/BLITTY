using System.Reflection;

namespace BLITTY.Logging;

internal class LogInfo
{
    internal Assembly? OwningAssembly { get; set; }
    internal Log? Log { get; set; }
}
