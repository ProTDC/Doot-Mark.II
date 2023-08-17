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
    public class FunSL : ApplicationCommandModule
    {
        [SlashCommand("Coinflip", "Flips a coin")]
        public async Task CoinSlashCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                .WithContent("okay!"));

            Random random = new Random();
            int rInt = random.Next(0, 2);

            if (rInt == 0)
            {
                await ctx.Channel.SendMessageAsync("Heads").ConfigureAwait(false);
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Tails").ConfigureAwait(false);
            }
        }
    }
}
