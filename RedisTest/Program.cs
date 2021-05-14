using SmartMedical.BLL;
using System;

namespace RedisTest
{
    class Program
    {
        static void Main(string[] args)
        {
            RedisHelper redis = new RedisHelper("127.0.0.1:6379");
            redis.SetValue("name","汤宇航");
            Console.WriteLine(redis.GetValue("name"));
            Console.ReadKey();
        }
    }
}
