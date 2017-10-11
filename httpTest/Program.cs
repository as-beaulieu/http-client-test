using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Net.Http;

namespace httpTest
{
    class Program
    {
        private static HttpClient StaticClient = new HttpClient();
        private static int numConnections = 10;

        static void Main(string[] args)
        {
            var OldTime = UsingHttpClient();

            var NewTime = StaticHttpClient();

            List<object> times = new List<object>();

            times.Add(OldTime.TotalSeconds);
            times.Add(NewTime.TotalSeconds);

            TestResult(times);

            Console.ReadLine();
        }

        public static TimeSpan UsingHttpClient()
        {
            TimeSpan OldWayFinal = new TimeSpan();

            Stopwatch timer = new Stopwatch();
            timer.Start();

            Console.WriteLine("Starting Connections");
            for (int i = 0; i < numConnections; i++)
            {
                using (var client = new HttpClient())
                {
                    var result = client.GetAsync("http://google.com").Result;
                    Console.WriteLine(result.StatusCode);
                }
            }

            timer.Stop();
            OldWayFinal = timer.Elapsed;
            Console.WriteLine("Connections Done");
            Console.WriteLine("Time to process (OldWay): " + OldWayFinal);
            Console.ReadLine();

            return OldWayFinal;
        }

        public static TimeSpan StaticHttpClient()
        {
            TimeSpan NewWayFinal = new TimeSpan();

            Stopwatch timer = new Stopwatch();
            timer.Start();

            Console.WriteLine("Starting Connections");
            for (int i = 0; i < numConnections; i++)
            {
                    var result = StaticClient.GetAsync("http://google.com").Result;
                    Console.WriteLine(result.StatusCode);
            }

            timer.Stop();
            NewWayFinal = timer.Elapsed;
            Console.WriteLine("Connections Done");
            Console.WriteLine("Time to process (NewWay): " + NewWayFinal);
            Console.ReadLine();

            return NewWayFinal;
        }

        public static void TestResult(List<object> times)
        {
            
            foreach(var time in times)
            {
                Console.WriteLine("Time: " + time);
                
            }

        }
    }
}
