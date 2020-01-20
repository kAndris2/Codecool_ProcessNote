﻿using System;
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
            while(true)
            {
                MainMenu();
                if (!DisplayMenu())
                    break;
            }





        }
        public static void MainMenu()
        {
            int menuNumber = 1;            
            List<string> menu = new List<string>() { "List CPU usage", "List  Memory usage", "List Running time", "List Start time", "List Threads", "Exit" };
            Console.WriteLine("Main Menu");

            
            {
                foreach (string options in menu)
                {
                    Console.WriteLine("({0}) {1}", menuNumber, options);
                    menuNumber++;

                }
            }

        }
        public static bool DisplayMenu()
        {
            
            Console.Write("Enter a number to enter a menu: ");
            int enter = Convert.ToInt32(Console.ReadLine());
            Console.Clear();

            if (enter == 1)
            {
                Console.WriteLine("cpu");
                return true;
            }
            else if (enter == 2)
            {
                Console.WriteLine("memory");
                return true;
            }
            else if (enter == 3)
            {
                Console.WriteLine("running time");
                return true;
            }
            else if (enter == 4)
            {
                Console.WriteLine("start");
                return true;
            }
            else if (enter == 5)
            {
                Console.WriteLine("thread");
                return true;
            }
            else if (enter == 6)
            {
                Console.WriteLine("byye");
                return false;
            }
            else
            {
                throw new KeyNotFoundException("Wrong number");
            }
        }
    }
}
