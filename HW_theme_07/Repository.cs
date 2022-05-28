using System.Text;


namespace HW_theme_07;

    /// <summary>
    /// Структура по работе с данными
    /// </summary>
    struct Repository
    {
        private Employee[] _staff; // Основной массив для хранения данных

        private string path; // путь к файлу с данными
        
        int _index; // текущий элемент для добавления в workers
        
        
        /// <summary>
        /// Констрктор
        /// </summary>
        /// <param name="Path">Путь к файлу с данными</param>
        public Repository(string Path)
        {
            this.path = Path; // Сохранение пути к файлу с данными
            this._index = 0; // текущая позиция для добавления сотрудника в workers
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
            this.Resize(_index >= this._staff.Length);
            this._staff[_index] = ConcreteWorker;
            this._index++;
        }

        /// <summary>
        /// Возвращает индекс элемента по его ID
        /// </summary>
        /// <param name="ID"></param>
        /// <returns>индекс или -1 если не найден </returns>
        private int IndexByID(uint ID)
        {
            int fIdx = -1;
            for (int i = 0; i < _index; i++)
            {
                if (_staff[i].ID==ID)
                {
                    fIdx = i;
                    break;
                }
            }

            return fIdx;
        }
        
        /// <summary>
        /// Удаление элемента по индексу в массиве
        /// </summary>
        /// <param name="index">индекс в массиве</param>
        public void RemoveAt(int index)
        {
            // сдвигаем элементы вверх после индекса
            for (int i = index; i < _index - 1; i++)
            {
                // сдвигаем элементы вверх после индекса
                _staff[i] = _staff[i + 1];
            }
            // уменьшаем размер значимой части
            _index--;
            
            // можно еще сжать сам размер массива
            // Array.Resize(ref _staff, _staff.Length - 1);
        }


        /// <summary>
        /// Удаление элемента по ID
        /// </summary>
        /// <param name="ID">ID элемента</param>
        public void Remove(uint ID)
        {
            int fIdx = IndexByID(ID);
            if (fIdx >= 0) RemoveAt(fIdx); 
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
            for (int i = 0; i < this._index; i++)
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
            Range idxRange = new Range(0, _index);
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
        
            for (int i = 0; i < _index; i++)
            {
                Console.WriteLine(this._staff[i].GetPrintLine());
            }
            Console.WriteLine($"Кол-во элементов в хранилище: {Count}");
            Console.WriteLine();
        }

        /// <summary>
        /// Количество сотрудников в хранилище
        /// </summary>
        public int Count { get { return this._index; } }

    }

