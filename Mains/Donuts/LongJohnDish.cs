namespace MiniCafe.Mains
{
    public class LongJohnDish : CustomDish
    {
        public override string UniqueNameID => "long_john_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Plated Long John");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Plated Long John");
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
            { Locale.English, "Add milk, flour, and sugar together and then knead. Chop this twice and then cook. Portion, plate, and then serve. Add sprinkles if ordered." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Long Johns", "Adds long johns as a main dish", "Long doughnuts!"))
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook)
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.Plate),
            GetGDO<Item>(ItemReferences.Flour),
            GetGDO<Item>(ItemReferences.Sugar),
            GetCastedGDO<Item, MilkIngredient>(),
            GetCastedGDO<Item, Sprinkles>(),
        };
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, PlatedLongJohn>(),
                DynamicMenuType = DynamicMenuType.Static,
                Phase = MenuPhase.Main,
                Weight = 1
            }
        };
    }
}
