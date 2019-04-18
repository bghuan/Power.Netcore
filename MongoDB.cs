using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using MongoDB.Bson;

namespace Power
{
    class MongoDB : StaticPower
    {
        private static MongoClient client = new MongoClient(Config.strCon);
        private static IMongoDatabase database = client.GetDatabase(Config.strDB);
        private static string default_collection = "In";
        private static string default_key = "str";

        public void init()
        {
            var collection = database.GetCollection<BsonDocument>(default_collection);
            var Ret = collection.Find(new BsonDocument());
        }
        public List<BsonDocument> Read(BsonDocument Bson)
        {
            var collection = database.GetCollection<BsonDocument>(default_collection);
            var bson = Bson.Contains("str") ? new BsonDocument("str", Bson["str"]) : Bson;
            var Ret = collection.Find(bson).ToList();
            Create(Bson);
            return Ret;
        }
        public void Create(BsonDocument Bson)
        {
            var collection = database.GetCollection<BsonDocument>(default_collection);
            if (!Bson.Contains("_id")) { Bson.Set("_id", ObjectId.GenerateNewId()); }
            collection.InsertOne(Bson);
        }
        public int Group(BsonDocument Bson)
        {
            var collection = database.GetCollection<BsonDocument>(default_collection);
            BsonDocument dbMatch = Bson.Contains("str") ? new BsonDocument("str", Bson["str"]) : Bson;
            BsonDocument db = new BsonDocument { { "_id", "" }, { "count", new BsonDocument("$sum", 1) } };
            //var aggregate = collection.Aggregate().Match(dbMatch).Group(db);
            var list = collection.Aggregate().Match(dbMatch).Group(db).ToList<BsonDocument>();
            if (list.Count == 0) { return 0; }
            else { return (int)list[0]["count"]; }
        }

