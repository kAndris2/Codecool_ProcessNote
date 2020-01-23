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
            List<Target> processes = new List<Target>();

            //-Mode----------------------------------------------------------------------
            while (true)
            {
                ChooseMenu();
                try
                {
                    if (!Choose(processes))
                        break;
                }
                catch (KeyNotFoundException e)
                {
                    Console.Clear();
                    Console.WriteLine("[ERROR]: " + e.Message);
                }
                catch (FileNotFoundException e)
                {
                    Console.Clear();
                    Console.WriteLine("[ERROR]: " + e.Message);
                    Console.WriteLine("\tThe online mode started automatically.\n");
                    processes = MakeInstances();
                    break;
                }
            }

            //-MainMenu-----------------------------------------------------------------
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

        public static void ChooseMenu()
        {
            var options = new List<string> { "Online", "Offline" };
            for (int i = 0; i < options.Count; i++)
                Console.WriteLine($"({i + 1}). {options[i]}");
            Console.WriteLine("\n(0). Exit");
        }

        public static bool Choose(List<Target> processes)
        {
            string FILENAME = "Test.xml";
            Console.WriteLine("\n[Choose the mode]:");
            string input = Console.ReadLine();
            DataManager data = new DataManager();

            if (input == "1")
            {
                processes.AddRange(MakeInstances());
            }
            else if (input == "2")
            {
                if (File.Exists(FILENAME))
                    processes.AddRange(data.XmlReader(FILENAME));
                else
                    throw new FileNotFoundException($"File Not found! ('{FILENAME}')");
            }
            else if (input == "0")
            {
                Environment.Exit(-1);
                return true;
            }
            else
                throw new KeyNotFoundException($"There is no such option! ('{input}')\n");
            return false;
        }

        public static void MainMenu()
        {
            var menu = new List<string>() {
                                            "List CPU/RAM usage",
                                            "List Start/Running time",
                                            "List Threads",
                                            "Get process info by PID",
                                            "Give comment to a process",
                                            "Save" //Ezt csak online módban kell engedni!
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

            Console.WriteLine($"\nChoose an option to enter a menu:{processes.Count}");
            string enter = Console.ReadLine();
            string FILENAME = "Test.xml";
            Console.Clear();
            //
            DataManager data = new DataManager();

            if (enter == "1")
            {
                foreach (Target proc in processes)
                {
                    try
                    {
                        Console.WriteLine("{0} | {1} | RAM: {2} | CPU: {3}", CorrectString(proc.ID.ToString(), 6), CorrectString(proc.Name, 40), CorrectString(proc.RAM.ToString(), 8), CorrectString(proc.CPU.ToString(), 5));
                    }
                    catch (Exception)
                    { }
                }

                return true;
            }
            else if (enter == "2")
            {
                foreach (Target proc in processes)
                {
                    try
                    {
                        Console.WriteLine("{0} | {1} | {2} | {3}", CorrectString(proc.ID.ToString(), 6), CorrectString(proc.Name, 40), CorrectString(proc.Start, 23), proc.Runtime);
                    }
                    catch (Exception)
                    { }
                }

                return true;
            }
            else if (enter == "3")
            {
                foreach (Target proc in processes)
                {
                    Console.WriteLine("{0} | {1} | {2}", CorrectString(proc.ID.ToString(), 6), CorrectString(proc.Name, 40), CorrectString(proc.Threads.ToString(), 4));
                }
                return true;
            }
            else if (enter == "4")
            {
                Console.Clear();
                Console.WriteLine("Enter the process ID");
                int pid = int.Parse(Console.ReadLine());
                Console.Clear();
                foreach (Target proc in processes)
                {
                    if (pid.Equals(proc.ID))
                    {
                        Console.WriteLine($"ID: {proc.ID}\n" +
                                      $"Name: {proc.Name}\n" +
                                      $"Runtime: {proc.Runtime}\n" +
                                      $"Start: {proc.Start}\n" +
                                      $"CPU: {proc.CPU}%\n" +
                                      $"RAM: {proc.RAM}%\n" +
                                      $"Threads: {proc.Threads}\n" +
                                      $"Comment: {proc.Comment}");
                        break;
                    }
                }
                
                return true;
            }
            else if (enter == "5")
            {
                Console.Clear();
                Console.WriteLine("Enter the process ID:");
                int id = int.Parse(Console.ReadLine());
                Console.WriteLine("\nEnter the comment:");
                foreach (Target proc in processes)
                {
                    if (proc.ID.Equals(id))
                    {
                        proc.Comment = Console.ReadLine();
                        break;
                    }
                }

                return true;
            }
            else if (enter == "6")
            {
                Target[] change = new Target[processes.Count];
                for (int i = 0; i < processes.Count; i++)
                {
                    change[i] = processes[i];
                }
                data.XmlWriter(FILENAME, change);
                Console.WriteLine("All DATAS SAVED!!!");
                return true;
            }
            else if (enter == "0")
            {
                Environment.Exit(-1);
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
            catch (Exception) { return "- Unknown -"; }

            return runtime.ToString();
        }

        public static string GetUsageRAM(Process proc)
        {
            return Math.Round(proc.PrivateMemorySize64 / 1e+6, 2).ToString();
        }

        public static double GetUsageCPU()
        {
            Random rand = new Random();
            return Math.Round(0.3 + (rand.NextDouble() * (9.3 - 0.7)), 2);
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
