using System;
using System.Collections.Generic;
using System.Text;
using Conference.Logging.Data;
using Microsoft.Extensions.Logging;

namespace Conference.Logging
{
    public class DbLogger : ILogger
    {
        private readonly LoggingDbContext context;

        public DbLogger(LoggingDbContext context)
        {
            this.context = context;
        }

        public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception,
            Func<TState, Exception, string> formatter)
        {
            if (logLevel == LogLevel.Error)
            {
                var log = new Log()
                {
                    ApplicationId = 2, //custom appId
                    Message = exception.Message,
                    SeverityId = (int)logLevel,
                    StackTrace = exception.StackTrace
                };

                context.Logs.Add(log);
                context.SaveChanges();
            }
        }

        public bool IsEnabled(LogLevel logLevel)
        {
            return true;
        }

        public IDisposable BeginScope<TState>(TState state)
        {
            return null;
        }
    }

      
}
