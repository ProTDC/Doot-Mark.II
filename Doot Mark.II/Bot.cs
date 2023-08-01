using Doot_Mark.II.Commands;
using Doot_Mark.II.SlashCommands;
using DSharpPlus;
using DSharpPlus.CommandsNext;
using DSharpPlus.Entities;
using DSharpPlus.EventArgs;
using DSharpPlus.Interactivity;
using DSharpPlus.Interactivity.Extensions;
using DSharpPlus.SlashCommands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Doot_Mark.II
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }  
        public InteractivityExtension Interactivity { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        public async Task RunAsync()
        {
            var json = string.Empty;
            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
                json = await sr.ReadToEndAsync();

            var configJson = JsonConvert.DeserializeObject<ConfigJson>(json);

            var config = new DiscordConfiguration()
            {
                Intents = DiscordIntents.All,
                Token = configJson.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true
            };

            Client = new DiscordClient(config);
            Client.UseInteractivity(new InteractivityConfiguration()
            {
                Timeout = TimeSpan.FromMinutes(2)
            });

            var commandConfig = new CommandsNextConfiguration()
            {
                StringPrefixes = new string[] { configJson.Prefix },
                EnableMentionPrefix = true,
                EnableDms = true,
                EnableDefaultHelp = false
            };

            Commands = Client.UseCommandsNext(commandConfig);
            var slashCommandsConfig = Client.UseSlashCommands();

            //Help Formatter
            Commands.SetHelpFormatter<HelpFormatter>();

            //Prefix Commands
            Commands.RegisterCommands<UserCommands>();

            //Slash Commands
            slashCommandsConfig.RegisterCommands<UserSL>();
            slashCommandsConfig.RegisterCommands<OljefondetSL>();
            slashCommandsConfig.RegisterCommands<GuildSL>();
            slashCommandsConfig.RegisterCommands<WeatherSL>();

            //Emojis
            var flagBritish = DiscordEmoji.FromName(Client, ":flag_gb:");
            var vomit = DiscordEmoji.FromName(Client, ":face_vomiting:");
            var pensive = DiscordEmoji.FromName(Client, ":pensive:");


            Client.GuildCreated += async (s, e) =>
            {
                if (e.Guild.SystemChannel.Equals(null))
                {
                    return;
                }
                else
                {
                    await e.Guild.SystemChannel.SendMessageAsync("big mistake").ConfigureAwait(false);
                    return;
                }

            };

            Client.GuildMemberAdded += async (s, e) =>
            {
                if (e.Guild.SystemChannel.Equals(null))
                {
                    return;
                }
                else
                {
                    await e.Guild.SystemChannel.SendMessageAsync($"Hewwo {e.Member.Mention} :3").ConfigureAwait(false);
                    return;
                }
            };

            Client.GuildMemberRemoved += async (s, e) =>
            {
                if (e.Guild.SystemChannel.Equals(null))
                {
                    return;
                }
                else
                {
                    await e.Guild.SystemChannel.SendMessageAsync($"{e.Member.Mention} has left us... {pensive}").ConfigureAwait(false);
                }
            };

            Client.MessageCreated += async (s, e) =>
            {
                if (e.Message.Author.IsBot)
                    return;

                if (e.Message.Content.ToLower().Contains("doot"))
                {
                    await e.Channel.TriggerTypingAsync();
                    await e.Message.RespondAsync("Doot").ConfigureAwait(false);
                }

                if (e.Message.Content.ToLower().Contains(":3"))
                {
                    await e.Channel.TriggerTypingAsync();
                    await e.Channel.SendMessageAsync(":3").ConfigureAwait(false);
                }

                if (e.Message.Content.ToLower().Contains("british") || e.Message.Content.ToLower().Contains("bri'ish") || e.Message.Content.ToLower().Contains("briish"))
                {
                    await e.Message.CreateReactionAsync(flagBritish).ConfigureAwait(false);
                    await e.Message.CreateReactionAsync(vomit).ConfigureAwait(false);
                }

            };

            Client.MessageDeleted += async (s, e) =>
            {
                await e.Channel.SendMessageAsync($"{e.Message.Author.Mention} just deleted a message. The message was: {e.Message.Content}");
            };

            await Client.ConnectAsync();
            await Task.Delay(-1);
        }

        private Task OnClientReady(ReadyEventArgs e)
        {
            return Task.CompletedTask;
        }
    }
}
