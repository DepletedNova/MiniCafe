namespace MiniCafe.Mains
{
    public class CappuccinoDish : CustomDish
    {
        public override string UniqueNameID => "cappuccino_dish";

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
        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => new()
        {
            new()
            {
                Ingredient = GetCastedGDO<Item, BigCappuccino>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigCoffee>()
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, SmallCappuccino>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallCoffee>()
            }
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetCastedGDO<Process, SteamProcess>(),
            GetGDO<Process>(ProcessReferences.FillCoffee)
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Steam milk in any coffee machine, combine with espresso, add spoon or any other valid extra, and then serve." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Cappuccino", "Adds cappuccino as a main dish", "Espresso with a few extra steps!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, Milk>(),
        };
    }
}
