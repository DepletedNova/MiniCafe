namespace MiniCafe.Desserts
{
    public class MochaDish : CustomDish
    {
        public override string UniqueNameID => "mocha_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Big Mocha");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Big Mocha");
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

        public override Item RequiredDishItem => GetCastedGDO<Item, SmallMug>();
        public override bool RequiredNoDishItem => true;

        public override DishType Type => DishType.Dessert;
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<ItemGroup, BigMocha>(),
                Phase = MenuPhase.Dessert,
                Weight = 1f
            },
            new()
            {
                Item = GetCastedGDO<ItemGroup, SmallMocha>(),
                Phase = MenuPhase.Dessert,
                Weight = 1f
            }
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetCastedGDO<Process, SteamProcess>(),
            GetCastedGDO<Process, CuplessFillCupProcess>(),
            GetGDO<Process>(ProcessReferences.Cook)
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take chocolate and cook, then add to cappuccino and then serve. Whipped cream can optionally be ordered." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Mocha", "Adds mocha as a dessert", "At least it pays well!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, Milk>(),
            GetCastedGDO<Item, Chocolate>(),
            GetCastedGDO<Item, CannedWhippedCream>()
        };
    }
}
