namespace MiniCafe.Appliances
{
    public class CuplessCoffeeMachine : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Coffee Machine");
        public override string UniqueNameID => "cupless_coffee_machine";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Coffee Machine", "Fills cups right up! Does not include cups.", new()
            {
                new()
                {
                    Title = "Cupless",
                    Description = "Does not provide cups."
                }
            }, new()))
        };
        public override bool IsPurchasable => true;
        public override PriceTier PriceTier => PriceTier.Cheap;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking;
        public override RarityTier RarityTier => RarityTier.Common;
        public override List<Process> RequiresProcessForShop => new()
        {
            GetCastedGDO<Process, CuplessFillCupProcess>()
        };
        public override List<Appliance> Upgrades => new()
        {
            GetCastedGDO<Appliance, BaristaMachine>(),
            GetCastedGDO<Appliance, SteamerMachine>()
        };
        public override List<Appliance.ApplianceProcesses> Processes => new()
        {
            new()
            {
                IsAutomatic = true,
                Process = GetGDO<Process>(ProcessReferences.FillCoffee),
                Speed = 1f,
                Validity = ProcessValidity.Generic
            },
            new()
            {
                IsAutomatic = true,
                Process = GetCastedGDO<Process, SteamProcess>(),
                Speed = 1f,
                Validity = ProcessValidity.Generic
            }
        };
        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder()
        };

        public override void OnRegister(GameDataObject gdo)
        {
            Prefab.AddComponent<HoldPointContainer>().HoldPoint = Prefab.transform.Find("HoldPoint");

            var machine = Prefab.GetChild("CoffeeMachine");
            machine.ApplyMaterialToChild("Machine", "Metal", "Plastic - Red", "Plastic - Black");
            machine.ApplyMaterialToChild("Spout", "Metal Very Dark");
            machine.ApplyMaterialToChild("Steam", "Metal Black");

            var counter = Prefab.GetChild("Counter");
            counter.ApplyMaterial("Wood - Default", "Wood 4 - Painted", "Wood 4 - Painted");
            counter.ApplyMaterialToChild("Handle", "Knob");
            counter.ApplyMaterialToChild("Countertop", "Wood - Default");
        }
    }
}
