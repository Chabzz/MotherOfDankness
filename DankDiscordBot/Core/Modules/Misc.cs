using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Reflection;

using Discord;
using Discord.Commands;
using System.IO;

namespace DankDiscordBot.Core.Modules
{
    public class Misc : ModuleBase<SocketCommandContext>
    {
        [Command("say"), Summary("Echos a message")]
        public async Task Say([Remainder, Summary("The text to echo")] string echo)
        {
            //ReplyAsync is a mtheod on ModuleBase<SocketCommandContext>
            await ReplyAsync(echo);
        }

        [Command("invite"), Summary("Gives an invite link to invite Sora to your own Guild!")]
        [Alias("inv")]
        public async Task InviteAsync()
        {
            var eb = new EmbedBuilder()
            {
                Title = "Invite MotherOfDankness",
                Color = new Color(4, 97, 247),
                Footer = new EmbedFooterBuilder()
                {
                    Text = $"Requested by {Context.User.Username}#{Context.User.Discriminator}",
                    IconUrl = (Context.User.GetAvatarUrl())
                },
                Description =
                    "Just uncheck the permissions you dont feel like giving, this might break me though. At least give me these permissions:\n" +
                    "Read/Send Messages, Embed Links, Attach Files, Send Embeds, Add Reactions, Read Message History\n" +
                    "Manage messages, Kick members, Ban members are needed to use the Moderator commands!\n" +
                    "[Click to Invite](https://discordapp.com/oauth2/authorize?client_id=405077025867563008&scope=bot&permissions=2146958463)"
            };

            await ReplyAsync("", false, eb);
        }

        [Command("log")]
        public async Task commandLog()
        {
            Console.WriteLine("Log = Success");
        }

        [Command("8ball")]
        public async Task Eightball([Remainder]string message)
        {
           string[] EightBall = new string[]
            {
                "It is certain.",
                "Without a doubt.",
                "Yes, definetly.",
                "Yes.",
                "Reply hazy try again.",
                "?",
                "Bitch, I might be.",
                "idk",
                "No.",
                "Fuck no.",
                "Very doubtful.",
                "My sources say no."
            };

            Random r = new Random();
            string selection = EightBall[r.Next(0, EightBall.Length)];

            await Context.Channel.SendMessageAsync($"{selection}");
        }

        [Command("pick")]
        public async Task PickOne([Remainder]string message)
        {
            string[] options = message.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            Random r = new Random();
            string selection = options[r.Next(0, options.Length)];

            await Context.Channel.SendMessageAsync($"{selection}");
  
        }

        [Command("source")]
        public async Task Source()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(4, 97, 247);
            Embed.WithTitle("For y'all programmers or plagiarizers :wink: ");
            Embed.WithDescription("https://github.com/Chabzz");
            Embed.WithThumbnailUrl("https://avatars0.githubusercontent.com/u/9919?s=280&v=4");

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        [Command("owner")]
        public async Task Owner()
        {
            EmbedBuilder Embed = new EmbedBuilder();
            Embed.WithColor(4, 97, 247);
            Embed.WithTitle("My owner is @Chabz#8815");
            Embed.WithDescription("He is a very nice man");
            Embed.WithUrl("https://steamcommunity.com/id/Chabz_/");
            Embed.WithThumbnailUrl("https://steamcdn-a.akamaihd.net/steamcommunity/public/images/avatars/f1/f122b5c82e025c2ea83d20e1aadd90c2471a945a_full.jpg");

            await Context.Channel.SendMessageAsync("", false, Embed.Build());
        }

        //gamble
        [Command("roll")]
        public async Task Roll()
        {
            var r = new Random();
            var files = Directory.GetFiles(@"C:\Users\SJFH\Desktop\Coding\DiscordBot\DankDiscordBot\DankDiscordBot\Resources\Dice");
            var dice = files[r.Next(0, files.Length)];

            await Context.Channel.SendMessageAsync("The dice rolled");
            await Context.Channel.SendFileAsync(dice);
        }

        [Command("draw")]
        public async Task Draw()
        {
            var r = new Random();
            var files = Directory.GetFiles(@"C:\Users\SJFH\Desktop\Coding\DiscordBot\DankDiscordBot\DankDiscordBot\Resources\Cards");
            var card = files[r.Next(0, files.Length)];

            await Context.Channel.SendMessageAsync("The card drawn is");
            await Context.Channel.SendFileAsync(card);
        }


    }
}
