// <copyright file="BotMessageLogic.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace ConsoleBot
{
    /// <summary>
    /// класс для управления логикой оброботки сообщений
    /// </summary>
    public class BotMessageLogic
    {
        /// <summary>
        /// поле для хранения объекта Messenger
        /// </summary>
        private readonly Messenger messanger;

        /// <summary>
        /// словарь для хранения чатов
        /// </summary>
        private readonly Dictionary<long, Conversation> chatList;

        /// <summary>
        /// Initializes a new instance of the <see cref="BotMessageLogic"/> class.
        /// </summary>
        /// <param name="botClient">Ввод бот-клиент</param>
        public BotMessageLogic(ITelegramBotClient botClient)
        {
            this.messanger = new Messenger(botClient);
            this.chatList = new Dictionary<long, Conversation>();
        }

        /// <summary>
        /// Метод отправки ответа при взаимодствии с ботом
        /// </summary>
        /// <param name="e">Ввод аргументов или событий или сообщений </param>
        /// <returns> возвращает Task </returns>
#pragma warning disable CS0618 // Тип или член устарел
        public async Task Response(MessageEventArgs e)
#pragma warning restore CS0618 // Тип или член устарел
        {
            var id = e.Message.Chat.Id;

            if (!this.chatList.ContainsKey(id))
            {
                var newchat = new Conversation(e.Message.Chat);

                this.chatList.Add(id, newchat);
            }

            var chat = this.chatList[id];

            chat.AddMessage(e.Message);

            await this.SendMessage(chat);
        }

        /// <summary>
        /// Метод отправки ответа для чата
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        /// <returns> возвращает Task </returns>
        private async Task SendMessage(Conversation chat)
        {
            await this.messanger.MakeAnswer(chat);
        }
    }
}
