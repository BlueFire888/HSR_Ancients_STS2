using BaseLib.Utils;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Potions;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HSR_Ancients_STS2.HSR_Ancients_STS2Code.Cards
{
    [Pool(typeof(EventCardPool))]
    public sealed class BigRedButton : HSR_Ancients_STS2Card
    {
        public BigRedButton() : base(2, CardType.Attack, CardRarity.Ancient, TargetType.TargetedNoCreature)
        {
        }
        public override IEnumerable<CardKeyword> CanonicalKeywords
        {
            get
            {
                return (IEnumerable<CardKeyword>)new[] { CardKeyword.Exhaust };
            }
        }
        protected override IEnumerable<DamageVar> CanonicalVars
        {
            get
            {
                return new[] { (DamageVar)new DamageVar(30,ValueProp.Move) };
            }
        }
        
        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)  
        {
            BigRedButton bigRedButton = this;
            Creature creature = bigRedButton.Owner.Creature;
            DamageVar damage = bigRedButton.DynamicVars.Damage;
            IEnumerable<DamageResult> damageResults = await CreatureCmd.Damage(choiceContext,  bigRedButton.Owner.Creature.CombatState.Creatures.Where<Creature>((Func<Creature, bool>)(c => !c.IsPet)), damage.BaseValue, damage.Props, creature, (CardModel)null);
            //AttackCommand attackCommand = await DamageCmd.Attack(bigRedButton.DynamicVars.Damage.BaseValue).FromCard((CardModel)bigRedButton).TargetingAllOpponents(bigRedButton.CombatState).WithHitFx("vfx/vfx_attack_blunt", tmpSfx: "blunt_attack.mp3").Execute(choiceContext);
        }
        
        protected override void OnUpgrade() => this.DynamicVars.Damage.UpgradeValueBy(10M);
    }
}
