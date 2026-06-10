using BaseLib.Abstracts;
using BaseLib.Extensions;
using HSR_Ancients_STS2.HSR_Ancients_STS2Code.Extensions;
using MegaCrit.Sts2.Core.Entities.Enchantments;

namespace HSR_Ancients_STS2.HSR_Ancients_STS2Code.Enchantments
{
    public abstract class HSR_Ancients_STS2Enchatment : CustomEnchantmentModel
    {
        //Loads from HSR_Ancients_STS2/images/enchantments/your_enchantment.png
        //public override string IconPath => $"{Id.Entry.RemovePrefix().ToLowerInvariant()}.png".EnchantmentImagePath();
    }
}