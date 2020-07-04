using System;
using McMaster.Extensions.CommandLineUtils;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotnetChecker.Commands.Mongo
{
    public class MongoCommand
    {
        [Option("-c|--Connection", "Mongodb connection string, example: mongodb://localhost:27017/db", CommandOptionType.SingleValue)]
        public string Connection { get; set; }

        [Option("-d|--Database", "Mongodb database", CommandOptionType.SingleValue)]
        public string Database { get; set; }

        private readonly IConsole _console;

        public MongoCommand(IConsole console)
        {
            _console = console;
        }

        public void OnExecute()
        {
            if (string.IsNullOrWhiteSpace(Connection))
            {
                _console.WriteLine("The connection can not be null.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Database))
            {
                _console.WriteLine("The database can not be null.");
                return;
            }

            var client = new MongoClient(Connection);
            var db = client.GetDatabase(Database);

            var id = Guid.NewGuid().ToString();

            var collectionName = $"dotnet-checker-{id}";

            var collection = db.GetCollection<BsonDocument>(collectionName);

            var document = new BsonDocument
            {
                { "id", id },
                { "name", "dotnet-checker" },
                { "timestamp", DateTimeOffset.UtcNow.ToUnixTimeSeconds() }
            };

            collection.InsertOne(document);

            _console.WriteLine("InsertOne is normal.");

            var entity = collection.Find(new BsonDocument {{"id", id}}).FirstOrDefault();

            if (entity != null)
            {
                _console.WriteLine($"Find is normal. The value is {entity}");
            }

            collection.DeleteOne(entity);

            _console.WriteLine("DeleteOne is normal.");

            db.DropCollection(collectionName);
            _console.WriteLine("DropCollection is normal.");
        }
    }
}