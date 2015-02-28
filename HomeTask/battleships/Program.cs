﻿using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Threading;
using Ninject;

namespace battleships
{
	public class Program
	{
		private static void Main(string[] args)
		{
		    var container = new StandardKernel(new AiTesterContainer());
			Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
			if (args.Length == 0)
			{
				Console.WriteLine("Usage: {0} <ai.exe>", Process.GetCurrentProcess().ProcessName);
				return;
			}
			var aiPath = args[0];
			var settings = new Settings("settings.txt");
            var tester = new AiTester(settings, new MapGenerator(settings, new Random(settings.RandomSeed)),new GameVisualizer());
			if (File.Exists(aiPath))
				tester.TestSingleFile(aiPath, new ProcessMonitor(TimeSpan.FromSeconds(settings.TimeLimitSeconds * settings.GamesCount), settings.MemoryLimit));
			else
				Console.WriteLine("No AI exe-file " + aiPath);

		}
	}
}