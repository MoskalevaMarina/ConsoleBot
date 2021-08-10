// <copyright file="IChatCommand.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
namespace ConsoleBot
{
    /// <summary>
    /// интерфейс для определения команды чата
    /// </summary>
    public interface IChatCommand
    {
        /// <summary>
        /// Метод получения сообщения
        /// </summary>
        /// <param name="message">Ввод текста</param>
        /// <returns> возвращает правда или ложь</returns>
        bool CheckMessage(string message);
    }
}