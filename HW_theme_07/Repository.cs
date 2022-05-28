﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HW_theme_07;

    /// <summary>
    /// Структура по работе с данными
    /// </summary>
    struct Repository
    {
        private Employee[] _staff; // Основной массив для хранения данных

        private string path; // путь к файлу с данными
        
        int index; // текущий элемент для добавления в workers
        
        
        /// <summary>
        /// Констрктор
        /// </summary>
        /// <param name="Path">Путь к файлу с данными</param>
        public Repository(string Path)
        {
            this.path = Path; // Сохранение пути к файлу с данными
            this.index = 0; // текущая позиция для добавления сотрудника в workers
            this._staff = new Employee[1]; // инициализаия массива сотрудников.    | изначально предпологаем, что данных нет

            this.Load(); // Загрузка данных
        }

        /// <summary>
        /// Метод увеличения текущего хранилища
        /// </summary>
        /// <param name="Flag">Условие увеличения</param>
        private void Resize(bool Flag)
        {
            if (Flag)
            {
                Array.Resize(ref this._staff, this._staff.Length * 2);
            }
        }

        /// <summary>
        /// Индексатор
        /// </summary>
        /// <param name="Idx">Индекс элемента</param>
        public Employee this[int Idx]
        {
            get { return this._staff[Idx]; }
        }
        
        /// <summary>
        /// Метод добавления сотрудника в хранилище
        /// </summary>
        /// <param name="ConcreteWorker">Сотрудник</param>
        public void Add(Employee ConcreteWorker)
        {
            this.Resize(index >= this._staff.Length);
            this._staff[index] = ConcreteWorker;
            this.index++;
        }
        
        /// <summary>
        /// Метод загрузки данных
        /// </summary>
        private void Load()
        {
            using (StreamReader sr = new StreamReader(this.path))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    Add(new Employee(line));
                }
            }
        }
        
        /// <summary>
        /// Метод сохранения данных
        /// </summary>
        /// <param name="Path">Путь к файлу сохранения</param>
        public void Save(string Path)
        {
            // перезаписываем файл        
            File.WriteAllText(Path, String.Empty, Encoding.Unicode);
            // добавляем содержимое
            for (int i = 0; i < this.index; i++)
            {
                string saveLine = this._staff[i].GetSaveLine();
                File.AppendAllText(Path, $"{saveLine}\n", Encoding.Unicode);
            }
        }

        /// <summary>
        /// Сортировка по дате создания
        /// </summary>
        /// <param name="ReverseFlag">Флаг убывания</param>
        public void SortByCreateDate(bool ReverseFlag = false)
        {
            // срез содержательной части массива
            Range idxRange = new Range(0, index);
            var tempStaff = _staff[idxRange];
            
            // непосредственно сортировка
            if (ReverseFlag) Array.Reverse(tempStaff);
            else Array.Sort(tempStaff);
            
            // запись в хранилище
            tempStaff.CopyTo(this._staff, 0);
        } 
        
        /// <summary>
        /// Вывод данных в консоль
        /// </summary>
        public void PrintDbToConsole()
        {
            Console.WriteLine(Employee.GetTitleLine());
        
            for (int i = 0; i < index; i++)
            {
                Console.WriteLine(this._staff[i].GetPrintLine());
            }
        }

        /// <summary>
        /// Количество сотрудников в хранилище
        /// </summary>
        public int Count { get { return this.index; } }

    }
