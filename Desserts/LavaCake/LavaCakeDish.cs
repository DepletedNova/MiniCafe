namespace MiniCafe.Desserts
{
    internal class LavaCakeDish : CustomDish
    {
        public override string UniqueNameID => "lava_cake_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Lava Cake");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Lava Cake");
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
        public override int MinimumFranchiseTier => 0;
        public override bool IsSpecificFranchiseTier => false;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override float SelectionBias => 0;

        public override bool RequiredNoDishItem => true;

        public override DishType Type => DishType.Dessert;
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, LavaCake>(),
                Phase = MenuPhase.Dessert,
                Weight = 1f
            }
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook)
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, Chocolate>(),
            GetGDO<Item>(ItemReferences.Egg),
            GetGDO<Item>(ItemReferences.Flour),
            GetGDO<Item>(MilkItem)
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Add flour, a cracked egg, milk, and then twice chopped chocolate and then cook. This can be shared between two people!" }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Lava Cake", "Adds Lava Cake as a dessert", "Flows like lava!"))
        };
    }
}
