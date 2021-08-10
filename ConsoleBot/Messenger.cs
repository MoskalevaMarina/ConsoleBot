// <copyright file="Messenger.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ConsoleBot.Commands;
using Telegram.Bot;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleBot
{
    /// <summary>
    /// класс сообщений 
    /// </summary>
    public class Messenger
    {
        /// <summary>
        /// поле для указания клента для работы с ботом
        /// </summary>
        private readonly ITelegramBotClient botClient;

        /// <summary>
        /// поле для указания парсера команд
        /// </summary>
        private readonly CommandParser parser;

        /// <summary>
        /// Initializes a new instance of the <see cref="Messenger"/> class.
        /// </summary>
        /// <param name="botClient">Ввод бот-клиент</param>
        public Messenger(ITelegramBotClient botClient)
        {
            this.botClient = botClient;
            this.parser = new CommandParser();

            this.RegisterCommands();
        }

        /// <summary>
        /// Метод отправки ответа для чата
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        /// <returns> возвращает Task </returns>
        public async Task MakeAnswer(Conversation chat)
        {
            var lastmessage = chat.GetLastMessage();

            if (chat.IsTraningInProcess && !this.parser.IsTextCommand(lastmessage))
            {
                this.parser.ContinueTraining(lastmessage, chat);

                return;
            }

            if (chat.IsAddingInProcess)
            {
                this.parser.NextStage(lastmessage, chat);

                return;
            }

            if (this.parser.IsMessageCommand(lastmessage))
            {
                await this.ExecCommand(chat, lastmessage);
            }
            else
            {
                var text = this.CreateTextMessage();

                await this.SendText(chat, text);
            }
        }

        /// <summary>
        /// Метод определения команды для чата
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        /// <param name="command">Ввод команды</param>
        /// <returns> возвращает Task </returns>
        public async Task ExecCommand(Conversation chat, string command)
        {
            if (this.parser.IsTextCommand(command))
            {
                var text = this.parser.GetMessageText(command, chat);

                await this.SendText(chat, text);
            }

            if (this.parser.IsButtonCommand(command))
            {
                var keys = this.parser.GetKeyBoard(command);
                var text = this.parser.GetInformationalMeggase(command);
                this.parser.AddCallback(command, chat);

                await this.SendTextWithKeyBoard(chat, text, keys);
            }

            if (this.parser.IsAddingCommand(command))
            {
                chat.IsAddingInProcess = true;
                this.parser.StartAddingWord(command, chat);
            }
        }

        /// <summary>
        /// Метод для регистрации команд
        /// </summary>
        private void RegisterCommands()
        {
            this.parser.AddCommand(new DictionaryWrite());
            this.parser.AddCommand(new DeleteWordCommand());
            this.parser.AddCommand(new TrainingCommand(this.botClient));
            this.parser.AddCommand(new StopTrainingCommand());
            this.parser.AddCommand(new AddWordCommand(this.botClient));
        }

        /// <summary>
        /// метод для отправки сообщения о не существовании команды
        /// </summary>
        /// <returns> возвращает текст</returns>
        private string CreateTextMessage()
        {
            var text = "Not a command";

            return text;
        }

        /// <summary>
        /// Метод определения ответа в виде текста для чата
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        /// <param name="text">Ввод текста</param>
        /// <returns> возвращает Task </returns>
        private async Task SendText(Conversation chat, string text)
        {
            await this.botClient.SendTextMessageAsync(chatId: chat.GetId(), text: text);
        }

        /// <summary>
        /// Метод определения ответа в виде клавиатуры для чата 
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        /// <param name="text">Ввод текста</param>
        /// <param name="keyboard">Ввод клавиатуры</param>
        /// <returns> возвращает Task </returns>
        private async Task SendTextWithKeyBoard(Conversation chat, string text, InlineKeyboardMarkup keyboard)
        {
            await this.botClient.SendTextMessageAsync(chatId: chat.GetId(), text: text, replyMarkup: keyboard);
        }
    }
}
