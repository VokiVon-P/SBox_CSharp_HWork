/*
Задание 1. Случайная матрица


Что нужно сделать
Выведите на экран матрицу заданного размера и заполните её случайными числами.
 
Детальный алгоритм работы приложения:

Сначала пользователю предлагается ввести количество строк в будущей матрице.
Затем — ввести количество столбцов в будущей матрице.
После того, как данные будут получены, нужно создать в памяти матрицу заданного размера.
Используя Random, заполнить матрицу случайными целыми числами.
Вывести полученную матрицу на экран. 
Вывести суммы всех элементов этой матрицы на экран отдельной строкой.


Рекомендация
Для работы с матрицами используйте вложенные циклы.



Что оценивается
Программа выводит на экран случайно созданную матрицу предварительно заданного размера, 
а также сумму всех элементов в ней.
    
*/

namespace HW_theme_04
{
    class Task_01
    {
        public static void Start(string[] Args)
        {
            Console.WriteLine("");
            Console.WriteLine("Выполняется Task_01!");
            
            Console.WriteLine("Случайная матрица\n");
            
            Console.Write("Введите число строк: ");
            int rows = int.Parse(Console.ReadLine());
            Console.Write("Введите число столбцов: ");
            int cols = int.Parse(Console.ReadLine());

            int[,] narray = new int[rows, cols];
            
            Random random = new Random();
            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    narray[i, j] = random.Next(Int32.MinValue, Int32.MaxValue);
                    // narray[i, j] = random.Next(-100, 100);
                    
                    Console.Write($"\t{narray[i,j], 12}");
                }
                Console.WriteLine("");
                
            }
            
            // Вывод суммы массива
            Int64 sum = 0;
            foreach (var item in narray) sum+=item;
            Console.WriteLine($"Сумма всех элементов матрицы = {sum}");

            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
