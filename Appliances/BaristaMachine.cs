using ApplianceLib.Api;
using MiniCafe.Appliances.Spills;

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
                    Description = "Performs <sprite name=\"fill_coffee\"> on two items at a quick rate!"
                }
            }, new()))
        };
        public override bool IsPurchasableAsUpgrade => true;
        public override PriceTier PriceTier => PriceTier.Medium;
        public override ShoppingTags ShoppingTags => ShoppingTags.Cooking;
        public override List<Process> RequiresProcessForShop => new()
        {
            GetGDO<Process>(ProcessReferences.FillCoffee)
        };

        public override List<IApplianceProperty> Properties => new()
        {
            new CDisplayDuration
            {
                Process = ProcessReferences.FillCoffee
            },
            new CTakesDuration {
                Mode = InteractionMode.Items,
            },
            new CAppliesProcessToFlexible
            {
                ProcessTimeMultiplier = 0.9f,
                ProcessType = FlexibleProcessType.Average,
            },
            new CFlexibleContainer
            {
                Maximum = 2
            },
            new COverfills
            {
                ID = GetCustomGameDataObject<CoffeeSpill1>().ID
            }
        };
        public override List<Appliance.ApplianceProcesses> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.FillCoffee),
                IsAutomatic = true,
                Validity = ProcessValidity.Generic,
                Speed = 1f
            }
        };

        public override void OnRegister(Appliance gdo)
        {
            var machine = Prefab.GetChild("CoffeeMachine");
            machine.ApplyMaterialToChildCafe("Machine", "Plastic - Dark Grey", "Wood 1", "Metal", "Plastic");
            machine.ApplyMaterialToChildren("Steam", "Metal Very Dark");

            var counter = Prefab.GetChild("Counter");
            counter.ApplyMaterialCafe("Wood - Default", "Wood 4 - Painted", "Wood 4 - Painted");
            counter.ApplyMaterialToChildCafe("Handle", "Knob");
            counter.ApplyMaterialToChildCafe("Countertop", "Wood - Default");

            var container = Prefab.TryAddComponent<FlexibleContainerView>();
            container.Items = new()
            {
                Prefab.GetChild("Hold (0)"),
                Prefab.GetChild("Hold (1)")
            };
        }
    }
}
