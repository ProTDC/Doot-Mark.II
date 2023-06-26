using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doot_Mark.II.Commands
{
    public class UserCommands : BaseCommandModule
    {
        [Command("prefixuser")]
        public async Task UserInfoCommand(CommandContext ctx, DiscordMember member)
        {
            await ctx.TriggerTypingAsync();

            //Build Embed
            var embed = new DiscordEmbedBuilder
            {
                Title = $"{member.DisplayName} ({member.Username})",
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
                embed.AddField("Roles", "No roles");
            }
            else
            {
                foreach (var userRole in roles)
                {
                    role += userRole.Mention + " ";
                }

                embed.AddField("Roles", role);
            }

            //Account Creation Dates
            embed.AddField("Account Created", member.CreationTimestamp.DateTime.ToLongDateString());
            embed.AddField("Joined Server", member.Guild.JoinedAt.DateTime.ToLongDateString());

            await ctx.RespondAsync(embed);
        }
    }
}
