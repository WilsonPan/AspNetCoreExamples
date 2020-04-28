using System;
using System.Collections.Concurrent;
using Microsoft.Extensions.Logging;


namespace LoggingPattern.MongodbLogging
{
    public class MongodbProvider : ILoggerProvider
    {
        private readonly ConcurrentDictionary<string, MongodbLogger> _loggers = new ConcurrentDictionary<string, MongodbLogger>();
        private MongoDB.Driver.IMongoDatabase _database;

        public MongodbProvider(MongoDB.Driver.IMongoDatabase database)
        {
            _database = database;
        }
        public ILogger CreateLogger(string categoryName)
            => _loggers.GetOrAdd(categoryName, name => new MongodbLogger(categoryName, this._database));
        public void Dispose() => this._loggers.Clear();
    }
}