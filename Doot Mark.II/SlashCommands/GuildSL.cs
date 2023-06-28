using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics.Metrics;

namespace Doot_Mark.II.SlashCommands
{
    public class GuildSL : ApplicationCommandModule
    {
        [SlashCommand("Server", "Displays information about the current Server")]
        public async Task ServerSlashCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                .WithContent("okay!"));

            var embedMessage = new DiscordEmbedBuilder
            {
                Title = $"{ctx.Guild.Name}",

                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                {
                    Url = ctx.Guild.IconUrl
                }
            };

            embedMessage.AddField("Members", ctx.Guild.MemberCount.ToString());

            embedMessage.AddField("Created", ctx.Guild.CreationTimestamp.DateTime.ToLongDateString());

            await ctx.Channel.SendMessageAsync(embedMessage);
        }
    }
}
