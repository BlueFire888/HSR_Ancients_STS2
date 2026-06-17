using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSR_Ancients_STS2.HSR_Ancients_STS2Code.Powers
{


public sealed class RingingNextTurnPower: HSR_Ancients_STS2Power
    {
        public override PowerType Type => PowerType.Debuff;

        public override PowerStackType StackType => PowerStackType.Single;

        public override async Task AfterPlayerTurnStart(PlayerChoiceContext choiceContext, Player player)
        {
            RingingNextTurnPower ringingNextTurnPower = this;
            if (player != ringingNextTurnPower.Owner.Player || ringingNextTurnPower.AmountOnTurnStart == 0)
                return;
            PowerModel powerModel = (PowerModel)await PowerCmd.Apply<RingingPower>(choiceContext, ringingNextTurnPower.Owner, 1M, ringingNextTurnPower.Owner,null);
            await PowerCmd.Remove((PowerModel)ringingNextTurnPower);
        }
    }
}
