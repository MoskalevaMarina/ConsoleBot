﻿// <copyright file="TrainingCommand.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types.ReplyMarkups;

namespace ConsoleBot.Commands
{
    /// <summary>
    /// класс для команды тренировки
    /// </summary>
    public class TrainingCommand : AbstractCommand, IKeyBoardCommand
    {
        /// <summary>
        /// поле для указания клента для работы с ботом
        /// </summary>
        private readonly ITelegramBotClient botClient;

        /// <summary>
        /// словарь для хранения типа тренировки
        /// </summary>
        private readonly Dictionary<long, TrainingType> training;

        /// <summary>
        /// словарь для хранения тренировки для чата
        /// </summary>
        private readonly Dictionary<long, Conversation> trainingChats;

        /// <summary>
        /// словарь для хранения активного слова
        /// </summary>
        private readonly Dictionary<long, string> activeWord;

        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingCommand"/> class.
        /// </summary>
        /// <param name="botClient">Ввод бот-клиент</param>
        public TrainingCommand(ITelegramBotClient botClient)
        {
            CommandText = "/training";

            this.botClient = botClient;

            training = new Dictionary<long, TrainingType>();
            trainingChats = new Dictionary<long, Conversation>();
            activeWord = new Dictionary<long, string>();
        }

        /// <summary>
        /// метод для определения клавиатуры
        /// </summary>
        /// <returns> возвращает клавиатуру</returns>
        public InlineKeyboardMarkup ReturnKeyBoard()
        {
            var buttonList = new List<InlineKeyboardButton>
            {
                new InlineKeyboardButton
                {
                    Text = "С русского на английский",
                    CallbackData = "rustoeng"
                },

                new InlineKeyboardButton
                {
                    Text = "С английского на русский",
                    CallbackData = "engtorus"
                }
            };

            var keyboard = new InlineKeyboardMarkup(buttonList);

            return keyboard;
        }

        /// <summary>
        /// метод для определения информационного сообщения
        /// </summary>
        /// <returns> возвращает информацию</returns>
        public string InformationalMessage()
        {
            return "Выберите тип тренировки. Для окончания тренировки введите команду /stop";
        }

        /// <summary>
        /// метод для добавления события
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        public void AddCallBack(Conversation chat)
        {
            trainingChats.Add(chat.GetId(), chat);

#pragma warning disable CS0618 // Тип или член устарел
            this.botClient.OnCallbackQuery -= Bot_Callback;
#pragma warning restore CS0618 // Тип или член устарел
#pragma warning disable CS0618 // Тип или член устарел
            this.botClient.OnCallbackQuery += Bot_Callback;
#pragma warning restore CS0618 // Тип или член устарел
        }

        /// <summary>
        /// Метод перехода к следующему шагу
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        /// <param name="message">Ввод текста</param> 
        public async void NextStepAsync(Conversation chat, string message)
        {
            var type = training[chat.GetId()];
            var word = activeWord[chat.GetId()];

            var check = chat.CheckWord(type, word, message);

            string text; // = string.Empty;

            if (check)
            {
                text = "Правильно!";
            }
            else
            {
                text = "Неправильно!";
            }

            text += " Следующее слово: ";

            var newword = chat.GetTrainingWord(type);

            text += newword;

            activeWord[chat.GetId()] = newword;

            await botClient.SendTextMessageAsync(chatId: chat.GetId(), text: text);
        }

#pragma warning disable CS0618 // Тип или член устарел
        /// <summary>
        /// Метод для реаизации события при нажатии на кнопку
        /// </summary>
        /// <param name="sender">Ввод параметров</param> 
        /// <param name="e">Ввод аргументов</param>
        private async void Bot_Callback(object sender, CallbackQueryEventArgs e)
#pragma warning restore CS0618 // Тип или член устарел
        {
            var text = string.Empty;

            var id = e.CallbackQuery.Message.Chat.Id;

            var chat = trainingChats[id];

            switch (e.CallbackQuery.Data)
            {
                case "rustoeng":
                    training.Add(id, TrainingType.RusToEng);

                    text = chat.GetTrainingWord(TrainingType.RusToEng);

                    break;
                case "engtorus":
                    training.Add(id, TrainingType.EngToRus);

                    text = chat.GetTrainingWord(TrainingType.EngToRus);
                    break;
                default:
                    break;
            }

            chat.IsTraningInProcess = true;
            activeWord.Add(id, text);

            if (trainingChats.ContainsKey(id))
            {
                trainingChats.Remove(id);
            }

            await botClient.SendTextMessageAsync(id, text);
            await botClient.AnswerCallbackQueryAsync(e.CallbackQuery.Id);
        }
    }
}
