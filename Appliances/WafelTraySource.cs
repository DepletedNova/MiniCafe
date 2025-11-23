using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using MiniCafe.Sides;
using System.Collections.Generic;
using UnityEngine;

namespace MiniCafe.Appliances
{
    public class WafelTraySource : CustomAppliance
    {
        public override string UniqueNameID => "nova.mc_wafel_tray_source";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("SW Tray Appliance");
        public override PriceTier PriceTier => PriceTier.Medium;
        public override bool SellOnlyAsDuplicate => true;
        public override bool IsPurchasable => true;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking | ShoppingTags.Misc;
        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder(),
            GetCItemProvider(GetCastedGDO<Item, EmptyWafelTray>().ID, 1, 1, false, false, true, false, false, true, false)
        };
        public override List<Appliance.ApplianceProcesses> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Speed = 0.7f,
                Validity = ProcessValidity.Generic
            },
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Knead),
                Speed = 0.75f,
                Validity = ProcessValidity.Generic
            },
        };
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.Default, LocalisationBuilder.NewBuilder<ApplianceInfo>().SetName("Wafel Tray").SetDescription("Provides a wafel tray")),
            (Locale.English, LocalisationBuilder.NewBuilder<ApplianceInfo>().SetName("Wafel Tray").SetDescription("Provides a wafel tray"))
        };

        public override void OnRegister(Appliance gameDataObject)
        {
            var counter = Prefab.GetChild("Counter");
            var paintedWood = GetMaterialArray("Wood 4 - Painted");
            var defaultWood = GetMaterialArray("Wood - Default");
            counter.ApplyMaterialToChild("Counter", paintedWood);
            counter.ApplyMaterialToChild("Counter Doors", paintedWood);
            counter.ApplyMaterialToChild("Counter Surface", defaultWood);
            counter.ApplyMaterialToChild("Counter Top", defaultWood);
            counter.ApplyMaterialToChild("Handles", "Knob");

            Prefab.ApplyMaterialToChild("HoldPoint/Empty Wafel Tray/Tray", "Metal Black - Shiny");

            var sourceView = Prefab.TryAddComponent<LimitedItemSourceView>();
            sourceView.HeldItemPosition = Prefab.TryAddComponent<HoldPointContainer>().HoldPoint = Prefab.transform.Find("HoldPoint");
            ReflectionUtils.GetField<LimitedItemSourceView>("Items").SetValue(sourceView, new List<GameObject>()
            {
                Prefab.GetChild("HoldPoint/Empty Wafel Tray")
            });

        }
    }
}
