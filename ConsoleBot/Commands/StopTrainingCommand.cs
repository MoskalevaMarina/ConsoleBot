// <copyright file="StopTrainingCommand.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBot.Commands
{
    /// <summary>
    /// класс для команды остановки тренировки
    /// </summary>
    public class StopTrainingCommand : AbstractCommand, IChatTextCommandWithAction
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StopTrainingCommand"/> class.
        /// </summary>
        public StopTrainingCommand()
        {
            CommandText = "/stop";
        }

        /// <summary>
        /// Метод действия для команды
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        /// <returns> возвращает успешно или нет</returns>
        public bool DoAction(Conversation chat)
        {
            chat.IsTraningInProcess = false;
            return !chat.IsTraningInProcess;
        }

        /// <summary>
        /// Метод вывода необходимого сообщения
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        /// <returns> возвращает текст</returns>
        public string ReturnText(Conversation chat)
        {
            return "Тренировка остановлена!";
        }
    }
}
