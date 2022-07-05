namespace HW_theme_09;

/// <summary>
/// Служебный - получение токена бота
/// </summary>
public class BotConfig
{
    public static readonly string BotToken = File.ReadAllText(@"BotToken");
    
}