using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using Godot;
using HSR_Ancients_STS2.HSR_Ancients_STS2Code.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.RestSite;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Random;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;


namespace HSR_Ancients_STS2.HSR_Ancients_STS2Code.Relics
{
    [Pool(typeof(EventRelicPool))]
public sealed class SnakeMask : HSR_Ancients_STS2Relic
    {
        public override RelicRarity Rarity => RelicRarity.Ancient;

        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                return new[] { (DynamicVar)new CardsVar(3),(new DynamicVar("Slither", 3M)) };
            }
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get => HoverTipFactory.FromEnchantment<Slither>();
        }

        public override async Task AfterObtained()
        {
            SnakeMask snakeMask= this;
            CardSelectorPrefs prefs = new CardSelectorPrefs(CardSelectorPrefs.EnchantSelectionPrompt, snakeMask.DynamicVars.Cards.IntValue);
            foreach (CardModel card in await CardSelectCmd.FromDeckForEnchantment(snakeMask.Owner, (EnchantmentModel)ModelDb.Enchantment<Slither>(), snakeMask.DynamicVars["Slither"].IntValue, prefs))
            {
                CardCmd.Enchant<Slither>(card,3M);
                NCardEnchantVfx child = NCardEnchantVfx.Create(card);
                if (child != null)
                {
                    NRun instance = NRun.Instance;
                    if (instance != null)
                        instance.GlobalUi.CardPreviewContainer.AddChildSafely((Node)child);
                }
            }
        }
    }
}