// <copyright file="DeleteWordCommand.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBot.Commands
{
    /// <summary>
    /// класс для определения команды удаления
    /// </summary>
    public class DeleteWordCommand : ChatTextCommandOption, IChatTextCommandWithAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DeleteWordCommand"/> class.
        /// </summary>
        public DeleteWordCommand()
        {
            CommandText = "/deleteword";
        }

        /// <summary>
        /// Метод вывода необходимого сообщения
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        /// <returns> возвращает текст</returns>
        public string ReturnText(Conversation chat)
        {
            return "Слово удалено";
        }

        /// <summary>
        /// Метод удаления слова
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        /// <returns> возвращает успешно или нет</returns>
        public bool DoAction(Conversation chat)
        {
            var message = chat.GetLastMessage();
            var text = ClearMessageFromCommand(message);
            if (chat.Dictionary.ContainsKey(text))
            {
                chat.Dictionary.Remove(text);
                return true;
            }

            return false;
        }
    }
}
