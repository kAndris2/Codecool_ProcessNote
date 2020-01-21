using System;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;

namespace ProcessNote
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Task> processes = new List<Task>();
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
                                            "List Threads"
                                          };
            Console.WriteLine("[Main Menu]\n");
            for (int i = 0; i < menu.Count; i++)
            {
                Console.WriteLine("({0}). {1}", i+1, menu[i]);
            }
            Console.WriteLine("\n(0). Exit");

        }
        public static bool DisplayMenu(List<Task> processes)
        {

            Console.WriteLine("\nEnter a number to enter a menu:");
            string enter = Console.ReadLine();
            Console.Clear();
            //
            DataManager data = new DataManager();
            var procs = Process.GetProcesses();

            if (enter == "1")
            {
                foreach (Process proc in procs)
                {
                    var memory = Math.Round(proc.PrivateMemorySize64 / 1e+6, 2);
                    Console.WriteLine("{0} | {1} | RAM: {2}", CorrectString(proc.Id.ToString(), 6), CorrectString(proc.ProcessName, 40), CorrectString(memory.ToString(), 5));
                }

                return true;
            }
            else if (enter == "2")
            {
                foreach (var proc in procs)
                {
                    TimeSpan runtime;
                    try
                    {
                        runtime = DateTime.Now - proc.StartTime;
                    }
                    catch (Win32Exception ex)
                    {
                        if (ex.NativeErrorCode == 5)
                            continue;
                        throw;
                    }

                    Console.WriteLine("{0} | {1} | {2} | {3}", CorrectString(proc.Id.ToString() ,6), CorrectString(proc.ProcessName, 40), CorrectString(proc.StartTime.ToString(), 23), runtime);
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
            else if (enter == "0")
            {
                Console.WriteLine("byye");
                return false;
            }
            else
                throw new KeyNotFoundException($"Invalid option! - ('{enter}')\n");
        }

        public static void TaskManager()
        {
            Process[] processes = Process.GetProcesses();
            foreach (Process item in processes)
            {
                if (item.Threads.Count > 1)
                {
                    Console.WriteLine("> " + PrintProcess(item));
                    Process[] localByName = Process.GetProcessesByName(item.ProcessName);
                    foreach (Process proc in localByName)
                    {
                        Console.WriteLine("\t" + PrintProcess(proc));
                    }
                }
                else
                    Console.WriteLine(PrintProcess(item));
            }
        }

        public static string PrintProcess(Process proc)
        {
            var memory = Math.Round(proc.PrivateMemorySize64 / 1e+6, 2);
            return $"{proc.Id} - {proc.ProcessName} - RAM:{memory}%";
        }

        public static string CorrectString(string element, int num)
        {
            num = num - element.Length;
            for (int i = 0; i < num; i++)
                element += " ";
            return element;
        }
    }
}
