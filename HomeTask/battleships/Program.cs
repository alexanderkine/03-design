using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using NLog;

namespace battleships
{
	public class Program
	{
		private static void Main(string[] args)
		{
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;          
			if (args.Length == 0)
			{
				Console.WriteLine("Usage: {0} <ai.exe>", Process.GetCurrentProcess().ProcessName);
				return;
			}
			var aiPath = args[0];
			var settings = new Settings("settings.txt");
			var aiTester = new AiTester(settings);
            var resultsLog = LogManager.GetLogger("results");
            aiTester.WriteLogInfo += resultsLog.Info;
			if (File.Exists(aiPath))
				aiTester.TestSingleFile(aiPath);
			else
				Console.WriteLine("No AI exe-file " + aiPath);
		}
	}
}