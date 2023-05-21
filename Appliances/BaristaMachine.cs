﻿using ApplianceLib.Api;
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

        public override List<IApplianceProperty> Properties => new()
        {
            new CDisplayDuration
            {
                Process = ProcessReferences.FillCoffee
            },
            new CTakesDuration {
                Mode = InteractionMode.Items,
                Total = 4.5f,
            },
            new CAppliesProcessToFlexible
            {
                MinimumItems = 2,
                ProcessTimeMultiplier = 0.5f,
                ProcessType = FlexibleProcessType.Average
            },
            new CFlexibleContainer
            {
                Maximum = 2
            },
            new CRestrictedReceiver
            {
                ApplianceKey = Main.EmptyMugKey
            },
            new CChangeRestrictedReceiverKeyAfterDuration
            {
                ApplianceKey = Main.FilledMugKey
            },
            new CChangeRestrictedReceiverKeyWhenEmpty
            {
                ApplianceKey = Main.EmptyMugKey
            }
        };
        public override List<Appliance.ApplianceProcesses> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.FillCoffee),
                Speed = 2f,
                IsAutomatic = true,
                Validity = ProcessValidity.Generic
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
