using MiniCafe.Mains;

namespace MiniCafe.Extras
{
    internal class CaramelDish : CustomDish
    {
        public override string UniqueNameID => "caramel_donut_dish";
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
            { Locale.English, "Cook sugar and add to a doughnut if ordered." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Caramel", "Adds caramel as a doughnut glaze", "A sugary choice!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.Sugar)
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
                Ingredient = GetCastedGDO<Item, Caramel>()
            },
        };
    }
}
