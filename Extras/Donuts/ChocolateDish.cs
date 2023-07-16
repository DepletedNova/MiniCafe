using MiniCafe.Mains;

namespace MiniCafe.Extras
{
    internal class ChocolateDish : CustomDish
    {
        public override string UniqueNameID => "chocolate_donut_dish";
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
        public override List<Unlock> HardcodedRequirements => new() { GetCastedGDO<Unlock, DonutDish>() };
        public override List<Unlock> HardcodedBlockers => new();

        public override DishType Type => DishType.Extra;

        public override bool RequiredNoDishItem => true;

        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take a pot, add milk, and then add sugar and once-chopped chocolate. Cook and then add to any doughnut if ordered." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Chocolate Glaze", "Adds chocolate as a doughnut glaze", "A little bit of sunshine!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.Pot),
            GetGDO<Item>(ItemReferences.Sugar),
            GetCastedGDO<Item, Chocolate>(),
            GetGDO<Item>(MilkItem),
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook)
        };

        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => new()
        {
            new()
            {
                MenuItem = GetCastedGDO<ItemGroup, PlatedDonut>(),
                Ingredient = GetCastedGDO<Item, ChocolateGlazeIngredient>()
            },
        };
    }
}
