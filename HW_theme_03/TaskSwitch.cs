namespace HW_theme_03;

public class BaseSwitch
{
    static void Main(string[] Args)
    {
        int userChoice;
        
        Console.WriteLine("Доступны следующие номера задач: 1; 2; 3 ");
        Console.WriteLine("Для выхода введите любой другой символ");
        Console.Write("Введите номер задачи для исполнения:");
        string inputValue = Console.ReadLine();
        
        Action<string[]> executor = CloseProg; // делегат для исполнения
        
        if (int.TryParse(inputValue, out userChoice))
        {
            switch (userChoice)
            {
                case 1 : Console.WriteLine($"Будет выполнена задача {userChoice}");
                    executor = Task_01.Start;
                    break;
                case 2 : Console.WriteLine($"Будет выполнена задача {userChoice}");
                    executor = Task_02.Start;
                    break;
                case 3 : Console.WriteLine($"Будет выполнена задача {userChoice}");
                    executor = Task_03.Start;
                    break;
                // case 4 : Console.WriteLine($"Будет выполнена задача {userChoice}");
                //     executor = Program.StartBonus;
                //     break;
            }
        }
        
        executor(Args);
       
    }
    
    static void CloseProg(string[] Args)
    {
        Console.WriteLine("Некорректный ввод номера задачи");
        Console.WriteLine("Завершение программы");
        Console.WriteLine("Нажмите любую клавишу...");
        Console.ReadKey();
    }

}