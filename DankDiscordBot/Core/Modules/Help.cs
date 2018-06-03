using System;
using System.Text;
using System.Threading.Tasks;
using System.Collections.Generic;

using Discord;
using Discord.Commands;
using System.Reflection;
using System.IO;

namespace DankDiscordBot.Core.Modules
{
   public class Help : ModuleBase<SocketCommandContext>
    {
        [Command("help")]
        public async Task Halp()
        { 
        
        var id = Context.User.Username;
        var eb = new EmbedBuilder()
        {
            Color = new Color(4, 97, 247),
            Title = $"Hello {id} :wave:",
            Description =
                    $"This is a list of the commands available"
        };

            eb.AddField((x) =>
            {
                x.Name = ".say [message]";
                x.IsInline = false;
                x.Value =
                    "Echos a message";
            });
            eb.AddField((x) =>
            {
                x.Name = ".invite";
                x.IsInline = false;
                x.Value =
                    "Sends an invite link to the bot";
            });
            eb.AddField((x) =>
            {
                x.Name = ".8ball [Question]";
                x.IsInline = false;
                x.Value =
                    "Ask the 8ball a question";
            });
            eb.AddField((x) =>
            {
                x.Name = ".pick [option1|option2]";
                x.IsInline = false;
                x.Value =
                    "Picks one option";
            });
            eb.AddField((x) =>
            {
                x.Name = ".roll";
                x.IsInline = false;
                x.Value =
                    "Rolls the dice 1-7";
            });
            eb.AddField((x) =>
            {
                x.Name = ".draw";
                x.IsInline = false;
                x.Value =
                    "Draws a random card";
            });
            eb.AddField((x) =>
            {
                x.Name = ".source";
                x.IsInline = false;
                x.Value =
                    "Sends link to the source code";
            });
            eb.AddField((x) =>
            {
                x.Name = ".owner";
                x.IsInline = false;
                x.Value =
                    "Displays the owner";
            });
            eb.AddField((x) =>
            {
                x.Name = ".modhelp";
                x.IsInline = false;
                x.Value =
                    "List of commands available for Moderator";
            });

            await Context.Channel.SendMessageAsync("", false, eb);
        }

        [Command("modhelp")]
        public async Task modHalp()
        {

            var id = Context.User.Username;
            var eb = new EmbedBuilder()
            {
                Color = new Color(4, 97, 247),
                Title = $"Hello @{id} :wave:",
                Description =
                        $"This is a list of the mod" +
                        $" commands available"
            };
            eb.AddField((x) =>
            {
                x.Name = ".kick [@user]";
                x.IsInline = false;
                x.Value =
                    "Kicks user mentioned";
            });
            eb.AddField((x) =>
            {
                x.Name = ".ban [@user]";
                x.IsInline = false;
                x.Value =
                    "Bans user mentioned";
            });
            eb.AddField((x) =>
            {
                x.Name = ".delete [amount]";
                x.IsInline = false;
                x.Value =
                    "Deletes X amount of messages";
            });

            await Context.Channel.SendMessageAsync("", false, eb);
        }
    }
}
