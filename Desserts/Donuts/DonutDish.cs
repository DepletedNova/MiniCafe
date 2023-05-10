namespace MiniCafe.Desserts
{
    internal class DonutDish : CustomDish
    {
        public override string UniqueNameID => "donut_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Donut");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Donut");
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
                Item = GetCastedGDO<Item, Donut>(),
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
            GetCastedGDO<Item, ButterBlock>(),
            GetGDO<Item>(ItemReferences.Flour),
            GetCastedGDO<Item, WhippingCreamIngredient>(),
            GetCastedGDO<Item, MilkIngredient>(),
            GetCastedGDO<Item, Sprinkles>()
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Knead dough or add water, add a slice of butter, add milk, then cook. Melt chocolate and add whipping cream then add to cooked doughnut. " +
                "Doughnuts can optionally have sprinkles." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Doughnuts", "Adds doughnuts as a dessert", "Now with sprinkles!"))
        };
    }
}
