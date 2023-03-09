using System.Collections.Generic;

namespace MiniCafe.Appliances
{
    public class BaristaMachine : CustomAppliance
    {
        public override GameObject Prefab => (GetExistingGDO(ApplianceReferences.CoffeeMachine) as Appliance).Prefab;

        public override string UniqueNameID => "barista_machine";
        public override List<(Locale, ApplianceInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateApplianceInfo("Mugs", "Provides both large and small mugs", new(), new()))
        };
        public override bool IsPurchasableAsUpgrade => true;
        public override PriceTier PriceTier => PriceTier.Medium;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking;
        public override List<Process> RequiresProcessForShop => new()
        {
            GetExistingGDO(ProcessReferences.FillCoffee) as Process,
            GetCastedGDO<Process, SteamProcess>()
        };

        public override List<Appliance.ApplianceProcesses> Processes => new()
        {
            new()
            {
                IsAutomatic = true,
                Process = GDOUtils.GetExistingGDO(ProcessReferences.FillCoffee) as Process,
                Speed = 1,
                Validity = ProcessValidity.Generic
            },
            new()
            {
                IsAutomatic = true,
                Process = GDOUtils.GetCastedGDO<Process, SteamProcess>(),
                Speed = 1,
                Validity = ProcessValidity.Generic
            }
        };

        public override List<IApplianceProperty> Properties => new()
        {
            new CItemHolder()
        };

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Barista Machine";

            
        }
    }
}
