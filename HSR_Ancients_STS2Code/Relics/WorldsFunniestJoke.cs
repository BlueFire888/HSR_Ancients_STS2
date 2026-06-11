using BaseLib.Utils;
using HSR_Ancients_STS2.HSR_Ancients_STS2Code.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.RelicPools;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSR_Ancients_STS2.HSR_Ancients_STS2Code.Relics
{


[Pool(typeof(EventRelicPool))]
    public sealed class WorldsFunniestJoke : HSR_Ancients_STS2Relic
    {
        public override RelicRarity Rarity => RelicRarity.Ancient;
        public override bool HasUponPickupEffect => true;

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get
            {
                List<IHoverTip> items = new List<IHoverTip>();
                items.Add(HoverTipFactory.ForEnergy((RelicModel)this));
                items.AddRange(HoverTipFactory.FromCardWithCardHoverTips<HideousLaughter>());
                return (IEnumerable<IHoverTip>) items;
            }
        }
        protected override IEnumerable<DynamicVar> CanonicalVars
        {
            get
            {
                return new[] { (DynamicVar)new EnergyVar(1) };
            }
        }
        public override async Task AfterObtained()
        {
            CardModel deck = await CardPileCmd.AddCurseToDeck<HideousLaughter>(this.Owner);
        }

        public override Decimal ModifyMaxEnergy(Player player, Decimal amount)
        {
            return player != this.Owner ? amount : amount + this.DynamicVars.Energy.BaseValue;
        }
    }
}
