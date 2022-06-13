// See https://aka.ms/new-console-template for more information

using System.Xml.Linq;
using Newtonsoft.Json;

Console.WriteLine("Задание 4. Записная книжка");
Console.WriteLine();
Console.WriteLine("Введите данные контакта.");

// ввод информации и формирование XElement
XElement GetInfo()
{
    XElement person = new XElement("Person");
    Console.Write("ФИО: ");
    person.SetAttributeValue("name", Console.ReadLine());

    // добавление адреса
    XElement address = new XElement("Address");
    Console.Write("Улица: ");
    address.Add(new XElement("Street", Console.ReadLine()));

    Console.Write("Номер дома: ");
    address.Add(new XElement("HouseNumber", Console.ReadLine()));

    Console.Write("Номер квартиры: ");
    address.Add(new XElement("FlatNumber", Console.ReadLine()));

    person.Add(address);
    
    // добавление телефонов
    XElement phoneInfo = new XElement("Phones");
    
    Console.Write("Мобильный телефон: ");
    phoneInfo.Add(new XElement("MobilePhone", Console.ReadLine()));
    
    Console.Write("Домашний телефон: ");
    phoneInfo.Add(new XElement("FlatPhone", Console.ReadLine()));
    
    person.Add(phoneInfo);

    return person;
}

// вывод информации в консоль
void PrintPersonInfo(XElement info)
{
    Console.WriteLine();
    Console.WriteLine("Информация о контакте:");
    Console.WriteLine(info);
}

// сохранение в XML
void SaveToXML(XElement info)
{
    const string XMLfileName = "person.xml";
    Console.WriteLine();
    info.Save(XMLfileName);
    Console.WriteLine($"Сохранено в файл {XMLfileName}.");
    
}

// сохранение в JSON
void SaveToJson(XElement info)
{
    const string jsonFileName = "person.json";
    Console.WriteLine();
    
    string json = JsonConvert.SerializeObject(info);
    File.WriteAllText(jsonFileName, json);
    
    Console.WriteLine($"Сохранено в файл {jsonFileName}.");
    
}


// ================
// блок исполнения

XElement personInfo = GetInfo();
PrintPersonInfo(personInfo);
SaveToXML(personInfo);
SaveToJson(personInfo);
