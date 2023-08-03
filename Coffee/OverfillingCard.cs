using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.MaterialUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Coffee
{
    public class OverfillingCard : CustomUnlockCard
    {
        public override string UniqueNameID => "overfill_card";
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Imperfect Brewing", "Cups have a 50% chance to overfill!", "Gets a bit messy!"))
        };
        public override List<UnlockEffect> Effects => new()
        {
            new StatusEffect
            {
                Status = Main.OVERFILLING_STATUS
            }
        };
        public override List<Unlock> HardcodedRequirements => new()
        {
            GetGDO<Unlock>(CoffeeshopMode)
        };
    }
}
