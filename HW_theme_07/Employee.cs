namespace HW_theme_07;

public struct Employee
{
    /// <summary>
    /// ID
    /// </summary>
    public long ID { get; private set; }

    /// <summary>
    /// Дата и время создания  
    /// </summary>
    private DateTime _dateTime;

    /// <summary>
    /// Ф.И.О
    /// </summary>
    private string _fullName;
    
    /// <summary>
    /// Возраст
    /// </summary>
    private uint _age;

    /// <summary>
    /// Рост
    /// </summary>
    private uint _tall;

    /// <summary>
    /// Дата рождения
    /// </summary>
    private DateTime _birthDate;

    /// <summary>
    /// Место рождения
    /// </summary>
    private string _birthPlace;

}