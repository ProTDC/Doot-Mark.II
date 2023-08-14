using DSharpPlus.Entities;
using DSharpPlus;
using DSharpPlus.SlashCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Net.Http;

namespace Doot_Mark.II.SlashCommands
{
    public class WeatherSL : ApplicationCommandModule
    {
        static readonly HttpClient httpClient = new HttpClient();

        [SlashCommand("Weather", "Displays the Weather in a City")]
        public async Task WeatherSlashCommand(InteractionContext ctx, [Option("City", "Type inn a City")] string message)
        {

            await ctx.CreateResponseAsync(InteractionResponseType.ChannelMessageWithSource, new DiscordInteractionResponseBuilder()
                .WithContent($"Found City {message}"));

            var api_key = API_Keys.weatherKey;
            var city = message;
            HttpResponseMessage response = await httpClient.GetAsync($"http://api.openweathermap.org/data/2.5/weather?q={city}&appid={api_key}&units=metric");
            var content = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(content);

            if (response.IsSuccessStatusCode == false)
            {
                await ctx.Channel.SendMessageAsync("Please provide a valid city");
                return;
            }
            else
            {
                var embed = new DiscordEmbedBuilder()
                {
                    Title = "Current weather in " + json["name"].ToString(),
                    Description = "Country: " + json["sys"]["country"].ToString()
                };
                embed.AddField("Weather: " + json["weather"][0]["main"].ToString(), json["weather"][0]["description"].ToString());
                embed.AddField("Temperature", json["main"]["temp"].ToString() + "℃");
                embed.AddField("Wind", json["wind"]["speed"].ToString() + " m/s");
                embed.AddField("Humidity", json["main"]["humidity"].ToString() + "%");

                Console.WriteLine(json);
                await ctx.Channel.SendMessageAsync(embed);
                return;
            }
        }
    }
}
