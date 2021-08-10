// <copyright file="IChatTextCommand.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
namespace ConsoleBot
{
    /// <summary>
    /// интерфейс для определения текстовой команды чата
    /// </summary>
    public interface IChatTextCommand
    {
        /// <summary>
        /// возвращение ответа в виде текста
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        /// <returns> возвращает текст</returns>
        string ReturnText(Conversation chat);
    }
}