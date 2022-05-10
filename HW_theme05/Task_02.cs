/*
Задание 2. Перестановка слов в предложении

Что нужно сделать
Пользователь вводит в программе длинное предложение. 
Каждое слово раздельно одним пробелом. 
После ввода пользователь нажимает клавишу Enter.
 
Необходимо создать два метода:

первый метод разделяет слова в предложении;
второй метод меняет эти слова местами (в обратной последовательности). 
При этом важно учесть, что один метод вызывается внутри другого метода, 
то есть в методе main вызывается метод cо следующей сигнатурой — ReversWords (string inputPhrase). 
Внутри этого метода вызывается метод по разделению входной фразы на слова.


Советы и рекомендации
Для сложения строк можно использовать конкатенацию строк. Выражение вида ResultString += NewString + “ ” добавит к текущей строке, которая представлена переменной ResultString, новую строку из переменной NewString и также добавит пробел к концу строки. 
Для реализации алгоритма разделения строки на слова можно воспользоваться рекомендациями из задания 1.

Что оценивается
Вызов метода по разделению на слова происходит внутри метода, 
который отвечает непосредственно за инвертирование слов в предложении.
 */

using System.Diagnostics;

namespace HW_theme_05
{
    class Task_02
    {
        public static void Start(string[] Args)
        {
            int sum = 0;
            
            Console.WriteLine();
            Console.WriteLine("Выполняется Task_02!");
            Console.WriteLine("Перестановка слов в предложении\n");
            
            Console.Write("Введите предложение: ");
            string text = Console.ReadLine();
            string reverseText = ReversWords(text);
            Console.WriteLine(reverseText);
            Console.WriteLine();
            Console.WriteLine("Нажмите любую клавишу...");
            Console.ReadKey();
        }
        
        static string[] SplitText(string InputText)
        {
            return InputText.Split();
        }

        static string ReversWords(string inputPhrase)
        {
            string outPhrase = ""; 
            string[] words = SplitText(inputPhrase);
            for (int i = words.Length - 1; i >= 0; i--)
            {
                outPhrase += words[i] + " ";
            }

            return outPhrase;
        }
    }
}
