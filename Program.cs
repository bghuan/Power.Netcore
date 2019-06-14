using MongoDB.Bson;
using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Threading;
using System.Timers;

namespace Power
{
    class Model19
    {
        int int_can_clear = 10;
        int int_repeat_remain = 3;
        BsonArray bson_see = new BsonArray();
        BsonArray bson_know = new BsonArray();
        int int_see { get { return bson_see.Count; } }
        int int_know { get { return bson_know.Count; } }
        int int_clear { get { return int_see / int_know; } }
        public void see(BsonDocument bson)
        {
            bson_see.Add(bson);
            if (bson_see.Count > int_can_clear)
            {
                bson.RemoveAt(0);
            }
            if (bson_see.Contains(bson) && bson_see.Count(s => s == bson) > int_repeat_remain)
            {
                bson_know.Add(bson);
            }
            if (bson_know.Contains(bson) && bson_know.Count(s => s == bson) > int_clear)
            {
                right();
            }
        }
        void right()
        {
            Reflect();
        }
        public void Reflect()
        {
            Console.WriteLine("right.    right should be turn to change the see,and connect to the part of return try clear.");
        }
    }
    ///<summary>
    ///循环去到更加清晰
    ///</summary>
    ///<param name="int_clear">清晰系数</param>
    ///<param name="is_clear">清晰的判断</param>
    ///<param name="not_cleat">当不清晰</param>
    ///<param name="be_clear">当清晰</param>
    class Model18
    {
        int int_clear = 1;
        bool is_clear()
        {
            //suo
            return int_clear > 1;
        }
        public void not_clear()//tend to clear
        {

        }
        public void clear()//tend to not_clear 
        {

        }
    }
    class Model17//Bson,mongoDB shoulen't save so large(wide?) bson about 200kb size
    {
        int pass = 1;
        int count_run;
        int new_key_index;
        int int_list_refresh;
        int int_stdout = 100;
        public bool pause = true;
        Random rnd = new Random();
        BsonArray deep_bson = new BsonArray();
        BsonArray arr_my_bson = new BsonArray();
        BsonDocument my_bson = new BsonDocument();
        BsonDocument current_bson = new BsonDocument();
        readonly string[] props = typeof(BsonValue).GetProperties().Select(prop => prop.PropertyType.Name).Distinct().ToArray();

