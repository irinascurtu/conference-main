using System;
using System.Collections.Generic;
using System.Text;
using Conference.Logging.Data;
using Microsoft.Extensions.Logging;

namespace Conference.Logging
{
    public class DbLoggingProvider : ILoggerProvider
    {
        private readonly LoggingDbContext context;

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public DbLoggingProvider(LoggingDbContext context)
        {
            this.context = context;
        }

        public ILogger CreateLogger(string categoryName)
        {
            return new DbLogger(context);
        }
    }
}
