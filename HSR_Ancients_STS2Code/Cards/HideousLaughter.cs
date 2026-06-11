using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using HSR_Ancients_STS2.HSR_Ancients_STS2Code.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSR_Ancients_STS2.HSR_Ancients_STS2Code.Cards
{
    [Pool(typeof(CurseCardPool))]
    public sealed class HideousLaughter : HSR_Ancients_STS2Card
    {
        public HideousLaughter() : base(2, CardType.Curse, CardRarity.Curse, TargetType.None)
        {
        }

        public override bool CanBeGeneratedByModifiers => false;
        public override int MaxUpgradeLevel => 0;

        public override IEnumerable<CardKeyword> CanonicalKeywords
        {
            get
            {
                return (IEnumerable<CardKeyword>)new[] { CardKeyword.Eternal, CardKeyword.Ethereal};
            }
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
           get
            {
               return (IEnumerable<IHoverTip>) [HoverTipFactory.FromPower<RingingPower>()];
            }
         
        }
       
        public override bool HasTurnEndInHandEffect => true;
        protected override async Task OnTurnEndInHand(PlayerChoiceContext choiceContext)
        {
            HideousLaughter cardSource = this;
            PowerModel powerModel = (PowerModel) await PowerCmd.Apply<RingingPower>(choiceContext, cardSource.Owner.Creature, 1M, (Creature)null, (CardModel)cardSource);
            if (powerModel == null)
                return;
            powerModel.SkipNextDurationTick = true;
        }
    }
}