        public int Update(BsonDocument bson_filter, BsonDocument bson_update)
        {
            var collection = database.GetCollection<BsonDocument>(default_collection);
            var filter = Builders<BsonDocument>.Filter.Eq(default_key, "");
            var update = Builders<BsonDocument>.Update.Set("power", 1);
            var result = collection.UpdateOne(filter, update);

            if (result.IsModifiedCountAvailable)
            {
                return (int)result.ModifiedCount;
            }
            else
            {
                return 0;
            }
        }
        public List<BsonDocument> Read(string str)
        {
            var collection = database.GetCollection<BsonDocument>(default_collection);
            var filter = Builders<BsonDocument>.Filter.Eq(default_key, str);
            return collection.Find(filter).ToList();
        }
        public void Create(List<BsonDocument> Bson)
        {
            var collection = database.GetCollection<BsonDocument>(default_collection);
            collection.InsertMany(Bson);
        }
        public async Task ReadAsync(BsonDocument bson)
        {
            var collection = database.GetCollection<BsonDocument>(default_collection);
            await collection.Find(bson).ForEachAsync(d => { });
            await CreateAsync(bson);
        }
        public async Task CreateAsync(BsonDocument Bson)
        {
            var collection = database.GetCollection<BsonDocument>(default_collection);
            if (!Bson.Contains("_id")) { Bson.Set("_id", ObjectId.GenerateNewId()); }
            await collection.InsertOneAsync(Bson);
            try { BSON["SYSTEM"].AsBsonDocument.Set("ASYNCCOUNT", (int)BSON["SYSTEM"]["ASYNCCOUNT"] - 1); }
            catch { }
        }
        public BsonDocument Attention()
        {
            var fliter = Builders<BsonDocument>.Filter.Exists("attention");
            var collection = database.GetCollection<BsonDocument>(default_collection);
            return collection.Find(fliter).Sort(new BsonDocument("_id", -1)).FirstOrDefault();
        }
        //public async Task Read(string str)
        //{
        //    var start = DateTime.Now;
        //    BsonDocument bson = Group(str)[0];
        //    Console.WriteLine(bson + " " + (int)(DateTime.Now - start).TotalMilliseconds + "ms");
        //    int count = (int)(bson["count"] == null ? 0 : bson["count"]);
        //    var collection = database.GetCollection<BsonDocument>(default_collecte);
        //    var filter = Builders<BsonDocument>.Filter.Eq(default_key, str);
        //    Power power = new Power();
        //    await collection.Find(filter).ForEachAsync(d => { power.SetPower(d.Contains("power") ? int.Parse(d["power"].ToString()) : 0); power.Out(d); });
        //    await Update(str);
        //}
        //public async Task Create(List<BsonDocument> Bson)
        //{
        //    var collection = database.GetCollection<BsonDocument>(default_collecte);
        //    await collection.InsertManyAsync(Bson);
        //}
        //public async Task Update(string str)
        //{
        //    var collection = database.GetCollection<BsonDocument>(default_collecte);
        //    var filter = Builders<BsonDocument>.Filter.Eq(default_key, str);
        //    var update = Builders<BsonDocument>.Update.Set("power", 1);
        //    var result = await collection.UpdateOneAsync(filter, update);
        //}
        //public List<BsonDocument> Group(string str)
        //{
        //    var collection = database.GetCollection<BsonDocument>(default_collecte);
        //    BsonDocument dbMatch = new BsonDocument { { default_key, str } };
        //    BsonDocument db = new BsonDocument { { "_id", str }, { "count", new BsonDocument("$sum", 1) } };
        //    var aggregate = collection.Aggregate().Match(dbMatch).Group(db);
        //    return aggregate.ToList<BsonDocument>();
        //}
        //public async Task Top(int limit = 10)
        //{
        //    var collection = database.GetCollection<BsonDocument>(default_collecte);
        //    await collection.Find(new BsonDocument()).Sort(new BsonDocument("_id", -1)).Limit(limit).ForEachAsync(d => { if (d["str"] == "wake") { ConsoleHelper.showConsole(); } });
        //}
        public BsonDocument GetFilter()
        {
            var filter = Builders<BsonDocument>.Filter.Eq("str", "");
            filter = Builders<BsonDocument>.Filter.Exists("str");
            filter = Builders<BsonDocument>.Filter.Gt("str", 0);
            filter = Builders<BsonDocument>.Filter.Lt("str", "");
            filter = Builders<BsonDocument>.Filter.Gte("str", "");
            filter = Builders<BsonDocument>.Filter.Lte("str", "");
            filter = Builders<BsonDocument>.Filter.In("str", "");
            filter = Builders<BsonDocument>.Filter.Mod("str", 1, 2);
            filter = Builders<BsonDocument>.Filter.Ne("str", "");
            //filter = Builders<BsonDocument>.Filter.Nin(new BsonArray(),2);
            filter = Builders<BsonDocument>.Filter.Not("str");
            filter = Builders<BsonDocument>.Filter.Size("str", 1);
            filter = Builders<BsonDocument>.Filter.SizeGt("str", 1);
            filter = Builders<BsonDocument>.Filter.SizeGte("str", 1);
            filter = Builders<BsonDocument>.Filter.SizeLt("str", 1);
            filter = Builders<BsonDocument>.Filter.SizeLte("str", 1);
            filter = Builders<BsonDocument>.Filter.Type("str", "");
            filter = Builders<BsonDocument>.Filter.Regex("str", "");
            filter = Builders<BsonDocument>.Filter.All("str", "");
            //filter = Builders<BsonDocument>.Filter.Any("str", "");
            //filter = Builders<BsonDocument>.Filter.Where(BsonJavaScript);
            //filter = Builders<BsonDocument>.Filter.Matches("str", "");
            filter = Builders<BsonDocument>.Filter.And("str", "");
            filter = Builders<BsonDocument>.Filter.Or("str", "");
            return new BsonDocument();
        }
        //public static BsonDocument Parse(string key, string value)
        //{
        //    return new BsonDocument(key, value.Replace("\\", "\\\\").Replace("\'", "\\'"));
        //}
        public List<BsonDocument> push()
        {
            BsonDocument db = new BsonDocument { { "_id", "sex" }, { "AllAge", new BsonDocument("push", "$age") } };
            var collection = database.GetCollection<BsonDocument>(default_collection);
            var aggregate = collection.Aggregate().Group(db);
            List<BsonDocument> list = aggregate.ToList<BsonDocument>();
            return list;
        }
    }
}
