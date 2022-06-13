// See https://aka.ms/new-console-template for more information

/*
 * Что нужно сделать
 * Пользователь вводит число. Число сохраняется в HashSet коллекцию.
 * Если такое число уже присутствует в коллекции,
 * то пользователю на экран выводится сообщение, что число уже вводилось ранее.
 * Если числа нет, то появляется сообщение о том, что число успешно сохранено. 
 */


Console.WriteLine("Задание 3. Проверка повторов");

HashSet<int> numSet = new HashSet<int>();

// добавление и проверка одной записи
bool AddOneRecord()
{
    Console.WriteLine();
    Console.Write("Введите целое число для записи: ");
    string input = Console.ReadLine();
    if (input == string.Empty) return false;
    
    int value;
    if (!int.TryParse(input, out value)) return true;
    
    string addMessage = numSet.Add(value)
     ? $"Запись [{value}] успешно добавлена."
     : $"Запись [{value}] уже существует.";

    Console.WriteLine(addMessage);
    return true;
}

// ввод данных 
void AddRecords()
{
    Console.WriteLine("Ввод данных для HashSet.");
    Console.WriteLine("Для завершение нажмите Enter в поле ввода.");
    while (AddOneRecord()){}
    Console.WriteLine("Ввод данных завершен!");
    
}

// печать содержания 
void PrintRecords()
{
    Console.WriteLine();
    Console.WriteLine("Содержание HashSet:");
    foreach (var item in numSet)
    {
        Console.WriteLine(item);
    }
    Console.WriteLine();
}

// ================
// блок исполнения
AddRecords();
PrintRecords();
