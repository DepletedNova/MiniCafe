namespace MiniCafe.Mains
{
    public class JellyDish : CustomDish
    {
        public override string UniqueNameID => "jelly_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Plated Jelly");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Plated Jelly");
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
		public override int MinimumFranchiseTier => 0;
		public override bool IsSpecificFranchiseTier => false;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override float SelectionBias => 0;
        public override List<Unlock> HardcodedRequirements => new() { GetCastedGDO<Unlock, DonutDish>() };
        public override List<Unlock> HardcodedBlockers => new();

        public override DishType Type => DishType.Main;
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Add milk, flour, and sugar together and then knead and cook. Add sugar and water, cook, add butter, and then mix. " +
                "Portion doughnut and combine it with the filling that is ordered before serving." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Filled Doughnuts", "Adds filled doughnuts as a main dish", "Can't be jelly with only creme, right?"))
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook)
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.Water),
            GetCastedGDO<Item, ButterBlock>(),
        };

        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, PlatedJelly>(),
                DynamicMenuType = DynamicMenuType.Static,
                Phase = MenuPhase.Main,
                Weight = 1
            }
        };

        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => new()
        {
            new()
            {
                Ingredient = GetCastedGDO<Item, CremeIngredient>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedJelly>()
            }
        };
    }
}
