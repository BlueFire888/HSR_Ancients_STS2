using BaseLib.Utils;
using HSR_Ancients_STS2.HSR_Ancients_STS2Code.Cards;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.DevConsole.ConsoleCommands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.Entities.Rngs;
using MegaCrit.Sts2.Core.Factories;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Logging;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.RelicPools;
using MegaCrit.Sts2.Core.Nodes.Relics;
using MegaCrit.Sts2.Core.Random;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
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
                    case RelicRarity.Event:
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
            IEnumerable <RelicModel> returnList = player.Relics.Where<RelicModel>((Func<RelicModel, bool>)(r => IsSwapable(r)));
            foreach (RelicModel returnModel in returnList)
            {
                MainFile.Logger.Info(returnModel.ToString());
            }
            return returnList;
        }
        public override async Task AfterObtained()
        {
            foreach (RelicModel relic in GetValidRelics(Owner).ToList<RelicModel>())
            {

                await RelicCmd.Remove(relic);
                await RelicCmd.Obtain((RelicFactory.PullNextRelicFromFront(Owner)).ToMutable(), Owner);

            }
        }



    }
}
