using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Appliances.Spills
{
    public class CoffeeSpill1 : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Coffee Spill 1");
        public override string UniqueNameID => "coffee_spill_1";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Mess - Coffee 1", "", new(), new()))
        };
        public override bool IsPurchasable => false;
        public override EntryAnimation EntryAnimation => EntryAnimation.Mess;
        public override ExitAnimation ExitAnimation => ExitAnimation.MessDestroy;
        public override OccupancyLayer Layer => OccupancyLayer.Floor;
        public override bool ForceHighInteractionPriority => true;

        public override List<IApplianceProperty> Properties => new()
        {
            new CSlowPlayer()
            {
                Radius = 0.25f,
                Factor = 0.6f
            },
            new CTakesDuration()
            {
                Total = 0.6f,
                RelevantTool = DurationToolType.Clean,
                Manual = true,
                Mode = InteractionMode.Items
            },
            new CDestroyAfterDuration(),
            new CDestroyApplianceAtNight(),
            new CDisplayDuration()
            {
                Process = ProcessReferences.Clean
            },
            new CStackableMess()
            {
                BaseMess = GetCustomGameDataObject<CoffeeSpill1>().ID,
                NextMess = GetCustomGameDataObject<CoffeeSpill2>().ID
            }
        };

        public override void OnRegister(Appliance gdo)
        {
            Prefab.ApplyMaterialToChild("spill", "Coffee - Black");
        }
    }
}
