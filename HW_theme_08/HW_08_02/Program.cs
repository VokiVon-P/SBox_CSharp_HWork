// See https://aka.ms/new-console-template for more information

Console.WriteLine("Задание 2. Телефонная книга");

Dictionary<string, string> phoneBook = new Dictionary<string, string>();

// добавление одной записи
bool AddOneRecord()
{
    Console.WriteLine();
    Console.WriteLine("Введите данные записи:");
    Console.Write("Номер телефона: ");
    string phoneNumber = Console.ReadLine();
    if (phoneNumber == string.Empty) return false;
    Console.Write("ФИО: ");
    string fullName = Console.ReadLine();

    string addMessage = phoneBook.TryAdd(phoneNumber, fullName)
        ? $"Запись [{phoneNumber}: {fullName}] успешно добавлена."
        : $"Запись c телефоном {phoneNumber} уже существует.";

    Console.WriteLine(addMessage);
    return true;
}

// ввод данных в записную книжку
void AddRecords()
{
    Console.WriteLine("Ввод данных записной книжки.");
    Console.WriteLine("Для завершение нажмите Enter в поле 'номер телефона'.");
    while (AddOneRecord()){}
    Console.WriteLine("Ввод данных завершен!");
    
}

// поиск одной записи по телефону
bool FindRecord()
{
    Console.WriteLine();
    
    Console.Write("Номер телефона для поиска: ");
    string phoneNumber = Console.ReadLine();
    if (phoneNumber == string.Empty) return false;
    
    string fullName = string.Empty;
    string findMessage = phoneBook.TryGetValue(phoneNumber, out fullName)
        ? $"Запись [{phoneNumber}: {fullName}] успешно найдена."
        : $"Записи c телефоном {phoneNumber} не существует.";

    Console.WriteLine(findMessage);
    return true;
}

// поиск записей
void FindRecords()
{
    Console.WriteLine("Поиск данных в записной книжке.");
    Console.WriteLine("Для завершение нажмите Enter в поле 'номер телефона'.");
    while (FindRecord()){}
    Console.WriteLine("Поиск данных завершен!");
}

// печать содержания записной книжки
void PrintRecords()
{
    Console.WriteLine();
    Console.WriteLine("Содержание телефонной книги:");
    foreach (var item in phoneBook)
    {
        Console.WriteLine(item);
    }
    Console.WriteLine();
}

// ================
// блок исполнения

AddRecords();
PrintRecords();
FindRecords();

