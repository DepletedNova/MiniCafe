﻿namespace MiniCafe.Dishes
{
    public class AmericanoDish : CustomDish
    {
        public override string UniqueNameID => "americano_dish";

        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
		public override int MinimumFranchiseTier => 0;
		public override bool IsSpecificFranchiseTier => false;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override float SelectionBias => 0;
        public override List<Unlock> HardcodedRequirements => new() { GetCastedGDO<Unlock, EspressoDish>() };
        public override List<Unlock> HardcodedBlockers => new();

        public override DishType Type => DishType.Main;
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, BigAmericano>(),
                Phase = MenuPhase.Main,
                Weight = 1,
                DynamicMenuIngredient = null,
                DynamicMenuType = DynamicMenuType.Static
            },
            new()
            {
                Item = GetCastedGDO<Item, SmallAmericano>(),
                Phase = MenuPhase.Main,
                Weight = 1,
                DynamicMenuIngredient = null,
                DynamicMenuType = DynamicMenuType.Static
            }
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetCastedGDO<Process, SteamProcess>(),
            GetGDO<Process>(ProcessReferences.FillCoffee)
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Add water to espresso and serve." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Americano", "Adds americano as a main dish", "Espresso with a touch of water!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.Water)
        };

        public override void AttachDependentProperties(GameData gameData, GameDataObject gdo)
        {
            base.AttachDependentProperties(gameData, gdo);
            gdo.name = "Americano Dish";
        }
    }
}
