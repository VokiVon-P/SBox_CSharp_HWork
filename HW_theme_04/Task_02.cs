﻿/*
Задание 2. Наименьший элемент в последовательности


Что нужно сделать
Найдите наименьший элемент в последовательности, которую вводит пользователь. 
Последовательность нужно сохранить в массив. Детальный алгоритм программы:

Пользователь вводит длину последовательности. 
Затем пользователь последовательно вводит целые числа (как положительные, так и отрицательные). 
Числа разделяются клавишей Enter.
Каждое введённое число помещается в соответствующий элемент массива.
После окончания ввода данных отдельный цикл проходит по последовательности и находит минимальное число. 
Для этого он сравнивает каждое число в итерации с предыдущим найденным минимальным числом. 


Рекомендация
Чтобы правильно организовать поиск наименьшего числа, на этапе инициализации, 
в качестве начального значения минимального числа, выберите int.MaxValue. 
Тогда любое число из массива, какое бы вы ни взяли, окажется меньше, чем это значение.


Что оценивается
Программа выводит на экран наименьшее число из последовательности пользователя. 
 */

using System.Diagnostics;

namespace HW_theme_04
{
    class Task_02
    {
        public static void Start(string[] Args)
        {
            Console.WriteLine("");
            Console.WriteLine("Выполняется Task_02!");
            Console.WriteLine("Наименьший элемент в последовательности\n");
            
            Console.Write("Введите длину последовательности: ");
            int length = int.Parse(Console.ReadLine());

            int[] narray = new int[length];
            int minValue = Int32.MaxValue;

            for (int i = 0; i < length; i++)
            {
                Console.Write($"Значение элемента {i}:  ");
                narray[i] = int.Parse(Console.ReadLine());
                if (minValue > narray[i]) minValue = narray[i]; 
                Debug.Print($"Значение элемента {i}:{narray[i]}\t minValue = {minValue}");
            }
            
            Console.WriteLine($"Наименьшее число из последовательности = {minValue}".ToUpper());
            
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
