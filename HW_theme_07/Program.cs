// See https://aka.ms/new-console-template for more information
// net6.0

/*
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

 */

using HW_theme_07;

const string fileData = @"data.txt";
Repository repo = new Repository(fileData);


bool flagExit = false;
while (!flagExit)
{
    int value = MakeChoice();
    switch (value)
    {
        // вывод таблицы
        case 1:
            ShowEmployees();
            break;
        case 2:
            ShowWorkerInfo();
            break;
        case 3:
            AddPerson();
            break;
        case 4:
            EditPerson();
            break;
        case 5:
            RemovePerson();
            break;
        case 6:
            SortByCreateDate();
            break;
        case 7:
            SortByCreateDateDesc();
            break;
        case 8:
            LoadRepoByDates();
            break;
        case 9:
            LoadRepository();
            break;
        case 10:
            SaveRepository();
            break;        
        
        default:
            flagExit = true;
            break;
        
    }
}

Console.WriteLine();
Console.WriteLine("Завершение программы");
Console.Write("Нажмите любую клавишу...");
Console.ReadKey();


int MakeChoice()
{
    Console.WriteLine();
    Console.WriteLine("Справочник <Сотрудники>");
    Console.WriteLine();
    Console.WriteLine("[1  - вывод данных на экран в виде таблицы]");
    Console.WriteLine("[2  - вывод данных о конкретном сотруднике]");
    Console.WriteLine("[3  - добавить нового сотрудника]");
    Console.WriteLine("[4  - изменить данные сотрудника]");
    Console.WriteLine("[5  - удалить сотрудника]");
    Console.WriteLine("[6  - отсортировать справочник сотрудников по дате создания по возрастанию]");
    Console.WriteLine("[7  - отсортировать справочник сотрудников по дате создания по убыванию]");
    Console.WriteLine("[8  - загрузить записи справочника в выбранном диапазоне дат]");
    Console.WriteLine("[9  - загрузить полный справочник]");
    Console.WriteLine("[10 - сохранить справочник]");
    Console.WriteLine("[0 или другой символ - выйти из программы]");
    
    Console.WriteLine();
    Console.Write("Введите номер команды: ");
    
    int choice;
    int.TryParse(Console.ReadLine(), out choice); 
    return choice;
}

// ввод ID сотрудника
uint GetWorkerID()
{
    Console.WriteLine();
    Console.Write("Введите ID сотрудника: ");
    uint Id;
    uint.TryParse(Console.ReadLine(), out Id); 
    return Id;
}

// [1] распечатать справочник в табличном виде
void ShowEmployees()
{
    repo.PrintDbToConsole();
}

// [2] показать информацию по сотруднику
void ShowWorkerInfo()
{
    uint Id = GetWorkerID();
    repo[Id].Print();
}


// [3] добавить сотрудника
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

// [4] изменить данные сотрудника
void EditPerson()
{
    uint Id = GetWorkerID();
    Employee worker = repo[Id];
    
    string[] employeeInfoHeader = Employee.Titles;
        
    Console.WriteLine("Изменение данных сотрудника!");
    Console.WriteLine();
    Console.WriteLine("Для пропуска редакирования поля введите Enter");
    Console.WriteLine();

    // ID
    Console.Write(employeeInfoHeader[0] + " : ");
    Console.WriteLine(worker.ID);
    
    // Data
    Console.Write(employeeInfoHeader[1] + " : ");
    string now = $"{worker.CreateDate.ToShortDateString()} {worker.CreateDate.ToShortTimeString()}";
    Console.WriteLine(now);
    
    // Ф.И.О.
    Console.Write(employeeInfoHeader[2] + $" [{worker.FullName}] : ");
    string line = Console.ReadLine() ?? string.Empty;
    if (line != String.Empty) worker.FullName = line;
   
    // Возраст
    Console.Write(employeeInfoHeader[3] + $" [{worker.Age}] : ");
    line = Console.ReadLine() ?? string.Empty;
    if (line != String.Empty) worker.Age = uint.Parse(line);

    // Рост
    Console.Write(employeeInfoHeader[4] + $" [{worker.Tall}] : ");
    line = Console.ReadLine() ?? string.Empty;
    if (line != String.Empty) worker.Tall = uint.Parse(line);
    

    // Дату рождения
    Console.Write(employeeInfoHeader[5] + $" [{worker.BirthDate.ToShortDateString()}] : ");
    line = Console.ReadLine() ?? string.Empty;
    if (line != String.Empty 
        & DateTime.TryParse(line, out var birthDate)) worker.BirthDate = birthDate; 
    
        
    // Место рождения
    Console.Write(employeeInfoHeader[2] + $" [{worker.BirthPlace}] : ");
    line = Console.ReadLine() ?? string.Empty;
    if (line != String.Empty) worker.BirthPlace = line;
    
    repo.ReplaceByID(Id, worker);
}

// [5] удаление сотрудника из справочника
void RemovePerson()
{
    Console.WriteLine("Удаление сотрудника из справочника.");
    Console.WriteLine();
    uint Id = GetWorkerID();
    var worker = repo[Id];
    repo.Remove(Id);
    Console.WriteLine($"Сотрудник {worker.FullName} [ID = {worker.ID}] удален из справочника");
}

// [6] сортировка по возрастанию по дате
void SortByCreateDate()
{
    Console.WriteLine("Справочник отсортирован по возрастанию дат создания записей");
    Console.WriteLine();
    repo.SortByCreateDate();
    ShowEmployees();
}

// [7] сортировка по убыванию по дате
void SortByCreateDateDesc()
{
    Console.WriteLine("Справочник отсортирован по убыванию дат создания записей");
    Console.WriteLine();
    repo.SortByCreateDate(ReverseFlag:true);
    ShowEmployees();
}

// [8] загрузка записей в выбранном диапазоне дат
void LoadRepoByDates()
{
    Console.WriteLine("Загрузка записей справочника в выбранном диапазоне дат");
    Console.WriteLine();
    
    Console.Write("Введите левую границу диапазона дат : ");
    string  line = Console.ReadLine() ?? string.Empty;
    DateTime.TryParse(line, out var leftDate); 
    
    Console.Write("Введите правую границу диапазона дат : ");
    line = Console.ReadLine() ?? string.Empty;
    DateTime.TryParse(line, out var rightDate);

    repo = new Repository(fileData, leftDate, rightDate);
    ShowEmployees();
}

// [9] загрузка полного справочника сотрудников
void LoadRepository()
{
    Console.WriteLine("Загрузка полного справочника сотрудников");
    Console.WriteLine();
    repo = new Repository(fileData);
    ShowEmployees();
}

// [10] Сохранение текущего справочника
void SaveRepository()
{
    Console.WriteLine("Сохранение текущего справочника сотрудников");
    Console.WriteLine();
    repo.Save(fileData);
    Console.WriteLine($"Сохранено {repo.Count} записей в файл [{fileData}]");
}

