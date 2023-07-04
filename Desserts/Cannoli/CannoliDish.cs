namespace MiniCafe.Desserts
{
    internal class CannoliDish : CustomDish
    {
        public override string UniqueNameID => "cannoli_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Cannoli");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Cannoli");
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
                Item = GetCastedGDO<Item, Cannoli>(),
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
            //GetCastedGDO<Item, WhippingCream>(),
            GetGDO<Item>(ItemReferences.Sugar),
            GetGDO<Item>(ItemReferences.Cheese),
            GetGDO<Item>(ItemReferences.Flour),
            GetGDO<Item>(ItemReferences.Oil),
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Filling: Chop up chocolate & cheese, add those with sugar and knead. \n" +
                "Shells: Add water to flour or knead flour and then add oil. Knead this again and then cook. \n" +
                "Combine both, portion, and then serve." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Cannoli", "Adds Cannoli as a dessert", "Can o' li!"))
        };
    }
}
