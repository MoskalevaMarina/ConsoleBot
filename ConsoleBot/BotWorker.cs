// <copyright file="BotWorker.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;

namespace ConsoleBot
{
    /// <summary>
    /// Класс для работы с ботом
    /// </summary>
    public class BotWorker
    {
        /// <summary>
        /// поле для указания клента для работы с ботом
        /// </summary>
        private ITelegramBotClient botClient;

        /// <summary>
        /// объект класса BotMessageLogic
        /// </summary>
        private BotMessageLogic logic;

        /// <summary>
        /// Метод создания клиента бота
        /// </summary>
        public void Inizalize()
        {
            this.botClient = new TelegramBotClient(BotCredentials.BotToken);
            this.logic = new BotMessageLogic(this.botClient);
        }

        /// <summary>
        /// Метод устанавливает событие на отправку сообщений и начинает ожидание этих сообщений
        /// </summary>
        public void Start()
        {
#pragma warning disable CS0618 // Тип или член устарел
            this.botClient.OnMessage += this.Bot_OnMessage;
#pragma warning restore CS0618 // Тип или член устарел
#pragma warning disable CS0618 // Тип или член устарел
            this.botClient.StartReceiving();
#pragma warning restore CS0618 // Тип или член устарел
        }

        /// <summary>
        /// Метод завершает процесс
        /// </summary>
        public void Stop()
        {
#pragma warning disable CS0618 // Тип или член устарел
            this.botClient.StopReceiving();
#pragma warning restore CS0618 // Тип или член устарел
        }

#pragma warning disable CS0618 // Тип или член устарел
        /// <summary>
        /// событие на получение сообщения
        /// </summary>
        /// <param name="sender">Ввод аргументов  </param>
        /// <param name="e">Ввод аргументов или событий или сообщений </param>
        private async void Bot_OnMessage(object sender, MessageEventArgs e)
#pragma warning restore CS0618 // Тип или член устарел
        {
            if (e.Message != null)
            {
                await this.logic.Response(e);
            }
        }
    }
}
