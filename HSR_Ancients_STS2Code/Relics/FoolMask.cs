using BaseLib.Utils;
using HSR_Ancients_STS2.HSR_Ancients_STS2Code.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.Rngs;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Nodes.Relics;
using MegaCrit.Sts2.Core.Random;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSR_Ancients_STS2.HSR_Ancients_STS2Code.Relics
{


[Pool(typeof(EventRelicPool))]
    public sealed class  FoolMask: HSR_Ancients_STS2Relic
    {
        public override RelicRarity Rarity => RelicRarity.Ancient;
        public override bool HasUponPickupEffect => true;

        private bool IsSwapable(RelicModel relic)
        {
            if (relic.SpawnsPets)
                return false;
            bool flag;
                switch (relic.Rarity)
                {
                    case RelicRarity.Starter:
                    case RelicRarity.Ancient:
                        flag = true;
                        break;
                    default:
                        flag = false;
                        break;
                }
                return !flag;
        }
        private IEnumerable<RelicModel> GetValidRelics(Player player)
        {
            return player.Relics.Where<RelicModel>((Func<RelicModel, bool>)(r => IsSwapable(r)));
        }
        public override async Task AfterObtained()
        {
            foreach (RelicModel relic in GetValidRelics(Owner).ToList<RelicModel>())
            {
                await RelicCmd.Remove(relic);
                RelicModel relicModel = await RelicCmd.Obtain(RelicFactory.PullNextRelicFromFront(this.Owner), this.Owner);
            }
        }



    }
}
