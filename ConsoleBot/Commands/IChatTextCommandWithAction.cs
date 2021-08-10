// <copyright file="IChatTextCommandWithAction.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>

namespace ConsoleBot.Commands
{
    /// <summary>
    /// интерфейс для определения текстовой команды с действием 
    /// </summary>
    public interface IChatTextCommandWithAction : IChatTextCommand
    {
        /// <summary>
        /// метод для действия
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        /// <returns> возвращает успешно ли прошло действие или нет</returns>
        bool DoAction(Conversation chat);
    }
}
