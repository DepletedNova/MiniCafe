namespace MiniCafe.Sides
{
    internal class StroopwafelDish : CustomDish
    {
        public override string UniqueNameID => "stroopwafel_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Stroopwafel");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Stroopwafel");
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
        public override int MinimumFranchiseTier => 0;
        public override bool IsSpecificFranchiseTier => false;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override float SelectionBias => 0;

        public override bool RequiredNoDishItem => true;

        public override DishType Type => DishType.Side;
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, Stroopwafel>(),
                Phase = MenuPhase.Side,
                Weight = 1f
            }
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook),
            GetGDO<Process>(ProcessReferences.Knead),
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, Cinnamon>(),
            GetCastedGDO<Item, ButterBlock>(),
            GetGDO<Item>(ItemReferences.Flour),
            GetGDO<Item>(ItemReferences.Sugar),
            GetCastedGDO<Item, Syrup>()
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Add cinnamon and flour together and then add a slice of butter. Knead and then cook to make the wafel. Add syrup and sugar together before cooking to create the stroop." +
                " Portion wafel and add stroop and then serve as a side." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Stroopwafels", "Adds stroopwafels as a side", "A tasty Dutch treat!"))
        };
    }
}
