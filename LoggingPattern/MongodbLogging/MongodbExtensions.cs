using LoggingPattern.MongodbLogging;

namespace Microsoft.Extensions.Logging
{
    public static class MongodbExtensions
    {
        public static ILoggerFactory AddMongodb(this ILoggerFactory factory, string connetionString = "mongodb://127.0.0.1:27017/logging")
        {
            var mongoUrl = new MongoDB.Driver.MongoUrl(connetionString);
            var client = new MongoDB.Driver.MongoClient(mongoUrl);

            factory.AddProvider(new MongodbProvider(client.GetDatabase(mongoUrl.DatabaseName)));

            return factory;
        }
    }
}