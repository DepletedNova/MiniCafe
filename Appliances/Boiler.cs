using MiniCafe.Appliances.Spills;
using MiniCafe.Coffee.Large;

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

        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder(),
            GetCItemProvider(GetCustomGameDataObject<BoiledWater>().ID, 6, 6, true, false, true, false, false, false, false),
            new CCombinesOnSelf(),
            new CRefillOnEmpty(),
            new CDisplayDuration
            {
                Process = GetCustomGameDataObject<BoilWaterProcess>().ID
            },
            new CTakesDuration
            {
                Total = 2f,
                Mode = InteractionMode.Items,
            }
        };

        public override void OnRegister(Appliance appliance)
        {
            Prefab.AddComponent<HoldPointContainer>().HoldPoint = Prefab.transform.Find("HoldPoint");

            var counter = Prefab.GetChild("Counter");
            counter.ApplyMaterialCafe("Wood - Default", "Wood 4 - Painted", "Wood 4 - Painted");
            counter.ApplyMaterialToChildCafe("Handle", "Knob");
            counter.ApplyMaterialToChildCafe("Countertop", "Wood - Default");

            var boiler = Prefab.GetChild("Boiler");
            boiler.ApplyMaterialToChildCafe("Dispenser", "Metal", "Plastic - Black", "Metal Black");
            boiler.ApplyMaterialToChildCafe("Pipe", "Metal Very Dark");
            boiler.ApplyMaterialToChildCafe("Boiler", "Metal- Shiny Blue");
            boiler.ApplyMaterialToChildCafe("Heat", "Metal", "Hob Black");

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
