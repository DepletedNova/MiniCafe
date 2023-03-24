namespace MiniCafe.Extras
{
    internal class LemonDish : CustomDish
    {
        public override string UniqueNameID => "lemon_slice_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Lemon Slice");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Lemon Slice");
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

        public override DishType Type => DishType.Extra;
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Chop a lemon and then add directly to any tea if ordered." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Lemon Slices", "Adds lemon slices as an extra", "Zowzers!"))
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Chop)
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, Lemon>(),
        };
        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => new()
        {
            new()
            {
                Ingredient = GetCastedGDO<Item, LemonSlice>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigHibiscus>(),
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, LemonSlice>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallHibiscus>(),
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, LemonSlice>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigSage>(),
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, LemonSlice>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallSage>(),
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, LemonSlice>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigEarlGrey>(),
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, LemonSlice>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallEarlGrey>(),
            },
        };
    }
}
