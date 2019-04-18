using MongoDB.Bson;

namespace Power
{
    class StaticPower
    {
        protected static BsonDocument BSON = new BsonDocument();
        protected static BsonArray bsonArray = new BsonArray();
    }
}
