// <copyright file="AbstractCommand.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBot
{
    /// <summary>
    /// Абстрактная команда
    /// </summary>
    public abstract class AbstractCommand : IChatCommand
    {
        /// <summary>
        /// Gets or sets текст команды
        /// </summary>
        public string CommandText { get; set; }

        /// <summary>
        /// Проверка текста команды
        /// </summary>
        /// <param name="message">Ввод сообщения</param>
        /// <returns> правда или ложь</returns>
        public virtual bool CheckMessage(string message)
        {
            return this.CommandText == message;
        }
    }
}
