namespace MiniCafe.Mains
{
    public class JellyDish : CustomDish
    {
        public override string UniqueNameID => "jelly_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Plated Jelly");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Plated Jelly");
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
            { Locale.English, "Doughnut: Add flour and knead or add water and then add both sugar and a cracked egg. Knead this, add milk, then chop and cook.\n " +
                "\nCustard: Add sugar, a cracked egg, and whipping cream together, knead and then add to a cooked doughnut tray. Portion, plate, and serve." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Custard Filled Doughnuts", "Adds custard filled doughnuts as a main dish", "No jelly?"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.Sugar),
            GetGDO<Item>(ItemReferences.Egg),
            GetCastedGDO<Item, WhippingCream>(),
        };

        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => new()
        {
            new()
            {
                Ingredient = GetCastedGDO<Item, PlainJelly>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedDonut>()
            }
        };
    }
}
