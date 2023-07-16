namespace MiniCafe.Mains
{
    public class LongJohnDish : CustomDish
    {
        public override string UniqueNameID => "long_john_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Plated Long John");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Plated Long John");
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

        public override DishType Type => DishType.Main;
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Add flour and knead or add water and then add both sugar and a cracked egg. Knead this, add milk, then chop thrice and cook. Portion, plate, and add sprinkles if ordered." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Long Johns", "Adds long johns as a main dish", "Long doughnuts!"))
        };

        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => new()
        {
            new()
            {
                Ingredient = GetCastedGDO<Item, PlainLongJohn>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedDonut>()
            }
        };
    }
}
