using BaseLib.Utils;
using HSR_Ancients_STS2.HSR_Ancients_STS2Code.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Models.Relics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSR_Ancients_STS2.HSR_Ancients_STS2Code.Relics
{



[Pool(typeof(EventRelicPool))]
    public sealed class SparklingPlushie : HSR_Ancients_STS2Relic
    {
        public override RelicRarity Rarity => RelicRarity.Ancient;
        public override bool HasUponPickupEffect => true;

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get
            {
                return HoverTipFactory.FromCardWithCardHoverTips<BigRedButton>();
            }
        
        }
        public override async Task AfterObtained()
        {
            CardCmd.PreviewCardPileAdd((IReadOnlyList<CardPileAddResult>)[(await CardPileCmd.Add((CardModel)Owner.RunState.CreateCard<BigRedButton>(Owner), PileType.Deck))], 2f);
        }
    }
}
