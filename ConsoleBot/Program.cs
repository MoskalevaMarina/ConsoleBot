// <copyright file="Program.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace ConsoleBot
{
    /// <summary>
    /// Класс Program
    /// </summary>
    public class Program
    {
        /// <summary>
        /// метод Main
        /// </summary>
        public static void Main()
        {
            var bot = new BotWorker();

            bot.Inizalize();
            bot.Start();

            Console.WriteLine("Напишите stop для прекращения работы");

            string command;
            do
            {
                command = Console.ReadLine();
            } 
            while (command != "stop");

            bot.Stop();
        }
    }
}
