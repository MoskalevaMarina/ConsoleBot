// <copyright file="AddingState.cs" company="My Company Marina">
// Copyright (c) 2021 All Rights Reserved
// </copyright>
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleBot
{
    /// <summary>
    /// Состояния при добавлении слов
    /// </summary>
    public enum AddingState
    {
        /// <summary>
        /// получение русского значения слова
        /// </summary>
        Russian,

        /// <summary>
        /// получение английского значения слова
        /// </summary>
        English,

        /// <summary>
        /// получение темы
        /// </summary>
        Theme,

        /// <summary>
        /// ввод слова завершен
        /// </summary>
        Finish
    }
}
