using Microsoft.Extensions.Configuration;

namespace Power
{
    class Config
    {
        //public static readonly string strCon = "mongodb://127.0.0.1:27017/Power";
        //public static readonly string strDB = "Power";

        //public static readonly string strCon = "mongodb://cs3_312:iuemvibngksi@localhost:27017/ConsoleApplication3DB";
        //public static readonly string strDB = "ConsoleApplication3DB";

        //从配置文件加载配置信息
        static IConfigurationBuilder builder = new ConfigurationBuilder()
                            .SetBasePath(System.IO.Directory.GetCurrentDirectory())
                            .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        //从配置文件源构造键值对的配置信息，得到IConfigurationRoot（继承自IConfiguration）的一个实例
        static IConfigurationRoot configRoot = builder.Build();

        // 调用IConfiguration的索引器或者GetSection(string key)即可拿到相应的配置信息
        public static readonly string strCon = configRoot.GetSection("DBConfig").GetSection("MongoDBConnStr").Value;
        public static readonly string strDB = configRoot.GetSection("DBConfig").GetSection("MongoDB").Value;
        public static readonly string strUser = configRoot.GetSection("DBConfig").GetSection("strUser").Value;
        public static readonly string strPwd = configRoot.GetSection("DBConfig").GetSection("strPwd").Value;
        public static readonly string strIP = configRoot.GetSection("DBConfig").GetSection("strIP").Value;
        public static readonly string dbName = configRoot.GetSection("DBConfig").GetSection("dbName").Value;
    }
}
