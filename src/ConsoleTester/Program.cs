using System;
using System.Linq;
using ServiceStack.Redis;
using System.Collections.Generic;
using System.Diagnostics;

namespace ConsoleTester
{
	class MainClass
	{
		public static void Main (string[] args)
		{
			var redisClient = new RedisClient ("pv-devmbl1.bstsoftware.net", 6379);

//			foreach (var client in redisClient.GetClientList ())
//			{
//				foreach (var c1 in client.Keys)
//				{
//					Console.WriteLine (c1);
//					Console.WriteLine (client [c1]);
//				}
//			}

			var list = new List<int> ();

			for (int n = 0; n < int.MaxValue / 2000; n++)
			{
				list.Add (n);
			}

			var stopWatch = new Stopwatch ();
//			stopWatch.Start ();
//			redisClient.Add ("list", list);
//			stopWatch.Stop ();
//
//			Console.WriteLine ("{0} integers added to Redis in {1}ms.", list.Count, stopWatch.ElapsedMilliseconds);

			stopWatch.Restart ();
			var newList = redisClient.Get<List<int>>("list");
			stopWatch.Stop ();

			Console.WriteLine ("{0} integers read from Redis in {1}ms.", newList.Count, stopWatch.ElapsedMilliseconds);

			if (!newList.SequenceEqual (list))
			{
				Console.WriteLine ("Return value is invalid.");
			}

//			for (int n = 0; n < 2000; n++)
//			{
//				list.Add (n);
//				redisClient.AddItemToList ("newL2", n.ToString ());
//			}

			stopWatch.Restart ();
			var newList2 = redisClient.GetAllItemsFromList ("newL2");
			stopWatch.Stop ();
			Console.WriteLine ("{0} items read from Redis list in {1}ms.", newList2.Count, stopWatch.ElapsedMilliseconds);


			Console.ReadLine ();
		}
	}
}
