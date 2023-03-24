namespace MiniCafe.Appliances
{
    public class SteamerMachine : CustomAppliance
    {
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Steamer Machine");
        public override string UniqueNameID => "steamer_machine";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Steamer Machine", "A one-trick pony", new()
            {
                new()
                {
                    Title = "<sprite name=\"upgrade\" color=#A8FF1E> Steamy",
                    Description = "Performs <sprite name=\"steam_0\"> 125% faster but does not perform <sprite name=\"fill_coffee\">"
                }
            }, new()))
        };
        public override List<Appliance> Upgrades => new()
        {
            GetCastedGDO<Appliance, BaristaMachine>()
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
                Process = GetCastedGDO<Process, SteamProcess>(),
                Speed = 2.25f,
                Validity = ProcessValidity.Generic
            }
        };
        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder()
        };

        public override void OnRegister(Appliance gdo)
        {
            Prefab.AddComponent<HoldPointContainer>().HoldPoint = Prefab.transform.Find("HoldPoint");

            var counter = Prefab.GetChild("Counter");
            counter.ApplyMaterialCafe("Wood - Default", "Wood 4 - Painted", "Wood 4 - Painted");
            counter.ApplyMaterialToChildCafe("Handle", "Knob");
            counter.ApplyMaterialToChildCafe("Countertop", "Wood - Default");

            Prefab.ApplyMaterialToChildCafe("Machine", "Metal- Shiny", "Metal Very Dark", "Metal Black", "Hob Black");
        }
    }
}