        public void Listen() { Thread thread = new Thread(Run); thread.Start(); thread.IsBackground = true; }
        public void Dispose() { pause = false; }
        void Run() { while (pause) { Power(); count_run++; } }
        void Power()
        {
            GetPower();
            ReleasePower();
            Stdout();
        }
        void GetPower()
        {
            for (int i = 0; i < current_bson.Count(); i++) { if (current_bson[i].IsBsonDocument) { deep_bson.Add(current_bson[i]); break; } }
            if (deep_bson.Count >= 10)/* depth */ { deep_bson = new BsonArray(); int_list_refresh++; }//why this line can't move
            current_bson = deep_bson.LastOrDefault()?.AsBsonDocument ?? my_bson;
            if (int_list_refresh >= 1000)/* size */ { ReleaseBson(); }
        }
        void ReleasePower() { ChangePower(); Thread.Sleep(pass); }
        void ChangePower() { current_bson.Set(NewKey(), GetBsonValue()); }
        public BsonDocument GetBson() { return my_bson; }
        public BsonArray GetBsons() { if (my_bson.Count() != 0) { arr_my_bson.Add(new BsonDocument(my_bson)); } return arr_my_bson; }
        public void AddBson(BsonValue bson_value) { if (bson_value == null) { } }
        string NewKey() { return new_key_index++.ToString(); }
        void Stdout()
        {
            if (count_run % int_stdout == 0)
            {
                Console.WriteLine(count_run.ToString().PadRight(8, ' ') + Thread.CurrentThread.ManagedThreadId.ToString().PadRight(8, ' ') + int_list_refresh);
            }
        }
        BsonValue GetBsonValue()
        {
            int luck = rnd.Next(props.Length);//props.Length=29
            if (luck < 5) return GetRandomString(rnd.Next(1, 10), false, true, false, false);
            else if (luck < 10) return rnd.Next(10000);
            else if (luck < 15) return rnd.Next(2) == 0 ? true : false;
            else if (luck < 20) return DateTime.Now;
            else if (luck < 25) return new BsonArray();
            else return new BsonDocument();
            //string all_type = "[Boolean, BsonArray, BsonBinaryData, BsonDateTime, BsonDocument, BsonJavaScript, BsonJavaScriptWithScope, BsonMaxKey, BsonMinKey, BsonNull, BsonRegularExpression, BsonSymbol, BsonTimestamp, BsonUndefined, BsonValue, Byte[], DateTime, Decimal, Decimal128, Double, Guid, Int32, Int64, Nullable`1, ObjectId, Regex, String, BsonType, Object]";
            //do
            //{
            //    switch (props[rnd.Next(props.Length)])
            //    {
            //        case "String":
            //            return "";
            //        case "Int32":
            //            return rnd.Next(10000);
            //        case "Boolean":
            //            return new BsonArray { true, false }[rnd.Next(2)];
            //        case "DateTime":
            //            return DateTime.Now;
            //        case "BsonDocument":
            //            return new BsonDocument();
            //        case "BsonArray":
            //            return new BsonArray();
            //    }
            //}
            //while (true);
        }
        #region 5.0 生成随机字符串 + static string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom)
        ///<summary>
        ///生成随机字符串 
        ///</summary>
        ///<param name="length">目标字符串的长度</param>
        ///<param name="useNum">是否包含数字，1=包含，默认为包含</param>
        ///<param name="useLow">是否包含小写字母，1=包含，默认为包含</param>
        ///<param name="useUpp">是否包含大写字母，1=包含，默认为包含</param>
        ///<param name="useSpe">是否包含特殊字符，1=包含，默认为不包含</param>
        ///<param name="custom">要包含的自定义字符，直接输入要包含的字符列表</param>
        ///<returns>指定长度的随机字符串</returns>
        public static string GetRandomString(int length, bool useNum, bool useLow, bool useUpp, bool useSpe, string custom = "")
        {
            byte[] b = new byte[4];
            new System.Security.Cryptography.RNGCryptoServiceProvider().GetBytes(b);
            Random r = new Random(BitConverter.ToInt32(b, 0));
            string s = null, str = custom;
            if (useNum == true) { str += "0123456789"; }
            if (useLow == true) { str += "abcdefghijklmnopqrstuvwxyz"; }
            if (useUpp == true) { str += "ABCDEFGHIJKLMNOPQRSTUVWXYZ"; }
            if (useSpe == true) { str += "!\"#$%&'()*+,-./:;<=>?@[\\]^_`{|}~"; }
            for (int i = 0; i < length; i++)
            {
                s += str.Substring(r.Next(0, str.Length - 1), 1);
            }
            return s;
        }
        #endregion
        public static T CreateInstance<T>(int i = 10)//缺少传入参数,不确定可否用T判断生成规则T
        {
            try
            {
                var props = typeof(BsonValue).GetProperties();
                var prop = props[i];
                var o = prop.GetType();
                object obj = Activator.CreateInstance(o, true);
                return (T)obj;
            }
            catch { }
            return default;
        }
        void ReleaseBson()
        {
            Console.WriteLine("--------------------------ReleaseBson--------------------------");
            Console.WriteLine(my_bson.ToString().Length);
            arr_my_bson.Add(new BsonDocument(my_bson));//C# Bson no size limit no width limit just have depth limit
            my_bson = new BsonDocument();
            deep_bson = new BsonArray();
            current_bson = new BsonDocument();
            int_list_refresh = 0;
        }
        void computer_bson()
        {
            if (my_bson.ToString().Length > Math.Pow(2, 22))
            {
                my_bson.RemoveElement(my_bson.LastOrDefault());
            }
        }
    }
    class Program : StaticPower
    {
        private static BsonDocument bson_program = new BsonDocument();
        private static System.Timers.Timer timer;
        private static readonly int r = TimerStart();
        private static Random rnd = new Random();
        private static int blockSecondCount = 0;
        private static int bsonArrayTick = 0;
        private static void Main()
        {
            var model = new Model17();
            model.Listen();
            new MongoDB().Read(new BsonDocument("time", DateTime.Now.ToString()));
            BsonValue bsonValue;
            while ((bsonValue = Console.ReadLine()).ToString().ToLower() != "exit")
            {
                if (bsonValue == "")
                {
                    model.pause = !model.pause;
                    model.Listen();
                    continue;
                }
                if (bsonValue == "save")
                {
                    Helper.Save(model.GetBsons());
                    continue;
                }
                model.AddBson(bsonValue);
                continue;
                Interactive(bsonValue);
            }
            model.Dispose();
            if (bsonValue != null && bsonValue.ToString().ToLower() == "exit") { Helper.Save(model.GetBsons()); }
        }
        private static void Interactive(BsonValue bsonValue)
        {
            switch (bsonValue.ToString())
            {
                case "FRESHEN":
                    BSON = new BsonDocument();
                    break;
                case "STOP":
                    if (timer.Enabled == true) { timer.Enabled = false; }
                    break;
                case "BSON":
                    Console.WriteLine(BSON);
                    break;
                case "bsonArray":
                    Console.WriteLine(bsonArray);
                    break;
                case "SYSTEM":
                    {
                        Console.WriteLine(BSON.Contains("SYSTEM") ? BSON["SYSTEM"] : BSON);
                        Console.Write("Type the key  :");
                        string key = Console.ReadLine();
                        Console.Write("Type the value:");
                        BsonValue value = Console.ReadLine();
                        try { BSON["SYSTEM"].AsBsonDocument.Set(key, value); }
                        catch { BSON.Set("SYSTEM", new BsonDocument(key, value)); }
                        finally { Console.WriteLine(BSON["SYSTEM"]); }
                    }
                    break;
                default:
                    {
                        if (BSON.Contains("SYSTEM") && BSON["SYSTEM"].IsBsonDocument && BSON["SYSTEM"].AsBsonDocument.Contains(bsonValue.ToString()))
                        {
                            Console.Write("SYSTEM:" + bsonValue + ":");
                            BsonValue value = Console.ReadLine();
                            bson_program = new BsonDocument(bsonValue.ToString(), value);
                            return;
                        }
                        Power power = new Power();
                        BsonDocument bson = new BsonDocument("str", bsonValue);
                        BsonDocument ret;
                        if (bson_program != null) { bson.AddRange(bson_program); }
                        if (BSON.Contains(bsonValue.ToString()) && BSON[bsonValue.ToString()].IsBsonDocument)
                        {
                            ret = BSON[bsonValue.ToString()].AsBsonDocument;//极快,内存,错了吗,不知道极限.至少有能力把bson保存到表里,读取或许有两套
                            ret.Set("_id", ObjectId.GenerateNewId());
                            ret.Set("power", (int)ret["power"] + 1);
                            power.ReflexAsync(ret);
                            Right(ret);
                        }
                        else
                        {
                            ret = power.Reflex(bson);
                            BSON.Set(bsonValue.ToString(), ret);
                            Right(ret);
                        }
                        LimitPower.Add(new BsonDocument(bsonValue.ToString(), new BsonDocument(ret)));
                    }
                    break;
            }
        }
        private static void Right(BsonDocument Bson)
        {
            int right = 0;
            if (Bson.Contains("right") && Bson["right"].IsInt32)
            {
                right = (int)Bson["right"];
            }
            switch (right)
            {
                case 1:
                    Console.WriteLine(Bson);
                    break;
                case 2:
                    Console.WriteLine("2:" + Bson);
                    break;
                case 3:
                    Console.WriteLine("refuse");
                    break;
                default:
                    Console.WriteLine("can't");
                    break;
            }
            blockSecondCount = 0;
            bsonArray.Add(Bson);
        }
        private static int TimerStart()
        {
            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(TimerFunction);
            timer.Interval = 1000;
            timer.Enabled = true;
            return 0;
        }
        private static void TimerFunction(object source, ElapsedEventArgs e)
        {
            if (blockSecondCount < 100)
            {
                blockSecondCount++;
            }
            else if (bsonArray.Count() != 0)
            {
                bsonArrayTick = rnd.Next(0, 1000);//bsonArrayTick++;
                blockSecondCount = 0;
                var bson = bsonArray[bsonArrayTick % bsonArray.Count()];
                if (bson.IsBsonDocument && bson.AsBsonDocument.Contains("str"))
                {
                    Console.WriteLine(bson["str"]);
                    Interactive(bson["str"].ToString());
                }
            }
        }
    }
    class LimitPower//混合两个Bson以BsonArray为标准 限度 false
    {
        protected static BsonDocument BaseBson = new BsonDocument();
        static int num1 = 100;
        static int num2 = BaseBson.Count();
        static string str1 = "";
        static bool over = num2 > num1;
        public static void Add(BsonDocument Bson)
        {
            if (over)
            {
                BaseBson.Remove(str1);
            }
            else
            {
                Mix(BaseBson, Bson);
            }
            //Console.WriteLine(BaseBson);
        }
        public static void Mix(BsonDocument BaseBson, BsonDocument Bson)
        {
            BsonArray bsonArray;
            foreach (var b in Bson)//Bson.kv
            {
                if (BaseBson.Contains(b.Name))//if(BaseBson.key==Bson.key)
                {
                    bsonArray = new BsonArray();
                    if (!BaseBson[b.Name].IsBsonArray)
                    {
                        bsonArray.Add(BaseBson[b.Name]);
                        BaseBson.Set(b.Name, new BsonArray());
                    }
                    bsonArray.Add(b.Value);
                    //BaseBson[b.Name].AsBsonArray.Add(bsonArray);//BaseBson[key].Add(Bson.value)
                    for (int i = 0; i < bsonArray.Count; i++)
                    {
                        BaseBson[b.Name].AsBsonArray.Add(bsonArray[i]);
                    }
                    continue;
                }
                else
                {
                    BaseBson.AddRange(new BsonDocument(b));//BaseBson.key=Bson.value
                }
            }
        }
    }
    class Model//模型 false
    {
        static int int_move;
        static System.Timers.Timer timer;
        static int init = Initialization();
        static int surviualSecond;
        static int conscious;
        static Random rnd = new Random();
        static int start = 0;
        static int end = 5;
        static bool judge() { return int_move < 100 || int_move > 100; }
        static int Initialization()
        {
            Console.WriteLine("Initialization...");
            int_move = 0;
            surviualSecond = 0;
            timer = new System.Timers.Timer();
            timer.Elapsed += new ElapsedEventHandler(TimerFunction);
            timer.Interval = 1000;
            timer.Enabled = true;
            return 0;
        }
        static void TimerFunction(object source, ElapsedEventArgs e)
        {
            int_move--;
            Console.WriteLine(surviualSecond + " " + int_move);
        }
        public static void surviual()
        {
            while (true)
            {
                if (judge())
                {
                    Console.WriteLine("game over:surviualSecond=" + surviualSecond + " start=" + start + " end=" + end);
                    Initialization();
                    start = rnd.Next(0, 5);
                    end = rnd.Next(5, 10);
                    Console.WriteLine("start new");
                }
                try
                {
                    conscious = rnd.Next(start, end);
                    Console.WriteLine(conscious);
                    Right();
                    Thread.Sleep(1000);
                }
                catch
                {
                    // judge = true;
                }
            }
        }
        static void Right()
        {
            if (conscious == 0)
            {
                int_move++;
            }
            if (conscious == 1)
            {
                int_move += 4;
            }
            if (conscious == 2)
            {
                int_move += 8;
            }
            if (conscious == 3)
            {
                int_move--;
            }
            if (conscious == 4)
            {
                int_move -= 5;
            }
            if (conscious == 5)
            {
                int_move -= 10;
            }
        }
    }
    class Model2//递归循环 blank
    {

    }
    class Model3//预先的出是否超出而决定表达那一个权限 false
    {
        static int self = 0;
        static bool Over() { return self == 0; }
        Random rnd = new Random();
        static int r;
        static BsonDocument Bson = new BsonDocument();
        public void Run()
        {
            while (true)
            {
                Thread.Sleep(1000);
                r = rnd.Next(-1, 2);
                Fun2(r);
            }
        }
        public void Fun2(int r)
        {
            self += r;
            Fun3();
            Console.WriteLine(Bson);
        }
        public void Fun3()
        {
            if (Bson.Contains(self.ToString()) && Bson[self.ToString()].IsBsonDocument)
            {
                if (Bson[self.ToString()].AsBsonDocument.Contains(r.ToString()))
                {
                    if (Bson[self.ToString()][r.ToString()].IsBoolean)
                    {

                    }
                    else
                    {
                        Bson[self.ToString()].AsBsonDocument.Set(r.ToString(), Over());
                    }
                }
                else
                {
                    Bson[self.ToString()].AsBsonDocument.Set(r.ToString(), Over());
                }
            }
            else
            {
                Bson.Set(self.ToString(), new BsonDocument(r.ToString(), Over()));
            }
        }
    }
    class Model4//不断自身循环 blank
    {

    }
    class Model6//暴露态度可调节,执行权限为表达,短到一次即修改
    {
        //面对多种来源
        //由要求而触碰
        //加权获得反馈的触碰
        //面对多种来源
        //由要求而从词频中取出触碰
        //加权反馈
        //面对
        static BsonDocument Bson = new BsonDocument();
        static bool Require = true;
        static int[] int_Init = new int[] { 0, 1, 2 };
        static int[] power = new int[] { 0, 1, 2 };
        static Random rnd = new Random();
        static int r = Init();
        static int GetPower()
        {
            return power[rnd.Next(0, power.Length)];
        }
        static public int Init()
        {
            for (int i = 0; i < int_Init.Length; i++)
            {
                Bson.Set(int_Init[i].ToString(), int_Init[i]);
            }
            return 0;
        }
        public void Run()
        {
            string str;
            while ((str = Console.ReadLine()) != "exit")
            {
                if (Require)
                {
                    if (Bson.Contains(str))
                    {
                        Console.WriteLine(Bson[str]);
                        Console.Write("Enter :");
                        if (Console.ReadLine() == "")
                        {

                        }
                        else
                        {
                            Bson.Set(str, GetPower());
                        }
                    }
                    else
                    {
                        Bson.Set(str, GetPower());
                        Console.WriteLine(Bson[str]);
                    }
                }
            }
        }
    }
    class Model7//寻找占据时间的方法 用得出随机数来等待占用并记录间隔使间隔趋近固定
    {
        static int int_1 = 0;
        static int int_2 = 1;
        static long end = 999;
        public static void Use_time()
        {
            int time = int_2 * 1000;
            Console.WriteLine("Random(1," + end + ")");
            Console.WriteLine("use time about:" + time + "/ms");
            Thread.Sleep(time);
            Console.WriteLine("");
        }
        static int r = Func1();
        static int Func1()
        {
            while (true)
            {
                try { ads(); }
                catch (SystemException ex) { Console.WriteLine(ex.ToString()); end = 999; ads(); }
            }
        }
        static void ads()
        {
            var start = DateTime.Now;
            Random rnd = new Random();
            int i = rnd.Next(1, (int)end);
            if (i == 1)
            {
                Use_time();
            }
            int tick = (int)(DateTime.Now - start).TotalMilliseconds;
            if (tick < 100 && end > 11)
            {
                end -= 10;
            }
            else if (tick < 1000)
            {
                end++;
            }
            else
            {
                end += 10;
            }
        }
    }
    class Model9//两组river词频存在,表达词频,变成两个kv 字符串不清晰
    {
        static BsonArray bsonArray = new BsonArray() { };
        static int r = Initialization();
        static int Initialization()
        {
            Mean("a");
            Mean("b");
            while (true)
            {
                Mean(Console.ReadLine());
            }
        }
        public static void Mean(string str)
        {
            BsonArray sameBson = new BsonArray() { };
            for (int i = 0; i < bsonArray.Count(); i++)
            {
                if (bsonArray[i] == str)
                {
                    try { sameBson.Add(i); }
                    catch (SystemException ex) { Console.WriteLine(ex); }
                }
            }
            for (int j = 0; j < sameBson.Count; j++)
            {
                int k = (int)sameBson[j];
                try { Console.Write(bsonArray[(int)sameBson[j]]); }
                catch (SystemException ex) { Console.WriteLine(ex); }
                try
                {
                    if (k != 0 && bsonArray.LastOrDefault() == bsonArray[k - 1])
                    {
                        bsonArray.Add(new BsonDocument(bsonArray[k - 1].ToString(), str));
                    }
                    Console.WriteLine(bsonArray[k + 1]);
                }
                catch (SystemException ex) { Console.WriteLine(ex); }
                finally { Console.WriteLine(bsonArray); }
            }
            bsonArray.Add(str);//不是主动是在一起的有意义,是那种词频就注定在一起就有意义吗,近,近的排序
        }
    }
    class Model10//方法占据的时间可以被调节,是否是限度系数,记忆中重复判断,保存最近系数长度变量组合变量判断是否contains like 习惯
    {
        static int ms = -1;
        static int len = 20;
        static int limit_time = 100;
        static int r = Func1();
        static int Func1()
        {
            BsonArray bsonArray1 = new BsonArray();
            BsonArray bsonArray2 = new BsonArray();
            BsonArray bsonArray3 = new BsonArray();
            bool same = true;
            int num1 = 0;
            while (true)
            {
                int time = Ref1();

                if (bsonArray2.Count >= len)
                {
                    bsonArray3 = new BsonArray();
                    for (int i = 1; i < len + 1; i++)
                    {
                        bsonArray3.Add(bsonArray2[bsonArray2.Count - i]);
                    }
                    if (bsonArray1.Contains(bsonArray3))
                    {
                        continue;
                    }
                    else
                    {
                        bsonArray1.Add(bsonArray3);//每次添加都另存一个最末长度的数组,相当于放个大数据len=20,20倍
                    }
                }

                bsonArray2.Add(num1);
                if (time >= 0 && time < limit_time)
                {
                    if (same) { num1++; }//latest ms+ true
                    else { num1 = 1; }
                    ms += Fib(num1);
                    same = true;
                }
                else
                {
                    if (!same) { num1++; }
                    else { num1 = 1; }
                    ms -= Fib(num1);
                    same = false;
                }
                Console.WriteLine("      " + Fib(num1));
            }
        }
        static int Ref1()
        {
            var start = DateTime.Now;
            if (ms >= 0)
            {
                Thread.Sleep(ms);
                Console.Write(ms);
            }
            else
            {
                Console.WriteLine("err   :" + ms + "\n请重新定义当前占据时间 ms :");
                try { ms = int.Parse(Console.ReadLine()); }
                catch { }
                return 0;
            }
            return (int)(DateTime.Now - start).TotalMilliseconds;
        }
        public static int Fib(int num)
        {
            if (num <= 0)
                return 0;
            else if (num > 0 && num <= 2)
                return 1;
            else
                return Fib(num - 1) + Fib(num - 2);
        }
    }
    class Model11//分流所有权限 两个占据时间为何要成比例 false 引入态度重写态度 false 碰壁更换权限 然后加入习惯?
    {
        static int ms = 0;
        static int limit_ms = 100;
        static int ms_tick = 0;
        static int limit_ms_tick = 1000;
        static string RegexStr = "^Ref[0-9a-f]{1,}";
        static BsonArray Refs = new BsonArray();
        static BsonDocument attitude = new BsonDocument();//对象,变量,反馈,,权限 却只有对象和反馈
        public int Run()
        {
            bool same = true;
            int num1 = 0;
            int time = 0;
            InitRefs();
            while (true)
            {
                MethodInfo mi = GetRefByAttitude();
                if (mi == null) { continue; }
                else if (mi.ReturnType.Name == "Int32") { time = (int)mi.Invoke(this, null); }
                else { }
                attitude.Set(mi.Name, (ms_tick >= 0 && ms_tick < limit_ms_tick));
                ms_tick += time;

                if (time >= 0 && time < limit_ms)
                {
                    if (same) { num1++; }
                    else { num1 = 1; }
                    ms += Fib(num1);
                    same = true;
                }
                else
                {
                    if (!same) { num1++; }
                    else { num1 = 1; }
                    ms -= Fib(num1);
                    same = false;
                }
            }
        }
        public static int Ref1()
        {
            var start = DateTime.Now;
            if (ms >= 0) { Thread.Sleep(ms); }
            Console.WriteLine(ms);
            return (int)(DateTime.Now - start).TotalMilliseconds;
        }
        public static int Ref2()
        {
            special();
            var start = DateTime.Now;
            if (ms >= 0) { Thread.Sleep(ms); }
            Console.WriteLine(ms);
            return (int)(DateTime.Now - start).TotalMilliseconds;
        }
        static void special() { ms_tick = -1; Console.WriteLine("this is special method"); }
        private void InitRefs()
        {
            MethodInfo[] methods = GetType().GetMethods();//GetType()|static:typeof(Model11)
            foreach (MethodInfo method in methods) { if (Regex.IsMatch(method.Name, RegexStr)) { Refs.Add(method.Name); } }
        }
        private MethodInfo GetRefByAttitude()
        {
            if (Refs.Count > 0)
            {
                foreach (string method in Refs) { if (attitude.Contains(method) && attitude[method].AsBoolean) { return GetType().GetMethod(method); } }
                foreach (string method in Refs) { attitude.Set(method, true); }
                return GetType().GetMethod((string)Refs[0]);
            }
            return null;
        }
        static int Fib(int num)
        {
            if (num <= 0) { return 0; }
            else if (num > 0 && num <= 2) { return 1; }
            else { return Fib(num - 1) + Fib(num - 2); }
        }
    }
    class Helper//添加获取所有权限
    {
        private static readonly string RegexStr = "^Ref[0-9a-f]{1,}";
        public static BsonArray GetRefs(Type t)
        {
            BsonArray bsonArray = new BsonArray();
            MethodInfo[] methods = t.GetMethods();//BindingFlags.NonPublic | BindingFlags.Instance
            for (int i = 0; i < methods.Count(); i++)
            {
                if (Regex.IsMatch(methods[i].Name, RegexStr)) { bsonArray.Add(methods[i].Name); }
            }
            return bsonArray;
        }
        public static void Save(BsonDocument bson)
        {
            // new MongoDB().Create(bson);
            string dir = "./log/";
            if (Directory.Exists(dir) == false) { Directory.CreateDirectory(dir); }
            DirectoryInfo root = new DirectoryInfo(dir);
            FileInfo[] files = root.GetFiles();
            int[] arr = new int[files.Count()];
            for (int i = 0; i < files.Count(); i++)
            {
                try { arr[i] = int.Parse(files[i].Name.Split('.')[0]); }
                catch { }
            }
            string file_name;
            if (arr.Length == 0 || arr.Max().ToString().IndexOf(DateTime.Now.ToString("Mdd")) != 0) { file_name = DateTime.Now.ToString("Mdd") + "01"; }
            else { file_name = (arr.Max() + 1).ToString(); }
            if (Directory.Exists(dir) == false) { Directory.CreateDirectory(dir); }
            StreamWriter sw = new StreamWriter(dir + file_name + ".log");
            sw.Write(bson);
            sw.Close();
        }
        public static void Save(BsonArray bson)
        {
            for (int i = 0; i < bson.Count; i++)
            {
                Save(bson[i].AsBsonDocument);
            }
        }
    }
    class Model12//要求表达的词频,要求,有权限,尝试权限,循环?不需要预知 16:27 添加多要求多词频 更改词频与表达存放在bson bson混乱
    {
        static BsonArray bsonArray = new BsonArray();
        static BsonDocument power = new BsonDocument();
        static BsonArray Refs = Helper.GetRefs(typeof(Model12));
        static Power mongo = new Power();
        static int r = Init();
        static readonly string mongo_key = MethodBase.GetCurrentMethod().DeclaringType.FullName.Replace('.', '_');
        static int Init()
        {
            power.AddRange(new BsonDocument("breath", new BsonDocument() { { "tick", 0 }, { "require", 2000 }, { "release", 2000 }, { "add_power_bytime", 1 }, { "refs", new BsonArray() { } } }));
            power.AddRange(new BsonDocument("jump", new BsonDocument() { { "tick", 0 }, { "require", 10000 }, { "release", 8000 }, { "add_power_bytime", 1 }, { "refs", new BsonArray() { } } }));
            return 0;
        }
        public void Run()
        {
            var start = DateTime.Now;
            int time = 0;
            while (true)
            {
                if ((DateTime.Now - start).TotalMilliseconds > 100)
                {
                    Reflex();
                    time = (int)(DateTime.Now - start).TotalMilliseconds;
                    start = DateTime.Now;
                    addPowerByTime(time);
                }
            }
        }
        string GetPower()//可以监听某些数据就像感官一样
        {
            BsonDocument bson;
            foreach (var p in power)
            {
                if (p.Value.IsBsonDocument)
                {
                    bson = new BsonDocument(p.Value.AsBsonDocument);
                    if (bson.Contains("require") && bson["require"].IsInt32)
                    {
                        if (bson.Contains("tick") && bson["tick"].IsInt32)
                        {
                            if (bson["tick"] > bson["require"])
                            {
                                return p.Name;
                            }
                        }
                    }
                }
            }
            return null;
        }
        void addPowerByTime(int num)
        {
            for (int i = 0; i < power.Count(); i++)
            {
                power[i].AsBsonDocument.Set("tick", (int)power[i].AsBsonDocument["tick"] + (int)power[i].AsBsonDocument["add_power_bytime"] * num);
            }
        }
        void Reflex()
        {
            string Name = GetPower();
            if (power.Contains(Name) && power[Name].IsBsonDocument)
            {
                Console.Write("require:" + Name);
                MethodInfo method = ChooseRef(Name);
                method.Invoke(this, null);
            }
            else if (Name != null)
            {
                CreatePower(Name);
            }
            TimeListen(Name);
        }
        void TimeListen(string Name)
        {
            if (GetPower() != null)//反馈负面
            {
                power[Name].AsBsonDocument["refs"].AsBsonArray.Remove(Name);
                Console.WriteLine(Name + " false");
            }
        }
        public void Ref1()
        {
            string str = "breath";
            var start = DateTime.Now;
            BsonDocument bson = power[str].AsBsonDocument;
            Thread.Sleep(1000);
            bson.Set("tick", (int)bson["tick"] - (int)bson["release"]);
            bson.Set("pass", (DateTime.Now - start).TotalMilliseconds);
            Console.WriteLine(string.Format("{0}:占用:{1},释放:{2},当前:{3}/{4}", str.PadLeft(10, ' '), bson["pass"], bson["release"], bson["tick"], bson["require"]));
            if (!bson["refs"].AsBsonArray.Contains(MethodBase.GetCurrentMethod().Name)) { bson["refs"].AsBsonArray.Add(MethodBase.GetCurrentMethod().Name); }
            bsonArray.Add(new BsonDocument { { str, new BsonDocument() { { "time", DateTime.Now }, bson } } });
            mongo.Reflex(new BsonDocument(mongo_key, new BsonDocument() { { str, new BsonDocument() { { "time", DateTime.Now }, bson } } }));
        }
        public void Ref2()
        {
            string str = "jump";
            var start = DateTime.Now;
            BsonDocument bson = power[str].AsBsonDocument;
            Console.Write("ing   ");
            Thread.Sleep(2000);
            bson.Set("tick", (int)bson["tick"] - (int)bson["release"]);
            bson.Set("pass", (DateTime.Now - start).TotalMilliseconds);
            Console.WriteLine(string.Format("{0}:占用:{1},释放:{2},当前:{3}/{4}", str.PadLeft(10, ' '), bson["pass"], bson["release"], bson["tick"], bson["require"]));
            if (!bson["refs"].AsBsonArray.Contains(MethodBase.GetCurrentMethod().Name)) { bson["refs"].AsBsonArray.Add(MethodBase.GetCurrentMethod().Name); }
            bsonArray.Add(new BsonDocument() { { str, new BsonDocument() { { "time", DateTime.Now }, bson } } });
            mongo.Reflex(new BsonDocument(mongo_key, new BsonDocument() { { str, new BsonDocument() { { "time", DateTime.Now }, bson } } }));
        }
        MethodInfo ChooseRef(string Name)
        {
            if (Refs.Count() == 0) { return null; }
            Random rnd = new Random();
            int index = rnd.Next(0, Refs.Count());
            string str_method = (string)Refs[index];
            BsonDocument bson = power[Name].AsBsonDocument;
            if (bson["refs"].IsBsonArray && bson["refs"].AsBsonArray.Count() > 0) { str_method = (string)bson["refs"][0]; }
            return GetType().GetMethod(str_method);
        }
        void CreatePower()
        {
            int i = 0;
            while (true)
            {
                if (!power.Contains(i.ToString()))
                {
                    power.AddRange(new BsonDocument(i.ToString(), new BsonDocument() { { "tick", 0 }, { "require", 10000 }, { "release", 8000 }, { "add_power_bytime", 1 }, { "refs", new BsonArray() { } } }));
                    break;
                }
                i++;
            }
        }
        bool CreatePower(string Name)
        {
            if (!power.Contains(Name))
            {
                power.AddRange(new BsonDocument(Name, new BsonDocument() { { "tick", 0 }, { "require", 10000 }, { "release", 8000 }, { "add_power_bytime", 1 }, { "refs", new BsonArray() { } } }));
                return true;
            }
            return false;
        }
    }
    class Model13//inherit Model12 false 最初是为了程度的变化吗
    {
        static readonly string mongo_key = MethodBase.GetCurrentMethod().DeclaringType.FullName.Replace('.', '_');
        static BsonArray bsonArray = new BsonArray();
        static BsonDocument power = new BsonDocument();
        static BsonArray Refs = Helper.GetRefs(typeof(Model12));
        static Power mongo = new Power();
        static int r = Init();
        static BsonArray str_Ref1 = new BsonArray("like listen");
        static int Init()
        {
            power.AddRange(new BsonDocument("entropy", new BsonDocument() { { "tick", 0 }, { "require", new BsonDocument() { { "max", 2000 }, { "min", 20 } } }, { "add_power_bytime", 0 }, { "refs", new BsonArray() { } } }));
            return 0;
        }
        public void Run()
        {
            var start = DateTime.Now;
            int time = 0;
            while (true)
            {
                if ((DateTime.Now - start).TotalMilliseconds > 100)
                {
                    Reflex();
                    time = (int)(DateTime.Now - start).TotalMilliseconds;
                    start = DateTime.Now;
                    addPowerByTime(time);
                }
            }
        }
        string GetPower()//可以监听某些数据就像感官一样
        {
            BsonDocument bson;
            foreach (var p in power)
            {
                if (p.Value.IsBsonDocument)
                {
                    bson = new BsonDocument(p.Value.AsBsonDocument);
                    if (bson.Contains("require") && bson["require"].IsInt32)
                    {
                        if (bson.Contains("tick") && bson["tick"].IsInt32)
                        {
                            if (bson["tick"] > bson["require"])
                            {
                                return p.Name;
                            }
                        }
                    }
                }
            }
            return null;
        }
        void addPowerByTime(int num)
        {
            for (int i = 0; i < power.Count(); i++)
            {
                power[i].AsBsonDocument.Set("tick", (int)power[i].AsBsonDocument["tick"] + (int)power[i].AsBsonDocument["add_power_bytime"] * num);
            }
        }
        void Reflex()
        {
            string Name = GetPower();
            if (power.Contains(Name) && power[Name].IsBsonDocument)
            {
                Console.Write("power:" + Name);
                MethodInfo method = ChooseRef(Name);
                method.Invoke(this, null);
            }
            else if (Name != null)
            {
                CreatePower(Name);
            }
            TimeListen(Name);
        }
        void TimeListen(string Name)
        {
            if (GetPower() != null)//反馈负面
            {
                power[Name].AsBsonDocument["refs"].AsBsonArray.Remove(Name);
                Console.WriteLine(Name + " false");
            }
        }
        public void Ref1(string Name)
        {
            var start = DateTime.Now;
            BsonDocument move = power["move"].AsBsonDocument;
            BsonDocument bson = power[Name].AsBsonDocument;
            Thread.Sleep(1000);
            Console.WriteLine(str_Ref1[0]);
            move.Set("tick", (int)move["tick"] - (int)bson["release"]);
            bson.Set("pass", (DateTime.Now - start).TotalMilliseconds);
            Console.WriteLine(string.Format("{0}:占用:{1},释放:{2},当前:{3}/{4}", Name.PadLeft(10, ' '), bson["pass"], bson["release"], move["tick"], move["require"]));
            if (!move["refs"].AsBsonArray.Contains(MethodBase.GetCurrentMethod().Name)) { move["refs"].AsBsonArray.Add(MethodBase.GetCurrentMethod().Name); }
            bsonArray.Add(new BsonDocument() { { Name, new BsonDocument() { { "time", DateTime.Now }, bson } } });
            mongo.Reflex(new BsonDocument(mongo_key, new BsonDocument() { { Name, new BsonDocument() { { "time", DateTime.Now }, bson } } }));
        }
        public void Ref2()
        {
            string str = "quiet";
            var start = DateTime.Now;
            BsonDocument bson = power[str].AsBsonDocument;
            Console.Write("ing   ");
            Thread.Sleep(2000);
            bson.Set("tick", (int)bson["tick"] - (int)bson["release"]);
            bson.Set("pass", (DateTime.Now - start).TotalMilliseconds);
            Console.WriteLine(string.Format("{0}:占用:{1},释放:{2},当前:{3}/{4}", str.PadLeft(10, ' '), bson["pass"], bson["release"], bson["tick"], bson["require"]));
            if (!bson["refs"].AsBsonArray.Contains(MethodBase.GetCurrentMethod().Name)) { bson["refs"].AsBsonArray.Add(MethodBase.GetCurrentMethod().Name); }
            bsonArray.Add(new BsonDocument() { { str, new BsonDocument() { { "time", DateTime.Now }, bson } } });
            mongo.Reflex(new BsonDocument(mongo_key, new BsonDocument() { { str, new BsonDocument() { { "time", DateTime.Now }, bson } } }));
        }
        MethodInfo ChooseRef(string Name)
        {
            if (Refs.Count() == 0) { return null; }
            Random rnd = new Random();
            int index = rnd.Next(0, Refs.Count());
            string str_method = (string)Refs[index];
            BsonDocument bson = power[Name].AsBsonDocument;
            if (bson["refs"].IsBsonArray && bson["refs"].AsBsonArray.Count() > 0) { str_method = (string)bson["refs"][0]; }
            return GetType().GetMethod(str_method);
        }
        void CreatePower()
        {
            int i = 0;
            while (true)
            {
                if (!power.Contains(i.ToString()))
                {
                    power.AddRange(new BsonDocument(i.ToString(), new BsonDocument() { { "tick", 0 }, { "require", 10000 }, { "release", 8000 }, { "add_power_bytime", 1 }, { "refs", new BsonArray() { } } }));
                    break;
                }
                i++;
            }
        }
        bool CreatePower(string Name)
        {
            if (!power.Contains(Name))
            {
                power.AddRange(new BsonDocument(Name, new BsonDocument() { { "tick", 0 }, { "require", 10000 }, { "release", 8000 }, { "add_power_bytime", 1 }, { "refs", new BsonArray() { } } }));
                return true;
            }
            return false;
        }
    }
    class Model14//group 曾存在 太浅
    {
        public BsonDocument record_bson = new BsonDocument();
        public BsonArray GetPowerByName(string Name)
        {
            if (record_bson.Contains(Name) && record_bson[Name].IsBsonArray) { return record_bson[Name].AsBsonArray; }
            return null;
        }
        public void SetPowerByName(string Key, BsonValue Value)
        {
            if (record_bson.Contains(Key) && record_bson[Key].IsBsonArray) { record_bson[Key].AsBsonArray.Add(Value); }
            else { record_bson.Set(Key, new BsonArray() { Value }); }
        }
        public void Run()
        {
            while (true)
            {
                Console.Write("group:");
                string asd = Console.ReadLine();
                if (asd == "bson")
                {
                    Console.WriteLine(record_bson);
                    continue;
                }
                if (asd == "admin")
                {
                    Console.Write("key");
                    string assdad = Console.ReadLine();
                    Console.Write("value");
                    string adsassdad = Console.ReadLine();
                    SetPowerByName(assdad, adsassdad);
                    continue;
                }
                BsonArray dsa = GetPowerByName(asd);
                if (dsa == null)
                {
                    Console.WriteLine("未查询到匹配内容");
                }
                else
                {
                    Random rnd = new Random();
                    BsonValue b = dsa[rnd.Next(0, dsa.Count())];
                    Console.WriteLine(b);
                }
            }
        }
    }
    class Model15//new Thread Listen true
    {
        //Model15 Model = new Model15();
        //Thread thread=new Thread(Model.Listen);
        //thread.Start();
        //BsonValue bsonValue;
        //while ((bsonValue = Console.ReadLine()).ToString().ToLower() != "exit")
        //{
        //    Model.bsonArray.Add(bsonValue);
        //    Interactive(bsonValue);
        //}
        public BsonArray bsonArray = new BsonArray();
        public void Listen()
        {
            while (true)
            {
                Run();
                Thread.Sleep(1000);
            }
        }
        private void Run()
        {
            if (bsonArray.Count() > 0)
            {
                Console.WriteLine(bsonArray);
                bsonArray.RemoveAt(0);
            }
        }
    }
    class Model16//mix thread listen and shunt power like stdin
    {
        public BsonArray my_bsonArray = new BsonArray();
        DateTime start = DateTime.Now;
        int time = 0;
        static readonly string mongo_key = MethodBase.GetCurrentMethod().DeclaringType.FullName.Replace('.', '_');
        static BsonArray bsonArray = new BsonArray();
        static BsonDocument power = new BsonDocument();
        static Power mongo = new Power();
        static int r = Init();
        static BsonArray Refs;
        static int KeyIndex = 0;
        static string CreateKey()
        {
            return KeyIndex++.ToString();
        }
        static int Init()
        {
            power.AddRange(new BsonDocument("entropy", new BsonDocument() { { "tick", 0 }, { "require", 10000 }, { "add_power_bytime", 1 }, { "refs", new BsonArray() { } } }));
            power.AddRange(new BsonDocument("my_bsonArray", new BsonDocument() { { "tick", 0 }, { "require", 1 }, { "add_power_bytime", 0 }, { "refs", new BsonArray() { } } }));
            power.AddRange(new BsonDocument("test1", new BsonDocument() { { "power", "test2" } }));
            power.AddRange(new BsonDocument("test2", new BsonDocument() { { "refs", new BsonArray() { } }, { "release", 100 } }));
            power.AddRange(new BsonDocument("test3", new BsonDocument() { { "1", true }, { "0", false } }));
            power.AddRange(new BsonDocument(CreateKey(), new BsonDocument() { { "1", power["test3"] }, { "0", false } }));
            return 0;
        }
        public void Listen()
        {
            Refs = Helper.GetRefs(GetType());
            while (true)
            {
                lock (my_bsonArray)
                {
                    try { Run(); }
                    catch { }
                    Thread.Sleep(100);
                }
            }
        }
        private void Run()
        {
            Reflex();
            time = (int)(DateTime.Now - start).TotalMilliseconds;
            start = DateTime.Now;
            addPowerByTime(time);
        }
        string GetPower()//可以监听某些数据就像感官一样
        {
            if (my_bsonArray.Count > 0)
            {
                return "my_bsonArray";
            }
            BsonDocument bson;
            foreach (var p in power)
            {
                if (p.Value.IsBsonDocument)
                {
                    bson = new BsonDocument(p.Value.AsBsonDocument);
                    if (bson.Contains("require") && bson["require"].IsInt32)
                    {
                        if (bson.Contains("tick") && bson["tick"].IsInt32)
                        {
                            if (bson["tick"] > bson["require"])
                            {
                                return p.Name;
                            }
                        }
                    }
                }
            }
            return null;
        }
        void addPowerByTime(int num)
        {
            for (int i = 0; i < power.Count(); i++)
            {
                power[i].AsBsonDocument.Set("tick", (int)power[i].AsBsonDocument["tick"] + (int)power[i].AsBsonDocument["add_power_bytime"] * num);
            }
        }
        void Reflex()
        {
            string Name = GetPower();
            if (power.Contains(Name) && power[Name].IsBsonDocument)
            {
                Console.Write("power:" + Name);
                MethodInfo method = ChooseRef(Name);
                method.Invoke(this, null);
            }
            else if (Name != null)
            {
            }
            TimeListen(Name);
        }
        void TimeListen(string Name)
        {
            if (GetPower() == "my_bsonArray") { }
            else if (GetPower() != null)//反馈负面
            {
                power[Name].AsBsonDocument["refs"].AsBsonArray.Remove(Name);
                Console.WriteLine(Name + " false");
            }
        }
        public void Ref3()
        {
            if (my_bsonArray.Count > 0)
            {
                string str = "my_bsonArray";
                var start = DateTime.Now;
                BsonDocument bson = power[str].AsBsonDocument;
                Console.WriteLine(my_bsonArray[0]);
                my_bsonArray.RemoveAt(0);
                bson.Set("tick", 0);
                bson.Set("pass", (DateTime.Now - start).TotalMilliseconds);
                //Console.WriteLine(string.Format("{0}:占用:{1},当前:{3}/{4}", str.PadLeft(10, ' '), bson["pass"], 1, bson["tick"], bson["require"]));
                if (!bson["refs"].AsBsonArray.Contains(MethodBase.GetCurrentMethod().Name)) { bson["refs"].AsBsonArray.Add(MethodBase.GetCurrentMethod().Name); }
                bsonArray.Add(new BsonDocument() { { str, new BsonDocument() { { "time", DateTime.Now }, bson } } });
                mongo.Reflex(new BsonDocument(mongo_key, new BsonDocument() { { str, new BsonDocument() { { "time", DateTime.Now }, bson } } }));
            }
            else
            {
                Console.WriteLine();
            }
        }
        public void Ref4()
        {
            Tick(new BsonDocument() { { "name", "entropy" }, { "tick", 100 } });
        }
        void Tick(BsonDocument Bson)
        {
            BsonDocument bson = power[(string)Bson["name"]].AsBsonDocument;
            bson.Set("tick", (int)bson["tick"] + (int)Bson["tick"]);
        }
        MethodInfo ChooseRef(string Name)
        {
            if (Refs.Count() == 0) { return null; }
            Random rnd = new Random();
            int index = rnd.Next(0, Refs.Count());
            string str_method = (string)Refs[index];
            BsonDocument bson = power[Name].AsBsonDocument;
            if (bson["refs"].IsBsonArray && bson["refs"].AsBsonArray.Count() > 0) { str_method = (string)bson["refs"][0]; }
            return GetType().GetMethod(str_method);
        }
        public void Save()
        {
            string dir = "./log/";
            DirectoryInfo root = new DirectoryInfo(dir);
            FileInfo[] files = root.GetFiles();
            int[] arr = new int[files.Count()];
            for (int i = 0; i < files.Count(); i++)
            {
                try { arr[i] = int.Parse(files[i].Name.Split('.')[0]); }
                catch { }
            }
            string file_name;
            if (arr.Length == 0 || arr.Max().ToString().IndexOf(DateTime.Now.ToString("Mdd")) != 0) { file_name = DateTime.Now.ToString("Mdd") + "01"; }
            else { file_name = (arr.Max() + 1).ToString(); }
            if (Directory.Exists(dir) == false) { Directory.CreateDirectory(dir); }
            StreamWriter sw = new StreamWriter(dir + file_name + ".txt");
            sw.Write(power);
            sw.Close();
        }
    }
    class Habit//false
    {
        int len = 2;
        BsonArray all_bson = new BsonArray();
        BsonArray last_len_bson
        {
            get
            {
                if (all_bson != null && all_bson.Count >= len)
                {
                    BsonArray bson = new BsonArray();
                    for (int i = 0; i < len; i++) { bson.Add(all_bson[all_bson.Count - 1 - i]); }
                    return bson;
                }
                else { return null; }
            }
        }
        BsonArray current_bson = new BsonArray();
        public Habit(BsonDocument bson)
        {
            all_bson.Add(bson);
        }
    }
}
