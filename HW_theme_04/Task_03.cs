/*
Задание 3. Игра «Угадай число» 


Что нужно сделать
Разработайте программу по следующему алгоритму:

Пользователь вводит максимальное целое число диапазона. 
На основе диапазона целых чисел (от 0 до «введено пользователем») программа 
генерирует случайное целое число из диапазона. 
Пользователю предлагается ввести загаданное программой случайное число. 
Пользователь вводит свои предположения в консоли приложения. 
Если число меньше загаданного, программа сообщает об этом пользователю. 
Если больше, то тоже сообщает, что число больше загаданного. 
Программа завершается, когда пользователь угадал число. 
Если пользователь устал играть, 
то вместо ввода числа он вводит пустую строку и нажимает Enter. 
Тогда программа завершается, предварительно показывая, какое число было загадано.


Советы и рекомендации
Чтобы организовать бесконечный ввод чисел и дать пользователю возможность вводить сколько угодно чисел,
используйте цикл while или do while с условием (1 == 1) или просто (true).
Для выхода из бесконечного цикла используйте break.


Что оценивается
Программа опрашивает пользователя и просит вводить числа. 
Реализована возможность выхода из бесконечного цикла, когда пользователь устал.
Демонстрируется загаданный результат. 
 */

using System.Diagnostics;

namespace HW_theme_04
{
    class Task_03
    {
        public static void Start(string[] Args)
        {
            
            Console.WriteLine("");
            Console.WriteLine("Выполняется Task_03!");
            Console.WriteLine("");
            Console.WriteLine("Игра «Угадай число»");
            
            Console.Write("Введите целое число > 0: ");
            int rightRange = int.Parse(Console.ReadLine());

            int secret = new Random().Next(rightRange);
            Console.WriteLine("Число загадано!\n");
            Debug.Print($"Загадали число {secret} !");

            int value;
            bool flagNext = false;
            int counter = 0; 
            while (true)
            {
                Console.Write("Попытайтесь удагадать [введите число]: ");
                flagNext = int.TryParse(Console.ReadLine(), out value);
                if (flagNext)
                {
                    if (secret > value)
                    {
                        Console.WriteLine("Загаднное число БОЛЬШЕ введенного");
                    }
                    else
                    {
                        if (secret < value)
                        {
                            Console.WriteLine("Загаднное число МЕНЬШЕ введенного");
                        }
                        else
                        {
                            Console.WriteLine($"вы угадали! Загаданное число = {secret}".ToUpper());
                            Console.WriteLine($"Число попыток = {counter}");
                            break;
                        }
                    }
                    
                }
                else
                {
                    Console.WriteLine("Прерываем игру!");
                    Console.WriteLine($"\nЗагаданное число = {secret}".ToUpper());
                    break;
                }

                counter++;

            }
            // if (value <= 1)
            // {
            //     Console.WriteLine("Ошибка ввода - завершаем программу");
            //     Console.WriteLine("Нажмите любую клавишу...");
            //     Console.ReadKey();
            //     return;
            // }
            //
            // bool flag = false;
            // int counter = 2;
            // while (counter < value)
            // {
            //     flag = ((value % counter) == 0);
            //     Debug.Print($"counter= {counter}, div = ({(value % counter)}), value= {value}");
            //     if (flag) break;
            //     counter++;
            // }
            //
            // string result = flag ? $"Число {value} не евляется простым [делится без остатка на {counter}]" 
            //     : $"Число {value} является простым";
            // Console.WriteLine(result.ToUpper());
           
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
            
        }
    }
}
