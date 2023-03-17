namespace MiniCafe.Extras
{
    public class CroissantDish : CustomDish
    {
        public override string UniqueNameID => "croissant_dish";
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
        public override List<Unlock> HardcodedRequirements => new() { GetCastedGDO<Unlock, EspressoDish>() };
        public override List<Unlock> HardcodedBlockers => new();
        public override DishType Type => DishType.Extra;

        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook),
            GetGDO<Process>(ProcessReferences.Knead)
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take flour, add water or knead, add a slice of butter, knead, cook, and then add with any coffee main. This is interchangeable with any extra/spoon." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Croissant", "Adds croissants as an extra", "Buttery goodness!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.Flour),
            GetCastedGDO<Item, Butter>(),
        };
        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => new()
        {
            // Espresso
            new()
            {
                Ingredient = GetCastedGDO<Item, Croissant>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigEspresso>()
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, Croissant>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallEspresso>()
            },
            // Americano
            new()
            {
                Ingredient = GetCastedGDO<Item, Croissant>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigAmericano>()
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, Croissant>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallAmericano>()
            },
            // Iced
            new()
            {
                Ingredient = GetCastedGDO<Item, Croissant>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigIced>()
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, Croissant>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallIced>()
            },
            // Cappuccino
            new()
            {
                Ingredient = GetCastedGDO<Item, Croissant>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigCappuccino>()
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, Croissant>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallCappuccino>()
            },
        };
    }
}
