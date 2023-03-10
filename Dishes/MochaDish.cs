namespace MiniCafe.Dishes
{
    public class MochaDish : CustomDish
    {
        public override string UniqueNameID => "mocha_dish";

        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
		public override int MinimumFranchiseTier => 0;
		public override bool IsSpecificFranchiseTier => false;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override float SelectionBias => 0;
        public override List<Unlock> HardcodedRequirements => new() { GetCastedGDO<Unlock, CappuccinoDish>() };
        public override List<Unlock> HardcodedBlockers => new();

        public override DishType Type => DishType.Main;
        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => new()
        {
            new()
            {
                Ingredient = GetCastedGDO<Item, BigMocha>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigMug>()
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, SmallMocha>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallMug>()
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, BigWhipped>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigMug>()
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, SmallWhipped>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallMug>()
            }
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetCastedGDO<Process, SteamProcess>(),
            GetGDO<Process>(ProcessReferences.FillCoffee),
            GetGDO<Process>(ProcessReferences.Cook)
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Add melted chocolate to cappuccino, add whipped cream if ordered, add to plate, and then serve." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Mocha", "Adds mocha as a main dish", "At least it pays well!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, Milk>(),
            GetCastedGDO<Item, Chocolate>(),
            GetCastedGDO<Item, CannedWhippedCream>()
        };

        public override void AttachDependentProperties(GameData gameData, GameDataObject gdo)
        {
            base.AttachDependentProperties(gameData, gdo);
            gdo.name = "Mocha Dish";
        }
    }
}
