﻿using DSharpPlus.CommandsNext;
using DSharpPlus.CommandsNext.Converters;
using DSharpPlus.CommandsNext.Entities;
using DSharpPlus.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Doot_Mark.II
{
    public class HelpFormatter : DefaultHelpFormatter
    {
        public HelpFormatter(CommandContext ctx) : base(ctx) { }

        public override CommandHelpMessage Build()
        {
            EmbedBuilder.Color = DiscordColor.DarkButNotBlack;
            EmbedBuilder.ImageUrl = "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTXSXaT2sNW2XGDEnvz5vEkJ5vD27XYpxb_Y9l96gmFxUH7ZbgwFhQsMm6_jlgr4kFifuo&usqp=CAU";

            return base.Build();
        }
    }
}
