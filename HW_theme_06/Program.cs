// See https://aka.ms/new-console-template for more information
// net6.0

/*
 Что нужно сделать
Создайте справочник «Сотрудники».

Разработайте для предполагаемой компании программу, 
которая будет добавлять записи новых сотрудников в файл. Файл должен содержать следующие данные:

ID
Дату и время добавления записи
Ф. И. О.
Возраст
Рост
Дату рождения
Место рождения
Для этого необходим ввод данных с клавиатуры. После ввода данных:

если файла не существует, его необходимо создать; 
если файл существует, то необходимо записать данные сотрудника в конец файла. 
При запуске программы должен быть выбор:

введём 1 — вывести данные на экран;
введём 2 — заполнить данные и добавить новую запись в конец файла.

 */

using System.Text;

const string fileEmployees = @"Employees.csv";
const string stripper = "#";

const string infoTemplate = "ID,Дата,Ф.И.О.,Возраст,Рост,Дата рождения,Место рождения";
string[] employeeInfoHeader = infoTemplate.Split(',');

Random rnd = new Random();

Console.WriteLine("Справочник <Сотрудники>");
Console.WriteLine();
Console.WriteLine("[1 - вывод данных на экран]");
Console.WriteLine("[2 - добавить нового сотрудника]");
Console.WriteLine("[0 или другой символ - выйти из программы]");

bool flagExit = false;
while (!flagExit)
{
    Console.WriteLine();
    Console.Write("Введите номер команды: ");
    int value;
    if (int.TryParse(Console.ReadLine(), out value))
        switch (value)
        {
            case 1:
                ShowEmployees();
                break;
            case 2:
                AddPerson();
                break;
            default:
                flagExit = true;
                break;
            
        }
    else break;
}

Console.WriteLine();
Console.WriteLine("Завершение программы");
Console.Write("Нажмите любую клавишу...");
Console.ReadKey();

void AddPerson()
{
    string line = "";
        
    Console.WriteLine("Добавление сотрудника!");
    Console.WriteLine();

    // ID
    Console.Write(employeeInfoHeader[0] + " : ");
    long id = rnd.NextInt64(int.MaxValue);
    Console.WriteLine(id);
    line += id + stripper;
    
    // Data
    Console.Write(employeeInfoHeader[1] + " : ");
    DateTime nowDate = DateTime.Now;
    string now = $"{nowDate.ToShortDateString()} {nowDate.ToShortTimeString()}";
    Console.WriteLine(now);
    line += now + stripper;
    
    // Ф.И.О.
    Console.Write(employeeInfoHeader[2] + " : ");
    string?  fullName = Console.ReadLine();
    line += fullName + stripper;
    
    // Возраст
    Console.Write(employeeInfoHeader[3] + " : ");
    int  age = int.Parse(Console.ReadLine() ?? string.Empty);
    line += age + stripper;

    // Рост
    Console.Write(employeeInfoHeader[4] + " : ");
    int  tall = int.Parse(Console.ReadLine() ?? string.Empty);
    line += tall + stripper;

    // Дату рождения
    Console.Write(employeeInfoHeader[5] + " : ");

    if (DateTime.TryParse(Console.ReadLine(), out var birthDate))
    {
        line += birthDate.ToShortDateString() + stripper;
    }
    else
    {
        line += stripper;
    }
        
    // Место рождения
    Console.Write(employeeInfoHeader[6] + " : ");
    string? birthPlace =  Console.ReadLine();
    line += birthPlace;

    using (StreamWriter sw = new StreamWriter(fileEmployees, true, Encoding.Unicode))
    {
        sw.WriteLine(line);  
        // Console.WriteLine(line);
    }
}

void ShowEmployees()
{
    #region ReadFileEmployees
    
    Console.WriteLine("Справочник сотрудников!");
    Console.WriteLine();

    try
    {
        using (StreamReader sr = new StreamReader(fileEmployees, Encoding.Unicode))
        {
            
            // Console.WriteLine(String.Concat(employeeInfoHeader));
            string? line;
            while ((line = sr.ReadLine()) != null)
            {
                
                string[] data = line.Split('#');
                for (int i = 0; i < employeeInfoHeader.Length; i++)
                {
                    Console.WriteLine(employeeInfoHeader[i] + "\t" + data[i]);
                }
                
                Console.WriteLine();
                Console.WriteLine("----------------------");
                Console.WriteLine();
            }
        }

    }
    catch (FileNotFoundException e)
    {
        Console.WriteLine($"Невозможно прочесть файл данных! [{e.FileName}]");
        Console.WriteLine($"Возможно файл еще не создан!");
    }
    catch (Exception e)
    {
        Console.WriteLine(e);
        throw;
    }
    
    #endregion
}