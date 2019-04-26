using NLog;
using NLog.Config;
using NLog.Targets;
using PostSharp.Extensibility;
using PostSharp.Patterns.Diagnostics;
using PostSharp.Patterns.Diagnostics.Backends.NLog;

[assembly: Log(AttributePriority = 1, AttributeTargetMemberAttributes = MulticastAttributes.Public | MulticastAttributes.Private)]
[assembly: Log(AttributePriority = 2, AttributeExclude = true, AttributeTargetMembers = "get_*")]

namespace MessengerCommon.Services
{
    public static class LoggingInitializer
    {
        public static void Initialize()
        {
            var config = new LoggingConfiguration();

            var fileTarget = new FileTarget("file")
            {
                FileName = "log.txt",
                KeepFileOpen = true,
                ConcurrentWrites = true,
            };

            config.AddTarget(fileTarget);
            config.LoggingRules.Add(new LoggingRule("*", NLog.LogLevel.Debug, fileTarget));

            LoggingServices.DefaultBackend = new NLogLoggingBackend(new LogFactory(config));
        }
    }
}
