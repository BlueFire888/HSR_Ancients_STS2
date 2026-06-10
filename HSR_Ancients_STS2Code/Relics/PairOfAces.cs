using BaseLib.Utils;
using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Extensions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using MegaCrit.Sts2.Core.Nodes;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Vfx;
using MegaCrit.Sts2.Core.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSR_Ancients_STS2.HSR_Ancients_STS2Code.Relics
{

[Pool(typeof(EventRelicPool))]
    public sealed class PairOfAces : HSR_Ancients_STS2Relic
    {
        public override RelicRarity Rarity => RelicRarity.Ancient;

        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                return new[] { (DynamicVar)new CardsVar(2)};
            }
        }

        public override async Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
        {
            PairOfAces pairofaces = this;
            if (player != pairofaces.Owner || pairofaces.Owner.PlayerCombatState.TurnNumber != 1)
                return;
            Rng combatCardSelection = this.Owner.RunState.Rng.CombatCardSelection;
            await CardPileCmd.ShuffleIfNecessary(choiceContext, pairofaces.Owner);
            IEnumerable<CardPileAddResult> cardPileAddResultList = await CardPileCmd.Add(await CardSelectCmd.FromCombatPile(choiceContext, PileType.Draw.GetPile(pairofaces.Owner), pairofaces.Owner, new CardSelectorPrefs(pairofaces.SelectionScreenPrompt, 2)), PileType.Hand);
            CardPileAddResult card1 = player.RunState.Rng.CombatCardSelection.NextItem<CardPileAddResult>(cardPileAddResultList);
            if (card1.cardAdded != null)
            {
                foreach (CardPileAddResult cardpile in cardPileAddResultList)
                {
                    if (card1.cardAdded != cardpile.cardAdded)
                        cardpile.cardAdded.SetToFreeThisTurn();
                }
                await CardCmd.Exhaust(choiceContext, card1.cardAdded);
            }     
        }
    }
}
