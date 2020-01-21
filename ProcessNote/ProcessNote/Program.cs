using System;
using System.IO;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
namespace ProcessNote
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                MainMenu();
                try
                {
                    if (!DisplayMenu())
                        break;
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
                                            "List CPU usage", 
                                            "List  Memory usage", 
                                            "List Running time", 
                                            "List Start time", 
                                            "List Threads"
                                          };
            Console.WriteLine("[Main Menu]\n");
            for (int i = 0; i < menu.Count; i++)
            {
                Console.WriteLine("({0}). {1}", i+1, menu[i]);
            }
            Console.WriteLine("\n(0). Exit");

        }
        public static bool DisplayMenu()
        {

            Console.WriteLine("\nEnter a number to enter a menu:");
            string enter = Console.ReadLine();
            Console.Clear();

            if (enter == "1")
            {
                Console.WriteLine("cpu");
                return true;
            }
            else if (enter =="2")
            {
                Console.WriteLine("memory");
                return true;
            }
            else if (enter == "3")
            {
                Console.WriteLine("running time");
                return true;
            }
            else if (enter == "4")
            {
                Console.WriteLine("start");
                return true;
            }
            else if (enter == "5")
            {
                Console.WriteLine("thread");
                return true;
            }
            else if (enter == "0")
            {
                Console.WriteLine("byye");
                return false;
            }
            else
            {
                throw new KeyNotFoundException($"Invalid option! - ('{enter}')\n");
            }
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
    }
}
