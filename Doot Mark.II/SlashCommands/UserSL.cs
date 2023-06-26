using DSharpPlus;
using DSharpPlus.Entities;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doot_Mark.II.SlashCommands
{
    public class UserSL : ApplicationCommandModule
    {
        [SlashCommand("Doot", "Doot")]
        public async Task TestSlashCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                .WithContent("Doot"));

            await ctx.Channel.SendMessageAsync("Doot!");
        }

        [SlashCommand("User", "User Information")]
        public async Task UserSlashCommand(InteractionContext ctx, [Option("User", "Type inn a User")] DiscordUser user)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                .WithContent("Found User"));

            DiscordGuild guild = ctx.Guild;
            var member = await guild.GetMemberAsync(user.Id);

            var embedMessage = new DiscordEmbedBuilder
            {
                Title = member.DisplayName,
                Color = member.Color,

                Thumbnail = new DiscordEmbedBuilder.EmbedThumbnail
                {
                    Url = member.AvatarUrl
                }
            };

            //Roles
            var roles = member.Roles;
            string role = string.Empty;
            if (roles.Count() == 0)
            {
                embedMessage.AddField("Roles", "No roles");
            }
            else
            {
                foreach (var userRole in roles)
                {
                    role += userRole.Mention + " ";
                }

                embedMessage.AddField("Roles", role);
            }

            //Account Creation Dates
            embedMessage.AddField("Account Created", member.CreationTimestamp.DateTime.ToLongDateString());
            embedMessage.AddField("Joined Server", member.Guild.JoinedAt.DateTime.ToLongDateString());


            embedMessage.AddField("Created", user.CreationTimestamp.ToString());

            await ctx.Channel.SendMessageAsync(embedMessage);
        }
    }
}
