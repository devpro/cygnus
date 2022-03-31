using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cygnus.Domain.Models;
using Cygnus.Domain.Repositories;
using Microsoft.Extensions.Logging;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using MongoDB.Bson.Serialization;
using MongoDB.Driver;

namespace Cygnus.Infrastructure.MongoDbDriverClient
{
    public class MongoDbDriverClientRepository : IDataSynchronizationRepository
    {
        private readonly MongoClientFactory _mongoClientFactory;

        private readonly ILogger<MongoDbDriverClientRepository> _logger;

        private readonly DatabaseConfiguration _configuration;

        public MongoDbDriverClientRepository(MongoClientFactory mongoClientFactory, ILogger<MongoDbDriverClientRepository> logger, DatabaseConfiguration configuration)
        {
            _mongoClientFactory = mongoClientFactory;
            _logger = logger;
            _configuration = configuration;
        }

        public Task<List<Dictionary<string, string>>> ReadAsync(SourceModel source, List<FieldModel> fields, string correlationField)
        {
            throw new System.NotImplementedException();
        }

        public async Task WriteAsync(List<Dictionary<string, string>> data, DestinationModel destination)
        {
            if (!_configuration.ConnectionStrings.ContainsKey(destination.Name))
            {
                throw new Exception($"Missing MongoDB connection string with name \"{destination.Name}\"");
            }

            var mongoDbClient = _mongoClientFactory.CreateClient(_configuration.ConnectionStrings[destination.Name]);

            var collection = mongoDbClient.GetDatabase(destination.Database).GetCollection<BsonDocument>(destination.Collection);

            var existing = await FindAllAsync(collection, destination.CorrelationField);

            var createdNb = 0;
            var updatedNb = 0;

            foreach (var record in data)
            {
                // not efficient
                //var existing = FindOneAsync(collection, record, destination.CorrelationField);

                if (!existing.ContainsKey(record[destination.CorrelationField]))
                {
                    await InsertOneAsync(collection, CreateJson(record, destination));
                    createdNb++;
                }

                // TODO: update if needed! (look at a last updated date)
            }

            _logger.LogInformation("Write completed {DestinationName} [Created={CreatedNb}] [Updated={UpdatedNb}]",
                destination.Name, createdNb, updatedNb);
        }

        private string CreateJson(Dictionary<string, string> record, DestinationModel destination)
        {
            var stringBuilder = new StringBuilder("{");
            var isEmpty = true;

            // TODO: manage field of type "_id":{"$oid":"622610696337d9b65a8e4c8a"}

            foreach (var field in destination.Fields)
            {
                if (!isEmpty)
                {
                    stringBuilder.Append(",");
                }
                else
                {
                    isEmpty = false;
                }

                switch (field.FieldType)
                {
                    case FieldTypeModel.String:
                        stringBuilder.Append($"\"{field.Name}\":\"{record[field.Name]}\"");
                        break;
                    case FieldTypeModel.Number:
                        stringBuilder.Append($"\"{field.Name}\":{record[field.Name]}");
                        break;
                    case FieldTypeModel.Boolean:
                        stringBuilder.Append($"\"{field.Name}\":{record[field.Name].ToBooleanString()}");
                        break;
                }
            }

            stringBuilder.Append("}");

            return stringBuilder.ToString();
        }

        private async Task<BsonDocument> FindOneAsync(IMongoCollection<BsonDocument> collection, Dictionary<string, string> record, string fieldName)
        {
            return await collection.Find(new BsonDocument(fieldName, record[fieldName])).FirstOrDefaultAsync();
        }

        private async Task<Dictionary<BsonValue, BsonDocument>> FindAllAsync(IMongoCollection<BsonDocument> collection, string fieldName)
        {
            var data = await collection.Find(new BsonDocument()).ToListAsync();
            return data.ToDictionary(x => x[fieldName]);
        }

        private async Task InsertOneAsync(IMongoCollection<BsonDocument> collection, string json)
        {
            using var jsonReader = new JsonReader(json);
            var context = BsonDeserializationContext.CreateRoot(jsonReader);
            var document = collection.DocumentSerializer.Deserialize(context);
            await collection.InsertOneAsync(document);
        }
    }
}
