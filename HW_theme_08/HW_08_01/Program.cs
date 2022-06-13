// See https://aka.ms/new-console-template for more information

using System.Linq.Expressions;

Console.WriteLine("Hello, World!");

Random rnd = new Random();
List<int> mainList = new List<int>();

void FillList(List<int> list)
{
    for (int i = 0; i < 100; i++)
    {
        mainList.Add(rnd.Next(0, 100));
    }
    Console.WriteLine(list.Count);
}

void PrintList(List<int> list)
{
    for (int i = 0; i < list.Count; i++)
    {
        if (i % 25 == 0) Console.WriteLine();
        Console.Write($"{list[i]} ");
    }
    Console.WriteLine();
}

void FilterList(List<int> list)
{
    // инлайн условие отбора
    bool FilterCondition(int x) => x is > 25 and < 50; 
    
    list.RemoveAll(FilterCondition);
    Console.WriteLine(list.Count);
}

FillList(mainList);
PrintList(mainList);
FilterList(mainList);
PrintList(mainList);    