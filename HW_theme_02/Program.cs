namespace HW_theme_02
{
    class Program
    {
        static void Main(string[] args)
        {
            // Заказчик просит написать программу «Записная книжка». Оплата фиксированная - $ 120.

            // В данной программе, должна быть возможность изменения значений нескольких переменных для того,
            // чтобы персонифецировать вывод данных, под конкретного пользователя.

            // Для этого нужно: 
            // 1. Создать несколько переменных разных типов, в которых могут храниться данные
            //    - имя;
            //    - возраст;
            //    - рост;
            //    - баллы по трем предметам: история, математика, русский язык;

            // 2. Реализовать в системе автоматический подсчёт среднего балла по трем предметам, 
            //    указанным в пункте 1.

            // 3. Реализовать возможность печатки информации на консоли при помощи 
            //    - обычного вывода;
            //    - форматированного вывода;
            //    - использования интерполяции строк;

            // 4. Весь код должен быть откомментирован с использованием обычных и хml-комментариев

            // **
            // 5. В качестве бонусной части, за дополнительную оплату $50, заказчик просит реализовать 
            //    возможность вывода данных в центре консоли.

            string firstName;
            string lastName;
            byte age;
            uint stature;
            float scoreHistory, scoreMath, scoreRusLang; 
            
            ConsoleColor consoleColor = ConsoleColor.Yellow;
            Console.ForegroundColor = consoleColor;
            int center = Console.WindowWidth/2;
            
            Console.CursorLeft = center;
            Console.Write("Имя: ");
            firstName = Console.ReadLine();
            
            Console.CursorLeft = center;
            Console.Write("Фамилия: ");
            lastName = Console.ReadLine();
            
            Console.CursorLeft = center;
            Console.Write("Возраст: ");
            age = byte.Parse(Console.ReadLine());
            
            Console.CursorLeft = center;
            Console.Write("Рост: ");
            stature = uint.Parse(Console.ReadLine());

            Console.ForegroundColor = ConsoleColor.Green;
            Console.CursorLeft = center;
            Console.Write("Балл по Истории: ");
            scoreHistory = float.Parse(Console.ReadLine());
            
            Console.CursorLeft = center;
            Console.Write("Балл по Математике: ");
            scoreMath = float.Parse(Console.ReadLine());
            
            Console.CursorLeft = center;
            Console.Write("Балл по Русскому Языку: ");
            scoreRusLang = float.Parse(Console.ReadLine());

            float meanScore = (float) ((scoreHistory + scoreMath + scoreRusLang) / 3.0);
            
            // Вывод результатов
            
            Console.WriteLine();
            Console.ForegroundColor = consoleColor;
            Console.CursorLeft = center;
            string pattern = "Имя: {0}  Фамилия: {1}  Возраст: {2}  Рост: {3} ";
            Console.WriteLine(pattern,
                firstName,
                lastName,
                age,
                stature);
            
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.CursorLeft = center;
            Console.WriteLine($"Среднее значение баллов по всем предметам = {meanScore}");
            
            Console.ReadKey();
        }
    }
}