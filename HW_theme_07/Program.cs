// See https://aka.ms/new-console-template for more information
// net6.0

/*
Что нужно сделать
Улучшите программу, которую разработали в модуле 6. 
Создайте структуру «Сотрудник» со следующими полями:

ID
Дата и время добавления записи
Ф.И.О.
Возраст
Рост
Дата рождения
Место рождения


Для записей реализуйте следующие функции:

Просмотр записи. Функция должна содержать параметр ID записи, которую необходимо вывести на экран. 
Создание записи.
Удаление записи.
Редактирование записи.
Загрузка записей в выбранном диапазоне дат.
Сортировка по возрастанию и убыванию даты.


После всех изменений записей создайте метод перезаписи изменённых данных в файл в таком виде:

1#20.12.2021 00:12#Иванов Иван Иванович#25#176#05.05.1992#город Москва
2#15.12.2021 03:12#Алексеев Алексей Иванович#24#176#05.11.1980#город Томск

…



Советы и рекомендации
Обратите внимание, что в строке есть символ # — разделитель. Символа # не должно быть при чтении (разбить строку на массив поможет разделение строк с помощью метода String.Split).
Создайте методы для работы с записями.
Файл может быть с данными изначально. Для примера скопируйте данные, продемонстрированные выше.


Что оценивается
Создан ежедневник, в котором могут храниться записи.
Одно из полей записи ― дата создания.
Все записи сохраняются на диске.
Все записи загружаются с диска.
С диска загружаются записи в выбранном диапазоне дат.
Записи можно создавать, редактировать и удалять.
Записи сортируются по выбранному полю.

 */

using System.Text;
using HW_theme_07;

const string fileData = @"data.txt";
const string stripper = "#";

const string infoTemplate = "ID,Дата,Ф.И.О.,Возраст,Рост,Дата рождения,Место рождения";
string[] employeeInfoHeader = infoTemplate.Split(',');

Random rnd = new Random();

// Console.WriteLine("Справочник <Сотрудники>");
// Console.WriteLine();
// Console.WriteLine("[1 - вывод данных на экран]");
// Console.WriteLine("[2 - добавить нового сотрудника]");
// Console.WriteLine("[0 или другой символ - выйти из программы]");
//
// bool flagExit = false;
// while (!flagExit)
// {
//     Console.WriteLine();
//     Console.Write("Введите номер команды: ");
//     int value;
//     if (int.TryParse(Console.ReadLine(), out value))
//         switch (value)
//         {
//             case 1:
//                 ShowEmployees();
//                 break;
//             case 2:
//                 AddPerson();
//                 break;
//             default:
//                 flagExit = true;
//                 break;
//             
//         }
//     else break;
// }
//
// Console.WriteLine();
// Console.WriteLine("Завершение программы");
// Console.Write("Нажмите любую клавишу...");
// Console.ReadKey();

Repository repo = new Repository(fileData);
repo.PrintDbToConsole();
Console.ReadKey();

repo.SortByCreateDate();
repo.PrintDbToConsole();
// AddPerson();
Console.ReadKey();

Console.WriteLine();
Console.WriteLine(DateTime.MinValue);
Console.WriteLine();

repo.SortByCreateDate(true);
repo.PrintDbToConsole();
// repo.Save(fileData);
Console.ReadKey();

void AddPerson()
{
    string line = "";
    Employee worker = new Employee();
    string[] employeeInfoHeader = Employee.Titles;
        
    Console.WriteLine("Добавление сотрудника!");
    Console.WriteLine();

    // ID
    Console.Write(employeeInfoHeader[0] + " : ");
    Console.WriteLine(worker.ID);
    
    // Data
    Console.Write(employeeInfoHeader[1] + " : ");
    string now = $"{worker.CreateDate.ToShortDateString()} {worker.CreateDate.ToShortTimeString()}";
    Console.WriteLine(now);
    
    // Ф.И.О.
    Console.Write(employeeInfoHeader[2] + " : ");
    worker.FullName = Console.ReadLine();
    
    // Возраст
    Console.Write(employeeInfoHeader[3] + " : ");
    worker.Age = uint.Parse(Console.ReadLine() ?? string.Empty);

    // Рост
    Console.Write(employeeInfoHeader[4] + " : ");
    worker.Tall = uint.Parse(Console.ReadLine() ?? string.Empty);

    // Дату рождения
    Console.Write(employeeInfoHeader[5] + " : ");

    if (DateTime.TryParse(Console.ReadLine(), out var birthDate))
    {
        worker.BirthDate = birthDate;
    }
        
    // Место рождения
    Console.Write(employeeInfoHeader[6] + " : ");
    worker.BirthPlace =  Console.ReadLine();
    
    repo.Add(worker);

}

