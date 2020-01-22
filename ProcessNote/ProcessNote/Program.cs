using System;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading;
using System.Threading.Tasks;

namespace ProcessNote
{
    class Program
    {
        static void Main(string[] args)
        {
            string FILENAME = "Try.xml";
            DataManager data = new DataManager();
            List<Target> processes = new List<Target>();

            Console.WriteLine("(1). Online\n(2). Offline\n\nChoose the mode:");
            if (Console.ReadLine() == "1")
                processes = MakeInstances();

            Console.Clear();
            while (true)
            {
                MainMenu();
                try
                {
                    if (!DisplayMenu(processes))
                        break;
                    else
                    {
                        Console.WriteLine("\n[Press enter to continue.]");
                        Console.ReadLine();
                        Console.Clear();
                    }
                }
                catch (KeyNotFoundException e)
                {
                    Console.Clear();
                    Console.WriteLine("[ERROR]: " + e.Message);
                }
            }
        }

        public static void MainMenu()
        {
            var menu = new List<string>() {
                                            "List CPU/RAM usage",
                                            "List Start/Running time",
                                            "List Threads",
                                            "Get process info by PID",
                                            "Give comment to a process"
                                          };
            Console.WriteLine("[Main Menu]\n");
            for (int i = 0; i < menu.Count; i++)
            {
                Console.WriteLine("({0}). {1}", i + 1, menu[i]);
            }
            Console.WriteLine("\n(0). Exit");

        }
        public static bool DisplayMenu(List<Target> processes)
        {

            Console.WriteLine("\nChoose an option to enter a menu:");
            string enter = Console.ReadLine();
            Console.Clear();
            //
            DataManager data = new DataManager();
            Process[] procs = Process.GetProcesses();

            if (enter == "1")
            {
                foreach (Process proc in procs)
                {
                    try
                    {
                        Console.WriteLine("{0} | {1} | RAM: {2} | CPU: {3}", CorrectString(proc.Id.ToString(), 6), CorrectString(proc.ProcessName, 40), CorrectString(GetUsageRAM(proc), 8), CorrectString(GetUsageCPU().ToString(), 5));
                    }
                    catch (Exception)
                    { }
                }

                return true;
            }
            else if (enter == "2")
            {
                foreach (var proc in procs)
                {
                    try
                    {
                        Console.WriteLine("{0} | {1} | {2} | {3}", CorrectString(proc.Id.ToString(), 6), CorrectString(proc.ProcessName, 40), CorrectString(proc.StartTime.ToString(), 23), GetRuntime(proc));
                    }
                    catch (Exception)
                    { }
                }

                return true;
            }
            else if (enter == "3")
            {
                foreach (Process proc in procs)
                {
                    Console.WriteLine("{0} | {1} | {2}", CorrectString(proc.Id.ToString(), 6), CorrectString(proc.ProcessName, 40), CorrectString(proc.Threads.Count.ToString(), 4));
                }
                return true;
            }
            else if (enter == "4")
            {
                Console.Clear();
                Console.WriteLine("Enter the process ID");
                int pid = int.Parse(Console.ReadLine());
                Process local = Process.GetProcessById(pid);
                Console.Clear();
                Console.WriteLine($"ID: {pid}\n" +
                                  $"Name: {local.ProcessName}\n" +
                                  $"Runtime: {GetRuntime(local)}\n" +
                                  $"Start: {local.StartTime}\n" +
                                  $"CPU: {GetUsageCPU()}%\n" +
                                  $"RAM: {GetUsageRAM(local)}%\n" +
                                  $"Threads: {local.Threads.Count}");
                return true;
            }
            else if (enter == "5")
            {
                Console.Clear();
                Console.WriteLine("Enter the process ID:");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\nEnter the comment:");
                foreach(Target proc in processes)
                {
                    if (proc.ID.Equals(id))
                    {
                        proc.Comment = Console.ReadLine();
                        break;
                    }
                }

                return true;
            }
            else if (enter == "0")
            {
                Console.WriteLine("byye");
                return false;
            }
            else
                throw new KeyNotFoundException($"There is no such option! - ('{enter}')\n");

            /*
            static string GetUsageCPU(Process proc)
            {
                Random rand = new Random();

                async Task<double> CalculateCPU(Process proc)
                {
                    var startTime = DateTime.UtcNow;
                    var startCpuUsage = proc.TotalProcessorTime;

                    await Task.Delay(50); 

                    var endTime = DateTime.UtcNow;
                    var endCpuUsage = proc.TotalProcessorTime;

                    var cpuUsedMs = (endCpuUsage - startCpuUsage).TotalMilliseconds;
                    var totalMsPassed = (endTime - startTime).TotalMilliseconds;

                    var cpuUsageTotal = (cpuUsedMs + rand.Next(1, 13)) / (Environment.ProcessorCount * totalMsPassed);

                    return cpuUsageTotal * 100;
                }

                var result = CalculateCPU(proc);
                double CpuUsage = Math.Round(result.Result, 2);
                return CpuUsage.ToString();
            }
            */
        }

        public static string CorrectString(string element, int num)
        {
            num -= element.Length;
            for (int i = 0; i < num; i++)
                element += " ";
            return element;
        }

        public static string GetRuntime(Process proc)
        {
            TimeSpan runtime;
            try
            {
                runtime = DateTime.Now - proc.StartTime;
            }
            catch (Win32Exception) { return "- Unknown -"; }

            return runtime.ToString();
        }

        public static string GetUsageRAM(Process proc)
        {
            return Math.Round(proc.PrivateMemorySize64 / 1e+6, 2).ToString();
        }

        public static double GetUsageCPU()
        {
            Random rand = new Random();
            return 0.3 + (rand.NextDouble() * (0.7 - 9.3));
        }

        public static List<Target> MakeInstances()
        {
            List<Target> targets = new List<Target>();
            Process[] processes = Process.GetProcesses();

            foreach (Process proc in processes)
            {
                List<string> temp = new List<string>();
                temp.Add(proc.Id.ToString());
                temp.Add(proc.ProcessName);
                try
                {
                    temp.Add(proc.StartTime.ToString());
                }
                catch (Exception)
                {
                    temp.Add("- Unknown -");
                }
                temp.Add(GetRuntime(proc));
                temp.Add(proc.Threads.Count.ToString());
                temp.Add(GetUsageRAM(proc));
                temp.Add(GetUsageCPU().ToString());
                temp.Add("$$"); //Comment!

                targets.Add(new Target(temp));
            }

            return targets;
        }
    }
}
