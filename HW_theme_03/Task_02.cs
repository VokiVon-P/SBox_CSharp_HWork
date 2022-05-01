/*
Задание 2. Программа подсчёта суммы карт в игре «21»


Что нужно сделать
Есть довольно простая карточная игра, она называется Blackjack. Суть игры сводится к подсчёту карт. Каждая карта имеет свой «вес». Напишите программу, которая подсчитает сумму всех карт у нас на руках. Задача программы сводится к следующему алгоритму:

    1. Программа приветствует пользователя и спрашивает, сколько у него на руках карт.

    2. Пользователь вводит целое число.

    3. Преобразуем это число в счётчик для цикла.

    4. С помощью цикла for итеративно просим пользователя ввести номинал каждой карты. Для карт с числовым номиналом он вводит только цифру. 

Для «картинок» следует принять обозначения латинскими буквами:

Валет = J

Дама = Q

Король = K

Туз = T

    5. Внутри цикла, используя оператор switch, «вес» каждой карты складываем в общую переменную суммы, которая объявлена до тела основного цикла. Для числовых карт их «вес» равен весу, указанному при вводе пользователем. Для «картинок» «вес» равен 10.

    6. По завершении ввода на экране появится значение суммы карт.
 */

using System.Diagnostics;

namespace HW_theme_03
{
    class Task_02
    {
        public static void Start(string[] Args)
        {
            int sum = 0;
            
            Console.WriteLine("");
            Console.WriteLine("Выполняется Task_02!");
            Console.WriteLine("Программа подсчёта суммы карт в игре «21»\n");
            Console.WriteLine("Валет = J, Дама = Q, Король = K, Туз = T\n");
            
            Console.Write("Сколько у вас карт на руках?: ");
            int counter = int.Parse(Console.ReadLine());

            while (counter > 0)
            {
                // Console.WriteLine($"{counter}");
                
                Console.Write("Введите номинал следующей карты : ");
                string cardInput = Console.ReadLine().ToLower();
                
                int cardValue;
                int.TryParse(cardInput, out cardValue);

                if (cardValue < 0) cardValue = 0;
                else if (cardValue > 10) cardValue = 10;
                
                switch (cardInput)
                {
                    case "j":
                    case "q":
                    case "k":
                    case "t": sum += 10;
                        break;
                    default: sum += cardValue;
                        break;
                }
                
               Debug.Print($"step_info: counter={counter, 2}, card_input={cardInput, 2} " +
                            $"card_value={cardValue, 2}, step_sum={sum, 4}");   
               counter--;
            }
            
            Console.WriteLine($"\nСумма карт на руках = {sum, 3}\n".ToUpper());
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
    }
}
