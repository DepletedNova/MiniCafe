namespace MiniCafe.Extras
{
    internal class HoneyDish : CustomDish
    {
        public override string UniqueNameID => "honey";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Lemon Slice Plate");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Lemon Slice Plate");
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
            { Locale.English, "Add honey to any tea if ordered." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Honey", "Adds honey as an extra", "A good sweetener!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, Honey>(),
        };
        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => new()
        {
            new()
            {
                Ingredient = GetCastedGDO<Item, HoneyIngredient>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigHibiscus>(),
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, HoneyIngredient>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallHibiscus>(),
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, HoneyIngredient>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigSage>(),
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, HoneyIngredient>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallSage>(),
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, HoneyIngredient>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigEarlGrey>(),
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, HoneyIngredient>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallEarlGrey>(),
            },
        };
    }
}
