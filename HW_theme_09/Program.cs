// See https://aka.ms/new-console-template for more information

using HW_theme_09;
using Telegram.Bot;
using Telegram.Bot.Polling;
using Telegram.Bot.Types.Enums;

Console.WriteLine("Hello, Voki Bot!");
Console.WriteLine($"{BotConfig.BotToken}");

var bot = new TelegramBotClient(BotConfig.BotToken);

var me = await bot.GetMeAsync();
Console.Title = me.Username ?? "My awesome Bot";

using var cts = new CancellationTokenSource();

// StartReceiving does not block the caller thread. Receiving is done on the ThreadPool.
var receiverOptions = new ReceiverOptions()
{
    AllowedUpdates = Array.Empty<UpdateType>(),
    ThrowPendingUpdates = true,
};

bot.StartReceiving(updateHandler: UpdateHandlers.HandleUpdateAsync,
    pollingErrorHandler: UpdateHandlers.PollingErrorHandler,
    receiverOptions: receiverOptions,
    cancellationToken: cts.Token);

Console.WriteLine($"Start listening for @{me.Username}");
Console.ReadLine();

// Send cancellation request to stop bot
cts.Cancel();