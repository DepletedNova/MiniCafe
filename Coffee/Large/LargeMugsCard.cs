using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.MaterialUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Coffee.Large
{
    public class LargeMugsCard : CustomUnlockCard
    {
        public override string UniqueNameID => "large_mugs_card";
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Large;
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Large Mugs", "Automatically adds larger mug cards for every coffee variant", "That's a lot of coffee!"))
        };
        public override List<UnlockEffect> Effects => new()
        {
            new StatusEffect
            {
                Status = Main.LARGE_MUG_STATUS
            }
        };
        public override List<Unlock> HardcodedRequirements => new() { GetGDO<Unlock>(CoffeeBaseDish) };

        public override void AttachDependentProperties(GameData GD, GameDataObject GDO)
        {
            base.AttachDependentProperties(GD, GDO);
            LargeDishRegistry.ID = GDO.ID;
        }
    }
}
