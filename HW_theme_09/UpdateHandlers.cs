

using System.Collections;

namespace HW_theme_09;

using Telegram.Bot;
using Telegram.Bot.Exceptions;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using Telegram.Bot.Types.InlineQueryResults;
using Telegram.Bot.Types.InputFiles;
using Telegram.Bot.Types.ReplyMarkups;


public static class UpdateHandlers
{
    static private DirectoryInfo _filesDir = new DirectoryInfo(@"./files");
    
    public static Task PollingErrorHandler(ITelegramBotClient botClient, Exception exception, CancellationToken cancellationToken)
    {
        var ErrorMessage = exception switch
        {
            ApiRequestException apiRequestException => $"Telegram API Error:\n[{apiRequestException.ErrorCode}]\n{apiRequestException.Message}",
            _ => exception.ToString()
        };

        Console.WriteLine(ErrorMessage);
        return Task.CompletedTask;
    }

    public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update, CancellationToken cancellationToken)
    {
        var handler = update.Type switch
        {
            // UpdateType.Unknown:
            // UpdateType.ChannelPost:
            // UpdateType.EditedChannelPost:
            // UpdateType.ShippingQuery:
            // UpdateType.PreCheckoutQuery:
            // UpdateType.Poll:
            UpdateType.Message            => BotOnMessageReceived(botClient, update.Message!),
            UpdateType.EditedMessage      => BotOnMessageReceived(botClient, update.EditedMessage!),
            UpdateType.CallbackQuery      => BotOnCallbackQueryReceived(botClient, update.CallbackQuery!),
            UpdateType.InlineQuery        => BotOnInlineQueryReceived(botClient, update.InlineQuery!),
            UpdateType.ChosenInlineResult => BotOnChosenInlineResultReceived(botClient, update.ChosenInlineResult!),
            _                             => UnknownUpdateHandlerAsync(botClient, update)
        };

        try
        {
            await handler;
        }
        #pragma warning disable CA1031
        catch (Exception exception)
        #pragma warning restore CA1031
        {
            await PollingErrorHandler(botClient, exception, cancellationToken);
        }
    }

    private static async Task BotOnMessageReceived(ITelegramBotClient botClient, Message message)
    {
        string text = $"{DateTime.Now.ToLongTimeString()}: {message.Chat.FirstName} {message.Chat.Id} {message.Text}";

        Console.WriteLine($"{text} TypeMessage: {message.Type.ToString()}");
        Console.WriteLine($"Receive message type: {message.Type}");

        if (message.Type is MessageType.Document)
        {
            Console.WriteLine(message.Document.FileId);
            Console.WriteLine(message.Document.FileName);
            Console.WriteLine(message.Document.FileSize);
            await DownLoadFile(botClient, message, message.Document.FileName, message.Document.FileId);
            return;
        }

        if (message.Type is MessageType.Location or MessageType.Contact)
        {
            await RemoveKeyboard(botClient, message);
            Console.WriteLine(message.ToString());
            return;
        }
        
        if (message.Type is MessageType.Video)
        {
            Console.WriteLine(message.Video.FileId);
            Console.WriteLine(message.Video.FileName);
            await DownLoadFile(botClient, message, message.Video.FileName, message.Video.FileId);
            return;
        }

        if (message.Text is not { } messageText)
            return;

        var action = messageText.Split(' ')[0] switch
        {
            "/list"   => SendInlineKeyboard(botClient, message),
            "/photo"    => SendPhotoFile(botClient, message),
            "/request"  => RequestContactAndLocation(botClient, message),
            "/sendfile" => SendFile(botClient, message),
            _           => Usage(botClient, message)
        };
        Message sentMessage = await action;
        Console.WriteLine($"The message was sent with id: {sentMessage.MessageId}");

        // Кнопки с именами файлов для получения
        // Ответ обрабатываем в BotOnCallbackQueryReceived handler
        static async Task<Message> SendInlineKeyboard(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.Typing);

            // Simulate longer running task
            // await Task.Delay(500);
            
            //---------------------------
            if (_filesDir.Exists)
            {
                List<List<InlineKeyboardButton>> fileButtons = new List<List<InlineKeyboardButton>>(); 
                FileInfo[] files = _filesDir.GetFiles();
                foreach (FileInfo file in files)
                {
                    fileButtons.Add(new List<InlineKeyboardButton>()
                        // используем хэш из за ограничений на размер передаваемых данных в 64 байта
                        {InlineKeyboardButton.WithCallbackData(file.Name, HashCode.Combine(file.FullName).ToString())});
                        Console.WriteLine(file.Name);
                }
                InlineKeyboardMarkup inlineKeyboard = new(fileButtons);
            
            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: "Выберите файл для получения:\n",
                                                        replyMarkup: inlineKeyboard);
            }
            else
            {
                return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                            text: "Нет доступных файлов для получения!"
                                                            );    
            }
            
        }

        // удаление клавиатуры
        static async Task<Message> RemoveKeyboard(ITelegramBotClient botClient, Message message)
        {
            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: "Спасибо за информацию!",
                                                        replyMarkup: new ReplyKeyboardRemove());
        }
        
        // загрузить файл в хранилище
        static async Task<Message> DownLoadFile(ITelegramBotClient botClient, Message message, 
            string fileName, string fileId)
        {
            string path = $"./files/{fileName}"; 
            var file = await botClient.GetFileAsync(fileId);
            FileStream fs = new FileStream(path, FileMode.Create);
            await botClient.DownloadFileAsync(file.FilePath, fs);
            fs.Close();
            fs.Dispose();

            string text = $"Сохранен файл: {fileName}";
            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id, text: text);
        }

        // отправить фото в чат
        static async Task<Message> SendPhotoFile(ITelegramBotClient botClient, Message message)
        {
            await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.UploadPhoto);

            const string filePath = @"photo/01.jpg";
            using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var fileName = filePath.Split(Path.DirectorySeparatorChar).Last();

            return await botClient.SendPhotoAsync(chatId: message.Chat.Id,
                                                  photo: new InputOnlineFile(fileStream, fileName),
                                                  caption: "Случайная картинка");
        }
        
        
        // запросить локацию или контактные данные
        static async Task<Message> RequestContactAndLocation(ITelegramBotClient botClient, Message message)
        {
            ReplyKeyboardMarkup RequestReplyKeyboard = new(
                new[]
                {
                    KeyboardButton.WithRequestLocation("Локация"),
                    KeyboardButton.WithRequestContact("Контактная информация"),
                });

            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: "Вы кто или вы где?",
                                                        replyMarkup: RequestReplyKeyboard);
        }
        
        static async Task<Message> SendFile(ITelegramBotClient botClient, Message message)
        {
            const string sendText = "Прикрепите или перетащите файл для отправки в хранилище.\n" +
                                    "Размер файла не больше 20 мегабайт.";

            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                text: sendText,
                replyMarkup: new ReplyKeyboardRemove());
        }

        static async Task<Message> Usage(ITelegramBotClient botClient, Message message)
        {
            const string usage = "Использование:\n" +
                                 "/start или любое слово - данная инструкция\n" +
                                 "/list   - получить список файлов\n" +
                                 "/sendfile - отправить файл в хранилище\n" +
                                 "/photo    - получить картинку\n" + 
                                 "/request  - запросить локацию или контактные данные";

            return await botClient.SendTextMessageAsync(chatId: message.Chat.Id,
                                                        text: usage,
                                                        replyMarkup: new ReplyKeyboardRemove());
        }
    }

    // Process Inline Keyboard callback data
    private static async Task BotOnCallbackQueryReceived(ITelegramBotClient botClient, CallbackQuery callbackQuery)
    {
        // отправка файла в чат
        static async Task<Message> SendDocFile(ITelegramBotClient botClient, Message message, string filePath)
        {
            await botClient.SendChatActionAsync(message.Chat.Id, ChatAction.UploadDocument);

            using FileStream fileStream = new(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var fileName = filePath.Split(Path.DirectorySeparatorChar).Last();

            return await botClient.SendDocumentAsync(chatId: message.Chat.Id,
                document: new InputOnlineFile(fileStream, fileName),
                caption: $"Файл получен: \n<b>{fileName}</b> !",
                parseMode: ParseMode.Html);
        }

        bool IsFileFound = false;
        string text = "Файл не найден";
        
        FileInfo[] files = _filesDir.GetFiles();
        foreach (FileInfo file in files)
        {
            // находим файл по хэшу
            if (callbackQuery.Data == HashCode.Combine(file.FullName).ToString())
            {
                text = file.FullName;
                IsFileFound = true;
                break;
            }
        }

        //----------------------------------------
        if (IsFileFound)
        {
            await botClient.AnswerCallbackQueryAsync(
                callbackQueryId: callbackQuery.Id,
                text: text);
            
            await SendDocFile(botClient, callbackQuery.Message!, filePath: text);            
        }
        else
        {
            await botClient.SendTextMessageAsync(
                chatId: callbackQuery.Message!.Chat.Id,
                text: text);
        }
        
    }

    private static async Task BotOnInlineQueryReceived(ITelegramBotClient botClient, InlineQuery inlineQuery)
    {
        Console.WriteLine($"Received inline query from: {inlineQuery.From.Id}");

        InlineQueryResult[] results = {
            // displayed result
            new InlineQueryResultArticle(
                id: "1",
                title: "TgBots",
                inputMessageContent: new InputTextMessageContent(
                    "hello"
                )
            )
        };

        await botClient.AnswerInlineQueryAsync(inlineQueryId: inlineQuery.Id,
                                               results: results,
                                               isPersonal: true,
                                               cacheTime: 0);
    }

    private static Task BotOnChosenInlineResultReceived(ITelegramBotClient botClient, ChosenInlineResult chosenInlineResult)
    {
        Console.WriteLine($"Received inline result: {chosenInlineResult.ResultId}");
        return Task.CompletedTask;
    }

    private static Task UnknownUpdateHandlerAsync(ITelegramBotClient botClient, Update update)
    {
        Console.WriteLine($"Unknown update type: {update.Type}");
        return Task.CompletedTask;
    }
}