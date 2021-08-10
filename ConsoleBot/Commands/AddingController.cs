// <copyright file="AddingController.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBot.Commands
{
    /// <summary>
    /// Контроллер при добавлении
    /// </summary>
    public class AddingController
    {
        /// <summary>
        /// Словарь хранит ID чата и состояние 
        /// </summary>
        private readonly Dictionary<long, AddingState> chatAdding;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddingController"/> class.
        /// </summary>
        public AddingController()
        {
            this.chatAdding = new Dictionary<long, AddingState>();
        }

        /// <summary>
        /// Метод установки первого состояния
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        public void AddFirstState(Conversation chat)
        {
            this.chatAdding.Add(chat.GetId(), AddingState.Russian);
        }

        /// <summary>
        /// Метод перехода к следующему состоянию
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        public void NextStage(Conversation chat)
        {
            var currentstate = this.chatAdding[chat.GetId()];
            this.chatAdding[chat.GetId()] = currentstate + 1;
            if (this.chatAdding[chat.GetId()] == AddingState.Finish)
            {
                chat.IsAddingInProcess = false;
                this.chatAdding.Remove(chat.GetId());
            }
        }

        /// <summary>
        /// Метод получения состояния чата
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        /// <returns> возвращает состояние чата</returns>
        public AddingState GetStage(Conversation chat)
        {
            return this.chatAdding[chat.GetId()];
        }
    }
}
