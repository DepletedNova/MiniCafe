namespace MiniCafe.Appliances
{
    public class BaristaMachine : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Barista Machine");
        public override string UniqueNameID => "barista_machine";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Barista Machine", "Pretty good at filling coffee!", new()
            {
                new()
                {
                    Title = "<sprite name=\"upgrade\" color=#A8FF1E> Filling",
                    Description = "Performs <sprite name=\"fill_coffee\"> 50% faster and <sprite name=\"steam_0\"> 50% slower"
                }
            }, new()))
        };
        public override List<Appliance> Upgrades => new()
        {
            GetCastedGDO<Appliance, SteamerMachine>()
        };
        public override bool IsPurchasableAsUpgrade => true;
        public override PriceTier PriceTier => PriceTier.Medium;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking;
        public override List<Process> RequiresProcessForShop => new()
        {
            GetCastedGDO<Process, CuplessFillCupProcess>()
        };

        public override List<Appliance.ApplianceProcesses> Processes => new()
        {
            new()
            {
                IsAutomatic = true,
                Process = GetGDO<Process>(ProcessReferences.FillCoffee),
                Speed = 1.5f,
                Validity = ProcessValidity.Generic
            },
            new()
            {
                IsAutomatic = true,
                Process = GetCastedGDO<Process, SteamProcess>(),
                Speed = 0.5f,
                Validity = ProcessValidity.Generic
            }
        };
        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder()
        };

        public override void OnRegister(Appliance gdo)
        {
            var machine = Prefab.GetChild("CoffeeMachine");
            machine.ApplyMaterialToChildCafe("Machine", "Plastic - Dark Grey", "Metal Black", "Plastic - Grey", "Plastic");
            machine.ApplyMaterialToChildCafe("Steam", "Metal Very Dark");

            var counter = Prefab.GetChild("Counter");
            counter.ApplyMaterialCafe("Wood - Default", "Wood 4 - Painted", "Wood 4 - Painted");
            counter.ApplyMaterialToChildCafe("Handle", "Knob");
            counter.ApplyMaterialToChildCafe("Countertop", "Wood - Default");

            Prefab.AddComponent<HoldPointContainer>().HoldPoint = Prefab.transform.Find("HoldPoint");
        }
    }
}
