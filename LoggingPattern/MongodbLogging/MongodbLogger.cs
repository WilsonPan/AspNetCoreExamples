using System;
using Microsoft.Extensions.Logging;
using MongoDB.Driver;

namespace LoggingPattern.MongodbLogging
{
public class MongodbLogger : ILogger
{
    private readonly string _name;
    private MongoDB.Driver.IMongoDatabase _database;

    public MongodbLogger(string name, MongoDB.Driver.IMongoDatabase database)
    {
        _name = name;
        _database = database;
    }
    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception exception, Func<TState, Exception, string> formatter)
    {
        var collection = _database.GetCollection<dynamic>(logLevel.ToString().ToLower());

        string message = formatter(state, exception);

        collection.InsertOneAsync(new
        {
            time = DateTime.Now,
            name = _name,
            message,
            exception
        });
    }
    public bool IsEnabled(LogLevel logLevel) => logLevel != LogLevel.None;

    public System.IDisposable BeginScope<TState>(TState state) => NullScope.Instance;
}
}