﻿using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using MiniCafe.Components;
using MiniCafe.Items;
using MiniCafe.Processes;
using MiniCafe.Views;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.KitchenPropertiesUtils;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Appliances
{
    // Appliance
    public class MugCabinet : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Mug Cabinet");
        public override string UniqueNameID => "mug_provider";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Mugs", "Provides both large and small mugs", new(), new()))
        };
        public override bool IsPurchasable => true;
        public override PriceTier PriceTier => PriceTier.Cheap;
        public override RarityTier RarityTier => RarityTier.Common;
        public override ShoppingTags ShoppingTags => ShoppingTags.Misc | ShoppingTags.Basic;
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

        public override List<Appliance> Upgrades => new()
        {
            GetCastedGDO<Appliance, SmallMugHolder>(),
            GetCastedGDO<Appliance, BigMugHolder>()
        };

        public override List<IApplianceProperty> Properties => new()
        {
            GetLimitedCItemProvider(SmallMug.ItemID, 4, 4),
            new CDualLimitedProvider()
            {
                Current = 1,
                Provide1 = SmallMug.ItemID,
                Available1 = 4,
                Maximum1 = 4,
                Provide2 = BigMug.ItemID,
                Available2 = 4,
                Maximum2 = 4,
            }
        };

        public override void OnRegister(Appliance gdo)
        {
            var cabinet = Prefab.GetChild("Cabinet");
            var bigMugs = cabinet.GetChild("Large Mugs");
            var smallMugs = cabinet.GetChild("Small Mugs");

            // View
            var sourceView = Prefab.TryAddComponent<DualLimitedSourceView>();
            sourceView.Animator = Prefab.GetComponent<Animator>();
            if (!Main.PaperPlatesInstalled)
            {
                for (int i = 0; i < smallMugs.GetChildCount(); i++)
                    sourceView.Items1.Add(smallMugs.GetChild(i).gameObject);

                for (int i = 0; i < bigMugs.GetChildCount(); i++)
                    sourceView.Items2.Add(bigMugs.GetChild(i).gameObject);
            }
            else
            {
                gdo.Properties = new()
                {
                    GetLimitedCItemProvider(SmallMug.ItemID, 0, 0),
                    new CDualLimitedProvider()
                    {
                        Current = 1,
                        Provide1 = SmallMug.ItemID,
                        Available1 = int.MaxValue / 2,
                        Maximum1 = int.MaxValue,
                        Provide2 = BigMug.ItemID,
                        Available2 = int.MaxValue / 2,
                        Maximum2 = int.MaxValue,
                    }
                };
            }

            // Materials
            GameObject parent = Prefab.GetChild("Block/Counter2");
            var paintedWood = GetMaterialArray("Wood 4 - Painted");
            var defaultWood = GetMaterialArray("Wood - Default");
            parent.ApplyMaterialToChild("Counter", paintedWood);
            parent.ApplyMaterialToChild("Counter Doors", paintedWood);
            parent.ApplyMaterialToChild("Counter Surface", defaultWood);
            parent.ApplyMaterialToChild("Counter Top", defaultWood);
            parent.ApplyMaterialToChild("Handles", "Knob");

            for (int i = 0; i < smallMugs.GetChildCount(); i++)
                SmallMug.ApplyMugMaterials(smallMugs.GetChild(i));
            for (int i = 0; i < bigMugs.GetChildCount(); i++)
                BigMug.ApplyMugMaterials(bigMugs.GetChild(i));

            cabinet.ApplyMaterialToChild("cabinet", "Wood");
            cabinet.ApplyMaterialToChild("door", "Door Glass", "Wood", "Metal");
        }
    }
}
