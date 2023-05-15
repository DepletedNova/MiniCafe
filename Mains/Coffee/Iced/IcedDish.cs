namespace MiniCafe.Mains.Coffee
{
    public class IcedDish : CustomDish
    {
        public override string UniqueNameID => "iced_coffee_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Big Ice Espresso");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Big Ice Espresso");
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

        public override Item RequiredDishItem => GetCastedGDO<Item, SmallMug>();
        public override bool RequiredNoDishItem => true;

        public override DishType Type => DishType.Main;
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.FillCoffee)
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Add ice to espresso, add spoon and then serve." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Iced Espresso", "Adds iced espresso as a main dish", "Chilling!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, Ice>()
        };
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, PlatedBigIced>(),
                DynamicMenuType = DynamicMenuType.Static,
                Phase = MenuPhase.Main,
                Weight = 1
            },
            new()
            {
                Item = GetCastedGDO<Item, PlatedSmallIced>(),
                DynamicMenuType = DynamicMenuType.Static,
                Phase = MenuPhase.Main,
                Weight = 1
            }
        };
    }
}
