namespace MiniCafe.Mains.Tea
{
    public class SageDish : CustomDish
    {
        public override string UniqueNameID => "sage_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Big Sage");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Big Sage");
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
		public override int MinimumFranchiseTier => 0;
		public override bool IsSpecificFranchiseTier => false;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override float SelectionBias => 0;
        public override List<Unlock> HardcodedRequirements => new() { GetCastedGDO<Unlock, EarlGreyDish>() };
        public override List<Unlock> HardcodedBlockers => new();

        public override DishType Type => DishType.Main;
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take a kettle, fill with water, and put it to a boil. Add sage and then let it steep on a counter. Portion with a mug and serve with a spoon or any other valid extra." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Sage Tea", "Adds sage tea as a main dish", "Green!"))
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook)
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, SmallMug>(),
            GetCastedGDO<Item, Teaspoon>(),
            GetCastedGDO<Item, Kettle>(),
            GetCastedGDO<Item, Sage>(),
            GetGDO<Item>(ItemReferences.Water),
        };
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, PlatedBigSage>(),
                DynamicMenuType = DynamicMenuType.Static,
                Phase = MenuPhase.Main,
                Weight = 1
            },
            new()
            {
                Item = GetCastedGDO<Item, PlatedSmallSage>(),
                DynamicMenuType = DynamicMenuType.Static,
                Phase = MenuPhase.Main,
                Weight = 1
            }
        };
    }
}
