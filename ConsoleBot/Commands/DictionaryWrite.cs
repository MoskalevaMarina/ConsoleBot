// <copyright file="DictionaryWrite.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBot
{
    /// <summary>
    /// класс для определения команды вывода всех слов из словаря
    /// </summary>
    public class DictionaryWrite : AbstractCommand, IChatTextCommand
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DictionaryWrite"/> class.
        /// </summary>
        public DictionaryWrite()
        {
            CommandText = "/dictionary";
        }

        /// <summary>
        /// Метод вывода необходимого сообщения
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        /// <returns> возвращает текст</returns>
        public string ReturnText(Conversation chat)
        {
            string st = string.Empty;
            if (chat.Dictionary.Count != 0)
            {
                foreach (var item in chat.Dictionary)
                { 
                    st = st + item.Value.English.ToString() + " - " + item.Value.Russian.ToString() + ", " + item.Value.Theme.ToString() + " \n"; 
                }
                
                return st;
            }

            return "Нет слов в словаре";
        }
    }
}
