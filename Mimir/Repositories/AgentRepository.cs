using Libplanet.Crypto;
using Mimir.Enums;
using Mimir.Exceptions;
using Mimir.Models;
using Mimir.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mimir.Repositories;

public class AgentRepository(MongoDbService dbService)
{
    public Agent GetAgent(Address agentAddress)
    {
        var collection = dbService.GetCollection<BsonDocument>(CollectionNames.Agent.Value);
        var filter = Builders<BsonDocument>.Filter.Eq("Address", agentAddress.ToHex());
        var document = collection.Find(filter).FirstOrDefault();
        if (document is null)
        {
            throw new DocumentNotFoundInMongoCollectionException(
                collection.CollectionNamespace.CollectionName,
                $"'Address' equals to '{agentAddress.ToHex()}'");
        }

        try
        {
            var doc = document["Object"].AsBsonDocument;
            return new Agent(doc);
        }
        catch (KeyNotFoundException e)
        {
            throw new KeyNotFoundInBsonDocumentException(
                "document[\"State\"][\"Object\"].AsBsonDocument",
                e);
        }
    }
}
