using MiniCafe.Appliances.Spills;
using MiniCafe.Coffee.Large;

namespace MiniCafe.Appliances
{
    internal class LargeCoffeeMachine : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Large Coffee Machine");
        public override string UniqueNameID => "large_coffee_machine";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Coffee Machine", "Provides large cups as well!", new(), new()))
        };
        public override bool IsPurchasable => true;
        public override PriceTier PriceTier => PriceTier.Cheap;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking;
        public override bool SellOnlyAsDuplicate => true;

        public override List<Appliance> Upgrades => new()
        {
            GetCastedGDO<Appliance, BaristaMachine>()
        };

        public override List<Appliance.ApplianceProcesses> Processes => new()
        {
            new()
            {
                IsAutomatic = true,
                Process = GetGDO<Process>(ProcessReferences.FillCoffee),
                Speed = 1f,
                Validity = ProcessValidity.Generic
            }
        };

        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder(),
            new COverfills()
            {
                ID = GetCustomGameDataObject<CoffeeSpill1>().ID
            },
            GetCItemProvider(GetCustomGameDataObject<LargeCup>().ID, 0, 0, false, false, true, false, false, false, false)
        };

        public override void OnRegister(Appliance appliance)
        {
            Prefab.AddComponent<HoldPointContainer>().HoldPoint = Prefab.transform.Find("HoldPoint");

            var machine = Prefab.GetChild("CoffeeMachine");
            machine.ApplyMaterialToChildCafe("Machine", "Metal", "Plastic - Red", "Plastic - Black");
            machine.ApplyMaterialToChildCafe("Spout", "Metal Very Dark");
            machine.ApplyMaterialToChildCafe("Steam", "Metal Black");

            var counter = Prefab.GetChild("Counter");
            counter.ApplyMaterialCafe("Wood - Default", "Wood 4 - Painted", "Wood 4 - Painted");
            counter.ApplyMaterialToChildCafe("Handle", "Knob");
            counter.ApplyMaterialToChildCafe("Countertop", "Wood - Default");

            Prefab.GetChild("Cups").ApplyMaterialToChildren("Mug", "Light Coffee Cup");
        }
    }
}
