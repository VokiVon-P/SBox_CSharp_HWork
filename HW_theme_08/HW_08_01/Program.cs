// See https://aka.ms/new-console-template for more information
/*
 * Что нужно сделать
    Создайте лист целых чисел. 
    Заполните лист 100 случайными числами в диапазоне от 0 до 100. 
    Выведите коллекцию чисел на экран. 
    Удалите из листа числа, которые больше 25, но меньше 50. 
    Снова выведите числа на экран. 
 */


// заполнение списка значениями от 0 до 100
void FillList(List<int> list)
{
    Random rnd = new Random();
    for (int i = 0; i < 100; i++)
    {
        list.Add(rnd.Next(0, 100));
    }
}

// печать списка в консоль
void PrintList(List<int> list)
{
    for (int i = 0; i < list.Count; i++)
    {
        if (i % 25 == 0) Console.WriteLine();
        Console.Write($"{list[i], 2} ");
    }
    Console.WriteLine();
    Console.WriteLine($"В списке {list.Count} элементов.");
}

// фильтр списка по условию
void FilterList(List<int> list)
{
    // инлайн условие отбора
    bool FilterCondition(int x) => x is > 25 and < 50; 
    list.RemoveAll(FilterCondition);
    
    Console.WriteLine();
    Console.WriteLine("Список отфильтрован!");
}

// -------------------------------------
// Код исполнения

Console.WriteLine("Задание 1. Работа с листом ");

List<int> mainList = new List<int>();
FillList(mainList);
PrintList(mainList);
FilterList(mainList);
PrintList(mainList);    