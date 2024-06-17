using Lib9c.GraphQL.Enums;
using Libplanet.Crypto;
using Mimir.Exceptions;
using Mimir.Services;
using MongoDB.Bson;
using MongoDB.Driver;

namespace Mimir.Repositories;

public class DailyRewardRepository(MongoDBCollectionService mongoDbCollectionService)
    : BaseRepository<BsonDocument>(mongoDbCollectionService)
{
    public long GetDailyReward(PlanetName planetName, Address avatarAddress)
    {
        var collection = GetCollection(planetName);
        var filter = Builders<BsonDocument>.Filter.Eq("Address", avatarAddress.ToHex());
        var document = collection.Find(filter).FirstOrDefault();
        if (document is null)
        {
            throw new DocumentNotFoundInMongoCollectionException(
                $"DailyReward document not found in '{GetCollectionName()}' collection.");
        }

        try
        {
            var obj = document["State"]["Object"];
            return obj.BsonType switch
            {
                BsonType.Int32 => obj.AsInt32,
                BsonType.Int64 => obj.AsInt64,
                _ => throw new UnexpectedTypeOfBsonValueException(
                    [BsonType.Int32, BsonType.Int64],
                    obj.BsonType),
            };
        }
        catch (KeyNotFoundException e)
        {
            throw new KeyNotFoundInBsonDocumentException("Invalid key used in DailyReward document", e);
        }
        catch (UnexpectedTypeOfBsonValueException e)
        {
            throw new UnexpectedTypeOfBsonValueException("Invalid type used in DailyReward document", e);
        }
    }

    protected override string GetCollectionName() => "daily_reward";
}
