using App;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Telegram.Bot;
using Telegram.Bot.Args;
using Telegram.Bot.Types;
using Telegram.Bot.Types.Enums;
using TelegramMathBot.View.Commands;
using TelegramMathBot.View.Messages;

namespace TelegramMathBot.View
{

    public class MathBot
    {
        private readonly ConcurrentQueue<Message> recvdMessages = new ConcurrentQueue<Message>();

        private readonly ConcurrentQueue<(Chat chatToSend, IMessage message)> sendMessages =
            new ConcurrentQueue<(Chat chatToSend, IMessage message)>();
        private readonly ClientManager clientManager;
        private readonly Dictionary<Client, ICommand> clientsCommands;
        private readonly TelegramBotClient bot;
        private readonly BotSender botSender;
        public Dictionary<string, ICommand> Commands { get; }
        public MathBot(
            TelegramBotClient bot, 
            BotSender botSender, 
            ClientManager app,
            List<ICommand> commands)
        {
            this.clientManager = app;
            this.Commands = GetDictCommands(commands);
            this.clientsCommands = new Dictionary<Client, ICommand>();
            this.bot = bot;
            this.botSender = botSender;
        }
        
        public void StartReceiving()
        {
            bot.OnMessage += OnMessageReceived;
            bot.StartReceiving();
            var handleThread = new Thread(() =>
            {
                while (true)
                {
                    var msgTaken = recvdMessages.TryDequeue(out var msg);
                    if (msgTaken)
                        sendMessages.Enqueue((msg.Chat, ProcessMessage(msg)));
                    else
                        Thread.Sleep(100);
                }
            });

            var sendThread = new Thread(() =>
            {
                while (true)
                {
                    var msgTaken = sendMessages.TryDequeue(out var msg);
                    if (msgTaken)
                        botSender.SendMessage(msg.message, msg.chatToSend);
                    else
                        Thread.Sleep(100);
                }
            });

            handleThread.IsBackground = true;
            sendThread.IsBackground = true;

            handleThread.Start();
            sendThread.Start();

            while (true) ;
        }

        private void OnMessageReceived(object sender, MessageEventArgs messageEvents)
        {
            var message = messageEvents.Message;

            if (message.Type == MessageType.Text)
                recvdMessages.Enqueue(message);
        }

        private Dictionary<string, ICommand> GetDictCommands(List<ICommand> commands)
        {
            return commands
                .Aggregate(
                    new Dictionary<string, ICommand>(),
                    (res, elem) =>
                    {
                        res.Add(elem.Command, elem);
                        return res;
                 });
        }
        
        public IMessage ProcessMessage(Message message)
        {
            var clientChatId = message.Chat.Id;
            var text = message.Text;
            var hasClient = clientManager.TryGetClientById(clientChatId, out var client);
            var defaultCommand = new UnknownCommand();

            if (!hasClient)
            {
                client = new Client(clientChatId);
                clientManager.AddClient(client);
                clientsCommands.Add(client, defaultCommand);
            }

            var currCommand = clientsCommands[client];

            if (Commands.ContainsKey(text))
                currCommand = Commands[text];

            var commandResult = currCommand.GetResponse(text);
            var response = commandResult.Response;
            clientsCommands[client] = commandResult.NextCommand;

            return response;
        }

        public async void SetCommands(Dictionary<string, ICommand> commands)
        {
            await bot.SetMyCommandsAsync(commands
                .Select(pair => new BotCommand
                {
                    Command = pair.Key,
                    Description = pair.Value.Description
                }));
        }
    }
}
