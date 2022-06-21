using Newtonsoft.Json;
using System;
using Telegram.Bot;
using Telegram.Bot.Extensions.Polling;
using Telegram.Bot.Types;
using Telegram.Bot.Types.ReplyMarkups;

namespace TelegramBot 
{

    public class Program 
    {
        public static async Task SendChoiceBlockAsync(ITelegramBotClient botClient, Update update)
        {
            var message = update.Message;
            var inlineKeyboard = new InlineKeyboardMarkup(new[]
{
                        // first row
                        new []
                        {
                            InlineKeyboardButton.WithCallbackData("Yes or No", "11"),
                            InlineKeyboardButton.WithCallbackData("What is a day", "12"),
                        },
                    });
            await botClient.SendTextMessageAsync(message.Chat, "Нажмите на кнопку:", replyMarkup: inlineKeyboard);

        }

        private const string token = "5329898591:AAEqcU0Zc6KmE_ali21qFIWyA-trg1bXHUM";
        public static ITelegramBotClient bot = new TelegramBotClient(token);
        public static async Task HandleUpdateAsync(ITelegramBotClient botClient, Update update,
                                                   CancellationToken cancellationToken) 
        {
            Console.WriteLine(JsonConvert.SerializeObject(update));
            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.Message)
            {
                var message = update.Message;
                if (message.Text.ToLower() == "/start")
                {
                    await botClient.SendTextMessageAsync(message.Chat, "Привет с большого бодуна! Как тебя звать, путник?", replyMarkup: new ForceReplyMarkup { Selective = true });
                }

                else if (message.Text.ToLower() == "/choice")
                {
                    await SendChoiceBlockAsync(bot, update);
                }

                else if (message.ReplyToMessage != null && message.ReplyToMessage.Text.Contains("Привет с большого бодуна! Как тебя звать, путник?"))
                {
                    var userName = message.Text;
                    await botClient.SendTextMessageAsync(message.Chat, "Дратути еще раз, " + userName + "! Что желаете взглянуть?");

                    await SendChoiceBlockAsync(bot, update);
                }
            }

            if (update.Type == Telegram.Bot.Types.Enums.UpdateType.CallbackQuery)
            {
                var inlineMes = update.CallbackQuery.Data;
                if (inlineMes == "11")
                {
                    YesNo ex = new YesNo();
                    ex.GetJson();
                    var img = JsonConvert.DeserializeObject<AnswerImage>(ex._json);
                    await botClient.SendAnimationAsync(update.CallbackQuery.Message.Chat.Id, img.Image);
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Посмотрим что-нибудь еще? /choice");
                }

                if (inlineMes == "12")
                {
                    Calendar calendar = new Calendar();
                    calendar.GetUrl();
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, calendar.CalendarPath, disableWebPagePreview: true);
                    await botClient.SendTextMessageAsync(update.CallbackQuery.Message.Chat.Id, "Посмотрим что-нибудь еще? /choice");
                }
            }
        }

        public static async Task HandleErrorAsync(ITelegramBotClient botClient,
                                                  Exception exception,
                                                  CancellationToken cancellationToken) 
        {
            Console.WriteLine(JsonConvert.SerializeObject(exception));
        }
        static void Main(string[] args) 
        {
            Console.WriteLine("Запущен бот " + bot.GetMeAsync().Result.FirstName);

            var cts = new CancellationTokenSource();
            var cansellationToken = cts.Token;
            var receiveOptions = new ReceiverOptions
            {
                AllowedUpdates = { },
            };
            bot.StartReceiving(HandleUpdateAsync, HandleErrorAsync, receiveOptions, cansellationToken);
            Console.ReadLine();
        }
    }
}
