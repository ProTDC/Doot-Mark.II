using DSharpPlus.CommandsNext.Attributes;
using DSharpPlus.CommandsNext;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.SlashCommands;
using DSharpPlus.Entities;
using DSharpPlus;

namespace Doot_Mark.II.SlashCommands
{
    public class OljefondetSL : ApplicationCommandModule
    {
        static readonly HttpClient httpClient = new HttpClient();

        [SlashCommand("Oljefondet", "Displays the markedvalue for Oljefondet")]
        public async Task OljefondetSlashCommand(InteractionContext ctx)
        {
            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                .WithContent("It worked!"));

            await ctx.Channel.TriggerTypingAsync();

            HttpResponseMessage Response = await httpClient.GetAsync($"https://www.nbim.no/LiveNavHandler/Current.ashx?l=en-GB&t=1657634463553&PreviousNavValue=12005458800555&key=263c30dd-d5ba-41d6-a9b1-c1fb59cf30da");
            var Content = await Response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(Content);
            Console.WriteLine(Response.StatusCode);

            await ctx.Channel.SendMessageAsync($"Oljefondets markedsverdi er {json["Value"].ToString()}kr");
        }
    }
}
