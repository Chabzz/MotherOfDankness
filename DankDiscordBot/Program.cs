using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

using Newtonsoft.Json;

using Discord;
using Discord.Net;
using Discord.Commands;
using Discord.WebSocket;

 

namespace DankDiscordBot
{
    public class Program : ModuleBase<SocketCommandContext>
    {
        private DiscordSocketClient Client;
        private CommandService Commands;

        static void Main(string[] args)
        => new Program().MainAsync().GetAwaiter().GetResult();

        private async Task MainAsync()
        {

            Client = new DiscordSocketClient(new DiscordSocketConfig
            {
                LogLevel = LogSeverity.Debug
            });

            //help help HELP heLp
            Commands = new CommandService(new CommandServiceConfig
            {
                CaseSensitiveCommands = true,
                DefaultRunMode = RunMode.Async,
                LogLevel = LogSeverity.Debug
            });

            Client.MessageReceived += Client_MessageReceived;
            await Commands.AddModulesAsync(Assembly.GetEntryAssembly());

            Client.Ready += Client_Ready;
            Client.Log += Client_Log;
            Client.JoinedGuild += Client_JoinedGuild;
            Client.UserJoined += AnnounceJoinedUser; ;
            Client.UserLeft += AnnounceLeftdUser;

            string Token = "123";
            await Client.LoginAsync(TokenType.Bot, Token);
            await Client.StartAsync();

            await Task.Delay(-1);
        }

        private Task Client_UserLeft(SocketGuildUser arg)
        {
            throw new NotImplementedException();
        }

        public async Task AnnounceJoinedUser(SocketGuildUser socketGuildUser) //welcomes New Players
        {
            var channel = Client.GetChannel(389671826395234308) as SocketTextChannel; //gets channel to send message in
            await channel.SendMessageAsync("Welcome " + socketGuildUser.Mention + " to the server!"); //Welcomes the new user

            ulong roleID = 376763319211524115; //Some hard-coded roleID
            var role = socketGuildUser.Guild.GetRole(roleID);
            await socketGuildUser.AddRoleAsync(role);
        }

        public async Task AnnounceLeftdUser(SocketGuildUser user) //welcomes New Players
        {
            var channel = Client.GetChannel(123456) as SocketTextChannel; //gets channel to send message in
            await channel.SendMessageAsync("Bye bye " + user.Mention + " :wave:!"); //Welcomes the new user
        }

        private async Task Client_Log(LogMessage Message)
        {
            Console.WriteLine($"{DateTime.Now} at {Message.Source}] {Message.Message}");

        }

        private async Task Client_Ready()
        {
            await Client.SetGameAsync(".help");
        }

        private async Task Client_MessageReceived(SocketMessage MessageParam)
        {
            var Message = MessageParam as SocketUserMessage;
            var Context = new SocketCommandContext(Client, Message);

            if (Context.Message == null || Context.Message.Content == "") return;
            if (Context.User.IsBot) return;

            int ArgPos = 0;
            if (!(Message.HasStringPrefix(".", ref ArgPos) || Message.HasMentionPrefix(Client.CurrentUser, ref ArgPos))) return;

            var Result = await Commands.ExecuteAsync(Context, ArgPos);
            if (!Result.IsSuccess)
                await Context.Channel.SendMessageAsync("Something went wrong in DankLand. Uknown command. Try using .help");
        }

        private async Task Client_JoinedGuild(SocketGuild guild)
        {

            var eb = new EmbedBuilder()
            {
                Color = new Color(4, 97, 247),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Bot created by Chabz#8815"
                },
                Title = $"Hello {guild.Name} :wave:",
                Description =
                    $"Thank you for inviting me. My name is Dank and I hope I can make your Guild a little bit better :)\n" +
                    $"Try using .help for a list of my commands"
            };
           /* eb.AddField((x) =>
            {
                x.Name = "Live Support and Feedback";
                x.IsInline = false;
                x.Value =
                    "If you need live support or simply want to give me feedback / bug reports or issue a Feature request head to this guild:\n[Click to Join](https://discord.gg/Pah4yj5)";
            }); */

            await guild.DefaultChannel.SendMessageAsync("", false, eb);
        }
    }
}
