namespace HW_theme_07;

public struct Employee
{
    private const string stripper = "#"; // разделитель для загрузки/сохранения
    
    const string infoTemplate = "ID,Дата и время записи,Ф.И.О.,Возраст,Рост,Дата рождения,Место рождения";

    private static Random rnd = new Random();
    
    /// <summary>
    /// ID
    /// </summary>
    public long ID { get; private set; }

    /// <summary>
    /// Дата и время создания  
    /// </summary>
    public DateTime CreateDate  {get; private set; }

    /// <summary>
    /// Ф.И.О
    /// </summary>
    public string FullName { get; set; }

    /// <summary>
    /// Возраст
    /// </summary>
    public uint Age { get; set; }

    /// <summary>
    /// Рост
    /// </summary>
    public uint Tall { get; set; }

    /// <summary>
    /// Дата рождения
    /// </summary>
    public DateTime BirthDate { get; set; }

    /// <summary>
    /// Место рождения
    /// </summary>
    public string BirthPlace { get; set; }

    /// <summary>
    /// Наименования полей
    /// </summary>
    public static string[] Titles { get; } = infoTemplate.Split(',');

    /// <summary>
    /// Бозовый конструктор с минимальным заполнением полей
    /// </summary>
    public Employee()
    {
        FullName = null;
        Age = 0;
        Tall = 0;
        BirthDate = default;
        BirthPlace = null;

        ID = rnd.Next(1000);
        CreateDate = DateTime.Now;
    }
    
    /// <summary>
    /// Конструктор с расширенной информацией
    /// </summary>
    /// <param name="fullName">Ф.И.О</param>
    /// <param name="age">Возраст</param>
    /// <param name="tall">Рост</param>
    /// <param name="birthDate">Дата рождения</param>
    /// <param name="birthPlace">Место рождения</param>
    public Employee(string fullName, uint age, uint tall, DateTime birthDate, string birthPlace)
    {
        FullName = fullName;
        Age = age;
        Tall = tall;
        BirthDate = birthDate;
        BirthPlace = birthPlace;

        ID = rnd.Next(1000);
        CreateDate = DateTime.Now;
    }

    /// <summary>
    /// Конструктор из строки загрузки
    /// </summary>
    /// <param name="loadInfo">строка загрузки</param>
    public Employee(string loadInfo)
    {
        string[] _loadData = loadInfo.Split('#');
        
        ID = Convert.ToInt64(_loadData[0]);
        CreateDate = Convert.ToDateTime(_loadData[1]);
        FullName = _loadData[2];
        Age = Convert.ToUInt32(_loadData[3]);
        Tall = Convert.ToUInt32(_loadData[4]);
        BirthDate = Convert.ToDateTime(_loadData[5]);
        BirthPlace = _loadData[6];
    }
    
    public void Print()
    {
        Console.Write(Titles[0] + " : ");
        Console.WriteLine(ID);
        Console.Write(Titles[1] + " : ");
        Console.WriteLine(CreateDate);
        Console.Write(Titles[2] + " : ");
        Console.WriteLine(FullName);
        Console.Write(Titles[3] + " : ");
        Console.WriteLine(Age);
        Console.Write(Titles[4] + " : ");
        Console.WriteLine(Tall);
        Console.Write(Titles[5] + " : ");
        Console.WriteLine(BirthDate.ToShortDateString());
        Console.Write(Titles[6] + " : ");
        Console.WriteLine(BirthPlace);
    }

    public string GetSaveLine()
    {
        string saveLine = String.Empty;
        saveLine += ID + stripper;
        saveLine += CreateDate + stripper;
        saveLine += FullName + stripper;
        saveLine += Age + stripper;
        saveLine += Tall + stripper;
        saveLine += BirthDate.ToShortDateString() + stripper;
        saveLine += BirthPlace;
        return saveLine;
    }
    
    /// <summary>
    /// Строка для печати
    /// </summary>
    /// <returns>строка вывода на печать</returns>
    public string GetPrintLine()
    {
         return $"{ID, 15} {CreateDate,20} {FullName,40} {Age,8} {Tall,8} {BirthDate.ToShortDateString(), 20} {BirthPlace, 20}";    
    }
    
    /// <summary>
    /// Строка с заголовками
    /// </summary>
    /// <returns>строка с заголовками</returns>
    public static string GetTitleLine()
    {
        return $"{Titles[0],15} {Titles[1],20} {Titles[2],40} {Titles[3],8} {Titles[4],8} {Titles[5],20} {Titles[6],20}";
    }
    
    
    
}