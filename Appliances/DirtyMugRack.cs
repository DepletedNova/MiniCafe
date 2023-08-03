using ApplianceLib.Api;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Registry;
using KitchenLib.Utils;
using MiniCafe.Processes;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Appliances
{
    public class DirtyMugRack : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Dirty Mug Rack");
        public override string UniqueNameID => "mug_rack";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Dirty Mug Rack", "Put them here and wait for someone else to do it", new() 
                { new() { Title = "Dirty Storage", Description = "Stores up to 4 dirty mugs" } }, new()))
        };
        public override bool IsPurchasable => true;
        public override List<Appliance.ApplianceProcesses> Processes => new()
        {
            new()
            {
                Process = GetCastedGDO<Process, RequiresMugProcess>()
            }
        };
        public override List<Process> RequiresProcessForShop => new()
        {
            GetCastedGDO<Process, RequiresMugProcess>()
        };
        public override PriceTier PriceTier => PriceTier.Cheap;
        public override RarityTier RarityTier => RarityTier.Common;
        public override ShoppingTags ShoppingTags => ShoppingTags.Plumbing;
        public override List<IApplianceProperty> Properties => new()
        {
            new CFlexibleContainer()
            {
                Maximum = 4,
            },
            new CRestrictedReceiver()
            {
                ApplianceKey = Main.DirtyMugKey
            }
        };

        public override void Convert(GameData gameData, out GameDataObject gdo)
        {
            base.Convert(gameData, out gdo);
            if (ModRegistery.Registered.Any(modPair => modPair.Value.ModID == "paperPlates"))
                (gdo as Appliance).IsPurchasable = false;
        }

        public override void OnRegister(Appliance gdo)
        {
            var parent = Prefab.GetChild("Block/Counter2");
            var paintedWood = GetMaterialArray("Wood 4 - Painted");
            var defaultWood = GetMaterialArray("Wood - Default");
            parent.ApplyMaterialToChild("Counter", paintedWood);
            parent.ApplyMaterialToChild("Counter Doors", paintedWood);
            parent.ApplyMaterialToChild("Counter Surface", defaultWood);
            parent.ApplyMaterialToChild("Counter Top", defaultWood);
            parent.ApplyMaterialToChild("Handles", "Knob");

            var rack = Prefab.GetChild("Rack");
            rack.ApplyMaterialToChild("Rack", "Plastic");

            List<GameObject> items = new();

            for (int i = 0; i < rack.GetChildCount(); i++)
            {
                var mug = rack.GetChild(i);
                if (!mug.name.ToLower().Contains("mug"))
                    continue;
                items.Add(mug);
            }

            var flexibleView = Prefab.AddComponent<FlexibleContainerView>();
            flexibleView.Items = items;
        }
    }
}
