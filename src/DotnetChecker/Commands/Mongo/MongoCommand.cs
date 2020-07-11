using System;
using McMaster.Extensions.CommandLineUtils;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DotnetChecker.Commands.Mongo
{
    [Command("mongo", Description = "Check mongodb")]
    public class MongoCommand
    {
        [Option("-c|--connection", "Mongodb connection string, example: mongodb://localhost:27017/db", CommandOptionType.SingleValue)]
        public string Connection { get; set; }

        [Option("-d|--database", "Mongodb database", CommandOptionType.SingleValue)]
        public string Database { get; set; }

        public void OnExecute(IConsole console)
        {
            if (string.IsNullOrWhiteSpace(Connection))
            {
                console.WriteLine("The connection can not be null.");
                return;
            }

            if (string.IsNullOrWhiteSpace(Database))
            {
                console.WriteLine("The database can not be null.");
                return;
            }

            try
            {
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

                console.WriteLine("InsertOne is normal.");

                var entity = collection.Find(new BsonDocument { { "id", id } }).FirstOrDefault();

                if (entity != null)
                {
                    console.WriteLine($"Find is normal. The value is {entity}");
                }

                collection.DeleteOne(entity);

                console.WriteLine("DeleteOne is normal.");

                db.DropCollection(collectionName);
                console.WriteLine("DropCollection is normal.");
            }
            catch (Exception ex)
            {
                console.WriteLine(ex.Message);
            }
        }
    }
}