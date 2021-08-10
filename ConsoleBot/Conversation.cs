// <copyright file="Conversation.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using System.Collections.Generic;
using System.Linq;
using Telegram.Bot.Types;

namespace ConsoleBot
{
    /// <summary>
    /// класс чат
    /// </summary>
    public class Conversation
    {
        /// <summary>
        /// поле для хранения чата
        /// </summary>
        private readonly Chat telegramChat;

        /// <summary>
        /// список сообщений
        /// </summary>
        private readonly List<Message> telegramMessages;

        /// <summary>
        /// Initializes a new instance of the <see cref="Conversation"/> class.
        /// </summary>
        /// <param name="chat">Ввод чата</param>
        public Conversation(Chat chat)
        {
            this.telegramChat = chat;
            this.telegramMessages = new List<Message>();
            this.Dictionary = new Dictionary<string, Word>();
        }

        /// <summary>
        /// Gets or sets a value indicating whether поле для хранения процесса добавления слова
        /// </summary>
        public bool IsAddingInProcess { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether поле для хранения процесса тренировки
        /// </summary>
        public bool IsTraningInProcess { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether словарь для вводимых слов
        /// </summary>
        public Dictionary<string, Word> Dictionary { get; set; }

        /// <summary>
        /// метод добавления сообщения
        /// </summary>
        /// <param name="message">Ввод объекта сообщений</param>
        public void AddMessage(Message message)
        {
            this.telegramMessages.Add(message);
        }

        /// <summary>
        /// метод добавления слова
        /// </summary>
        /// <param name="key">Ввод ключа для словаря </param>
        /// <param name="word">Ввод слова </param>
        public void AddWord(string key, Word word)
        {
            this.Dictionary.Add(key, word);
        }

        /// <summary>
        /// метод очистки истории чата
        /// </summary>
        public void ClearHistory()
        {
            this.telegramMessages.Clear();
        }

        /// <summary>
        /// метод получения списка сообщений
        /// </summary>
        /// <returns> возвращает список</returns>
        public List<string> GetTextMessages()
        {
            var textMessages = new List<string>();

            foreach (var message in this.telegramMessages)
            {
                if (message.Text != null)
                {
                    textMessages.Add(message.Text);
                }
            }

            return textMessages;
        }

        /// <summary>
        /// метод получения Id чата
        /// </summary>
        /// <returns> возвращает Id</returns>
        public long GetId() => this.telegramChat.Id;

        /// <summary>
        /// метод получения последнего сообщения
        /// </summary>
        /// <returns> возвращает текст сообщения</returns>
#pragma warning disable IDE0056 // Использовать оператор индекса
        public string GetLastMessage() => this.telegramMessages[this.telegramMessages.Count - 1].Text;
#pragma warning restore IDE0056 // Использовать оператор индекса

        /// <summary>
        /// метод получения слова для тренировки
        /// </summary>
        /// <param name="type">Ввод типа тренировки </param>
        /// <returns> возвращает текст</returns>
        public string GetTrainingWord(TrainingType type)
        {
            var rand = new Random();
            var item = rand.Next(0, this.Dictionary.Count);

            var randomword = this.Dictionary.Values.AsEnumerable().ElementAt(item);

            var text = string.Empty;

            switch (type)
            {
                case TrainingType.EngToRus:
                    text = randomword.English;
                    break;

                case TrainingType.RusToEng:
                    text = randomword.Russian;
                    break;
            }

            return text;
        }

        /// <summary>
        /// метод проверки слова 
        /// </summary>
        /// <param name="type">Ввод типа тренировки </param>
        /// <param name="word">Ввод слова </param>
        /// <param name="answer">Ввод ответа пользователя </param>
        /// <returns> возвращает правильный ли ответ пользователя или нет</returns>
        public bool CheckWord(TrainingType type, string word, string answer)
        {
            Word control;

            var result = false;

            switch (type)
            {
                case TrainingType.EngToRus:

                    control = this.Dictionary.Values.FirstOrDefault(x => x.English == word);

                    result = control.Russian == answer;

                    break;

                case TrainingType.RusToEng:
                    control = this.Dictionary[word];

                    result = control.English == answer;

                    break;
            }

            return result;
        }
    }
}
