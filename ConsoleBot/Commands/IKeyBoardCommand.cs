// <copyright file="IKeyBoardCommand.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleBot
{
    /// <summary>
    /// интерфейс для определения команды с клавиатурой 
    /// </summary>
   public interface IKeyBoardCommand
    {
        /// <summary>
        /// метод для определения клавиатуры
        /// </summary>
        /// <returns> возвращает клавиатуру</returns>
        InlineKeyboardMarkup ReturnKeyBoard();

        /// <summary>
        /// метод для добавления события
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        void AddCallBack(Conversation chat);

        /// <summary>
        /// метод для определения информационного сообщения
        /// </summary>
        /// <returns> возвращает информацию</returns>
        string InformationalMessage();
    }
}
