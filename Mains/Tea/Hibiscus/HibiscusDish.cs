﻿namespace MiniCafe.Mains.Tea
{
    public class HibiscusDish : CustomDish
    {
        public override string UniqueNameID => "hibiscus_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Big Hibiscus");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Big Hibiscus");
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
		public override int MinimumFranchiseTier => 0;
		public override bool IsSpecificFranchiseTier => false;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override float SelectionBias => 0;
        public override List<Unlock> HardcodedRequirements => new() { GetCastedGDO<Unlock, EarlGreyDish>() };
        public override List<Unlock> HardcodedBlockers => new();

        public override Item RequiredDishItem => GetCastedGDO<Item, SmallMug>();
        public override bool RequiredNoDishItem => true;

        public override DishType Type => DishType.Main;
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take a kettle, fill with water, and put it to a boil. Add hibiscus and then let it steep on a counter. Portion with a mug and serve with a spoon or any other valid extra." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Hibiscus Tea", "Adds hibiscus tea as a main dish", "Bitter yet gratifying"))
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook)
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, SmallMug>(),
            GetCastedGDO<Item, Teaspoon>(),
            GetCastedGDO<Item, Kettle>(),
            GetCastedGDO<Item, Hibiscus>(),
            GetGDO<Item>(ItemReferences.Water),
        };
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, PlatedBigHibiscus>(),
                DynamicMenuType = DynamicMenuType.Static,
                Phase = MenuPhase.Main,
                Weight = 1
            },
            new()
            {
                Item = GetCastedGDO<Item, PlatedSmallHibiscus>(),
                DynamicMenuType = DynamicMenuType.Static,
                Phase = MenuPhase.Main,
                Weight = 1
            }
        };
    }
}
