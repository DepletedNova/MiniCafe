using ApplianceLib.Api;
using IngredientLib.Ingredient.Items;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using MiniCafe.Appliances.Spills;
using MiniCafe.Components;
using System.Collections.Generic;
using UnityEngine;
using MiniCafe.Processes;
using MiniCafe.Items;
using MiniCafe.Views;
using MiniCafe.Appliances;
using static KitchenLib.Utils.MaterialUtils;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.KitchenPropertiesUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Appliances
{
    public class Boiler : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Boiler");
        public override string UniqueNameID => "boiler";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Boiler", "Provides hot water!", new(), new()))
        };
        public override bool IsPurchasable => true;
        public override PriceTier PriceTier => PriceTier.Cheap;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking;
        public override bool SellOnlyAsDuplicate => true;

        public override List<Appliance.ApplianceProcesses> Processes => new()
        {
            new()
            {
                Process = GetCastedGDO<Process, BoilWaterProcess>()
            }
        };

        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder(),
            GetCItemProvider(GetCustomGameDataObject<BoiledWater>().ID, 4, 4, true, false, true, false, false, false, false),
            new CCombinesOnSelf(),
            new CRefillOnEmpty()
            {
                MinimumProvided = 1,
                FillIncrement = 2
            },
            new CDisplayDuration
            {
                Process = GetCustomGameDataObject<BoilWaterProcess>().ID
            },
            new CTakesDuration
            {
                Total = 4f,
                Mode = InteractionMode.Items,
            },
            new CItemHolderPreventTransfer(),
            new CItemProviderPreventTransfer(),
            new CPreventTransferDuringDuration()
        };

        public override void OnRegister(Appliance appliance)
        {
            Prefab.AddComponent<HoldPointContainer>().HoldPoint = Prefab.transform.Find("HoldPoint");

            var counter = Prefab.GetChild("Counter");
            counter.ApplyMaterial("Wood - Default", "Wood 4 - Painted", "Wood 4 - Painted");
            counter.ApplyMaterialToChild("Handle", "Knob");
            counter.ApplyMaterialToChild("Countertop", "Wood - Default");

            var boiler = Prefab.GetChild("Boiler");
            boiler.ApplyMaterialToChild("Dispenser", "Metal", "Plastic - Black", "Metal Black");
            boiler.ApplyMaterialToChild("Pipe", "Metal Very Dark");
            boiler.ApplyMaterialToChild("Boiler", "Metal- Shiny Blue");
            boiler.ApplyMaterialToChild("Heat", "Metal", "Hob Black");

            Prefab.ApplyMaterialToChild("Empty", "Milk Glass");
            var water = Prefab.GetChild("Water");
            water.ApplyMaterialToChildren("Water", "Milk Glass", "Water");
            water.ApplyMaterialToChild("Water 5", "Water", "Milk Glass");
            water.ApplyMaterialToChild("Water 6", "Water", "Milk Glass");
            water.ApplyMaterialToChild("Full", "Water");

            var items = new List<GameObject>();
            for (int i = 0; i < water.GetChildCount(); i++)
            {
                items.Add(water.GetChild(i));
            }

            var limitedSource = Prefab.TryAddComponent<LimitedItemSourceView>();
            ReflectionUtils.GetField<LimitedItemSourceView>("Empty").SetValue(limitedSource, Prefab.GetChild("Empty"));
            ReflectionUtils.GetField<LimitedItemSourceView>("FixedType").SetValue(limitedSource, true);
            ReflectionUtils.GetField<LimitedItemSourceView>("OnlyShowOne").SetValue(limitedSource, true);
            ReflectionUtils.GetField<LimitedItemSourceView>("Items").SetValue(limitedSource, items);
        }
    }
}
