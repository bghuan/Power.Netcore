using MongoDB.Bson;
using System;
using System.Collections.Generic;

namespace Power
{
    class Power : StaticPower
    {
        private static MongoDB mongo = new MongoDB();

        private BsonDocument bson = new BsonDocument();
        private int power = 0;//词频
        private int right = 0;//权限
        private DateTime start;
        internal BsonDocument Reflex(BsonDocument Bson)
        {
            start = DateTime.Now;
            bson = Bson;

            power = mongo.Group(bson);

            List<BsonDocument> read = mongo.Read(bson);
            for (int i = 0; i < read.Count; i++) { }

            Right();

            return bson;
        }
        private void Right()
        {
            int use_time = (int)(DateTime.Now - start).TotalMilliseconds;
            if (use_time < 1000) { right = 1; }
            else if (use_time < 10000) { right = 2; }
            else { right = 3; }
            bson.AddRange(new BsonDocument("right", right));
            bson.AddRange(new BsonDocument("power", power));
        }
        public void ReflexAsync(BsonDocument Bson)
        {
            if (!BSON.Contains("SYSTEM") || !BSON["SYSTEM"].IsBsonDocument)
            {
                BSON.Set("SYSTEM", new BsonDocument("ASYNCCOUNT", 0));
            }
            if (!BSON["SYSTEM"].AsBsonDocument.Contains("ASYNCCOUNT") || !BSON["SYSTEM"]["ASYNCCOUNT"].IsInt32)
            {
                BSON["SYSTEM"].AsBsonDocument.Set("ASYNCCOUNT", 0);
            }
            else
            {
                int asyncCount = (int)BSON["SYSTEM"]["ASYNCCOUNT"];
                if (asyncCount < 10)
                {
                    BSON["SYSTEM"].AsBsonDocument.Set("ASYNCCOUNT", (int)BSON["SYSTEM"]["ASYNCCOUNT"] + 1);
                    bson = new BsonDocument(Bson);
                    bson.Remove("right");
                    bson.Remove("power");
                    mongo.ReadAsync(bson);
                }
                else
                {
                    Console.WriteLine("Please wait after " + asyncCount);
                }
            }
        }
    }
}
//namespace Power
//{
//    class Power
//    {
//        private static MongoDBOperator mongo = new MongoDBOperator();

//        private BsonDocument bson = new BsonDocument();
//        private int artitude = 0;//态度
//        private int power = 0;//词频
//public void Add(string str)
//{
//    porposes_read.Add(str);
//    porposes_create.Add(new BsonDocument("str", str));
//    Read();
//}
//private static void Read()
//{
//    while (true)
//    {
//        lock (locker)
//        {
//            if (porposes_read.Count == 0)
//            {
//                return;
//            }
//            mongo.Read(porposes_read[0]);
//            porposes_read.RemoveAt(0);
//        }
//    }
//}
//private static void Write(object source, ElapsedEventArgs e)
//{
//    if (porposes_create.Count == 0)
//    {
//        porposes_create.Add(new BsonDocument("str", ""));
//    }
//    mongo.Create(porposes_create);
//    porposes_create.Clear();
//}
//private static int TimerStart()
//{
//    System.Timers.Timer timer = new System.Timers.Timer();
//    timer.Elapsed += new ElapsedEventHandler(Write);
//    timer.Interval = 1000;
//    timer.Enabled = true;
//    return 0;
//}

//}
//class Bugu
//{
//    private static List<string> porpose = new List<string>();
//    private static JSON Json = new JSON();
//    private static int count = 0;
//    private static readonly object locker = new object();
//    private static Power power = new Power();

//    public static void add(string str)
//    {
//        porpose.Add(str);
//        WriteRead();
//        //ThreadPool.QueueUserWorkItem(c => { WriteRead(); });
//    }
//    public static void WriteRead()
//    {
//        while (true)
//        {
//            if (porpose.Count == 0)
//            {
//                //Console.WriteLine(Json.disParse());
//                return;
//            }
//            else
//            {
//                string a;
//                lock (locker)
//                {
//                    a = porpose[0];
//                    porpose.RemoveAt(0);
//                    count++;
//                    // Console.Write(count);
//                }
//                TimeSpan ts1 = new TimeSpan(DateTime.Now.Ticks);
//                string ret = Http.Get("https://buguoheng.com/read.php?a=" + a);
//                int ts = ts1.Subtract(new TimeSpan(DateTime.Now.Ticks)).Duration().Milliseconds;
//                lock (locker)
//                {
//                    if (Json[a] == null) { JSON j = new JSON(); j.Add(ts); Json.SetValue(a, j); }
//                    else { Json[a].Add(ts); }
//                    JSON json = JSON.Parse(ret);
//                    for (int i = 0; i < json.Count; i++)
//                    {
//                        if (!string.IsNullOrEmpty(json[i]["a"].ToString()) && Json[a].Count < 2 || a == "bugu")
//                        {
//                            if (json[i]["a"] == "JSON_ARRAY")
//                            {
//                                if (!string.IsNullOrEmpty(json[i]["a"].ToCommaString()))
//                                {
//                                    try
//                                    {
//                                        add(Regex.Unescape(json[i]["a"].ToCommaString()));
//                                        power.Add(Regex.Unescape(json[i]["a"].ToCommaString()));
//                                    }
//                                    catch
//                                    {
//                                        add(json[i]["a"].ToCommaString());
//                                        power.Add(json[i]["a"].ToCommaString());
//                                    }
//                                }
//                            }
//                            else
//                            {
//                                try
//                                {
//                                    add(Regex.Unescape(json[i]["a"]));
//                                    power.Add(Regex.Unescape(json[i]["a"]));
//                                }
//                                catch
//                                {
//                                    add(json[i]["a"]);
//                                    power.Add(json[i]["a"]);
//                                }
//                            }
//                        }
//                    }
//                }
//            }
//        }
//    }
//}
//}
