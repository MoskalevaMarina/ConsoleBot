// <copyright file="AddWordCommand.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Telegram.Bot;

namespace ConsoleBot.Commands
{
    /// <summary>
    /// Команда добавления слова
    /// </summary>
    public class AddWordCommand : AbstractCommand
    {
        /// <summary>
        /// поле для указания клента для работы с ботом
        /// </summary>
        private readonly ITelegramBotClient botClient;

        /// <summary>
        /// временное хранилище
        /// </summary>
        private readonly Dictionary<long, Word> buffer;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddWordCommand"/> class.
        /// </summary>
        /// <param name="botClient">Ввод бот-клиент</param>
        public AddWordCommand(ITelegramBotClient botClient)
        {
            this.CommandText = "/addword";

            this.botClient = botClient;

            this.buffer = new Dictionary<long, Word>();
        }

        /// <summary>
        /// Метод начало процесса
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        public async void StartProcessAsync(Conversation chat)
        {
            this.buffer.Add(chat.GetId(), new Word());

            var text = "Введите русское значение слова";

            await SendCommandText(text, chat.GetId());
        }

        /// <summary>
        /// Метод перехода к следующей стадии
        /// </summary>
        /// <param name="addingState">Ввод текущей стадии процесса</param> 
        /// <param name="chat">Ввод чата</param>
        /// <param name="message">Ввод текста</param>
        public async void DoForStageAsync(AddingState addingState, Conversation chat, string message)
        {
            var word = this.buffer[chat.GetId()];
            var text = string.Empty;

            switch (addingState)
            {
                case AddingState.Russian:
                    word.Russian = message;

                    text = "Введите английское значение слова";
                    break;

                case AddingState.English:
                    word.English = message;

                    text = "Введите тематику";
                    break;

                case AddingState.Theme:
                    word.Theme = message;

                    text = "Успешно! Слово " + word.English + " добавлено в словарь. ";

                    chat.Dictionary.Add(word.Russian, word);

                    buffer.Remove(chat.GetId());
                    break;
            }

            await SendCommandText(text, chat.GetId());
        }

        /// <summary>
        /// Метод отправки текста
        /// </summary>
        /// <param name="text">Ввод текста</param> 
        /// <param name="chat">Ввод чата</param>
        /// <returns> возвращает Task </returns>
        private async Task SendCommandText(string text, long chat)
        {
            await botClient.SendTextMessageAsync(chatId: chat, text: text);
        }
    }
}
