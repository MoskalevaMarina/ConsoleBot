// <copyright file="ChatTextCommandOption.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBot.Commands
{
    /// <summary>
    /// абстрактный класс для текстовых команд с дополнением
    /// </summary>
    public abstract class ChatTextCommandOption : AbstractCommand
    {
        /// <summary>
        /// Метод проверки соответствия сообщения тексту команды
        /// </summary>
        /// <param name="message">Ввод текста</param> 
        /// <returns> возвращает правда или ложь </returns>
        public override bool CheckMessage(string message)
        {
            return message.StartsWith(CommandText);
        }

        /// <summary>
        /// Метод получения текста после команды
        /// </summary>
        /// <param name="message">Ввод текста</param> 
        /// <returns> возвращает текст, который идет после команды </returns>
#pragma warning disable IDE0057 // Использовать оператор диапазона
        public string ClearMessageFromCommand(string message)
        {
            if (message.Length == CommandText.Length)
            {
                return string.Empty;
            }
            else
            {
                return message.Substring(CommandText.Length + 1);
            }
        }
#pragma warning restore IDE0057 // Использовать оператор диапазона
    }
}
