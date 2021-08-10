// <copyright file="CommandParser.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;
using ConsoleBot.Commands;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleBot
{
    /// <summary>
    /// Класс парсер команд
    /// </summary>
    public class CommandParser
    {
        /// <summary>
        /// список команд
        /// </summary>
        private readonly List<IChatCommand> command;

        /// <summary>
        /// контроллер для отслеживания процесса добавления слов
        /// </summary>
        private readonly AddingController addingController;

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandParser"/> class.
        /// </summary>
        public CommandParser()
        {
            this.command = new List<IChatCommand>();
            this.addingController = new AddingController();
        }

        /// <summary>
        /// Метод добавления команд
        /// </summary>
        /// <param name="chatCommand">Ввод чата </param>
        public void AddCommand(IChatCommand chatCommand)
        {
            this.command.Add(chatCommand);
        }

        /// <summary>
        /// Метод определения введена команда или обычный текст
        /// </summary>
        /// <param name="message">Ввод текста </param>
        /// <returns> возвращает правда или ложь </returns>
        public bool IsMessageCommand(string message)
        {
            return this.command.Exists(x => x.CheckMessage(message));
        }

        /// <summary>
        /// Метод определения является ли команда текстовой
        /// </summary>
        /// <param name="message">Ввод текста </param>
        /// <returns> возвращает правда или ложь </returns>
        public bool IsTextCommand(string message)
        {
            var command = this.command.Find(x => x.CheckMessage(message));

            return command is IChatTextCommand;
        }

        /// <summary>
        /// Метод определения являестя ли команда кнопочной
        /// </summary>
        /// <param name="message">Ввод текста </param>
        /// <returns> возвращает правда или ложь </returns>
        public bool IsButtonCommand(string message)
        {
            var command = this.command.Find(x => x.CheckMessage(message));

            return command is IKeyBoardCommand;
        }

        /// <summary>
        /// Метод получения ответа
        /// </summary>
        /// <param name="message">Ввод текста </param>
        /// <param name="chat">Ввод чата </param>
        /// <returns> возвращает текст </returns>
        public string GetMessageText(string message, Conversation chat)
        {
            var command = this.command.Find(x => x.CheckMessage(message)) as IChatTextCommand;

            if (command is IChatTextCommandWithAction)
            {
                if (!(command as IChatTextCommandWithAction).DoAction(chat))
                {
                    return "Ошибка выполнения команды ";
                }
            }

            return command.ReturnText(chat);
        }

        /// <summary>
        /// Метод получения информационного сообщения
        /// </summary>
        /// <param name="message">Ввод текста </param>
        /// <returns> возвращает текст </returns>
        public string GetInformationalMeggase(string message)
        {
            var command = this.command.Find(x => x.CheckMessage(message)) as IKeyBoardCommand;

            return command.InformationalMessage();
        }

        /// <summary>
        /// Метод вывода клавиатуры
        /// </summary>
        /// <param name="message">Ввод текста </param>
        /// <returns> возвращает клавиатуру </returns>
        public InlineKeyboardMarkup GetKeyBoard(string message)
        {
            var command = this.command.Find(x => x.CheckMessage(message)) as IKeyBoardCommand;

            return command.ReturnKeyBoard();
        }

        /// <summary>
        /// Метод добавления события
        /// </summary>
        /// <param name="message">Ввод текста </param>
        /// <param name="chat">Ввод чата </param>
        public void AddCallback(string message, Conversation chat)
        {
            var command = this.command.Find(x => x.CheckMessage(message)) as IKeyBoardCommand;
            command.AddCallBack(chat);
        }

        /// <summary>
        /// Метод определения является ли команда командой добавления
        /// </summary>
        /// <param name="message">Ввод текста </param>
        /// <returns> возвращает правда или ложь </returns>
        public bool IsAddingCommand(string message)
        {
            var command = this.command.Find(x => x.CheckMessage(message));

            return command is AddWordCommand;
        }

        /// <summary>
        /// Метод запуска начала добавления слова
        /// </summary>
        /// <param name="message">Ввод текста </param>
        /// <param name="chat">Ввод чата </param>
        public void StartAddingWord(string message, Conversation chat)
        {
            var command = this.command.Find(x => x.CheckMessage(message)) as AddWordCommand;

            this.addingController.AddFirstState(chat);
            command.StartProcessAsync(chat);
        }

        /// <summary>
        /// Метод перехода к следующей стадии при добавлении слова
        /// </summary>
        /// <param name="message">Ввод текста </param>
        /// <param name="chat">Ввод чата </param>
        public void NextStage(string message, Conversation chat)
        {
            var command = this.command.Find(x => x is AddWordCommand) as AddWordCommand;

            command.DoForStageAsync(this.addingController.GetStage(chat), chat, message);

            this.addingController.NextStage(chat);
        }

        /// <summary>
        /// Метод продолжения тренировки
        /// </summary>
        /// <param name="message">Ввод текста </param>
        /// <param name="chat">Ввод чата </param>
        public void ContinueTraining(string message, Conversation chat)
        {
            var command = this.command.Find(x => x is TrainingCommand) as TrainingCommand;

            command.NextStepAsync(chat, message);
        }
    }
}
