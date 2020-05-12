using Contracts;
using NLog;
using System;
using System.Collections.Generic;
using System.Text;

namespace LoggerService
{
    public sealed class LoggerManager : ILoggerManager
    {
        //https://csharpindepth.com/articles/singleton
        //A single constructor, which is private and parameterless. This prevents other 
        //classes from instantiating it (which would be a violation of the pattern). 
        //Note that it also prevents subclassing - if a singleton can be subclassed once, it 
        //can be subclassed twice, and if each of those subclasses can create an instance, the 
        //pattern is violated. The factory pattern can be used if you need a single instance 
        //of a base type, but the exact type isn't known until runtime.
        //private LoggerManager() { }

        private static ILogger logger = LogManager.GetCurrentClassLogger();

        public void LogDebug(string message)
        {
            logger.Debug(message);
        }

        public void LogError(string message)
        {
            logger.Error(message);
        }

        public void LogInfo(string message)
        {
            logger.Info(message);
        }

        public void LogWarn(string message)
        {
            logger.Warn(message);
        }
    }
}
