namespace MiniCafe.Coffee
{
    public class CroissantCoffeeDish : CustomDish
    {
        public override string UniqueNameID => "croissant_coffee_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Croissant");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Croissant");
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
        public override int MinimumFranchiseTier => 0;
        public override bool IsSpecificFranchiseTier => false;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override float SelectionBias => 0;
        public override List<Unlock> HardcodedRequirements => new() { GetGDO<Unlock>(CoffeeshopMode) };
        public override DishType Type => DishType.Extra;

        public override bool RequiredNoDishItem => true;

        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook),
            GetGDO<Process>(ProcessReferences.Knead)
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take flour, add water or knead, add a slice of butter, knead, cook, and then add to any main if ordered!" }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Croissant", "Adds croissants as a baked good", "Buttery goodness!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.Flour),
            GetCastedGDO<Item, ButterBlock>(),
            GetGDO<Item>(ItemReferences.Water)
        };
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, Croissant>(),
                Phase = MenuPhase.Dessert,
                Weight = 1f
            }
        };
    }
}
