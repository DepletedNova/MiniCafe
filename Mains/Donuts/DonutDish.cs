namespace MiniCafe.Mains
{
    public class DonutDish : CustomDish
    {
        public override string UniqueNameID => "donut_dish";
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Small;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.None;
        public override int MinimumFranchiseTier => 0;
        public override bool IsSpecificFranchiseTier => false;
        public override float SelectionBias => 0;

        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Donut Icon");
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Plated Donut");
        public override DishType Type => DishType.Base;
        public override bool IsAvailableAsLobbyOption => true;
        public override bool DestroyAfterModUninstall => false;
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Add flour and knead or add water and then add both sugar and a cracked egg. Knead this, add milk, then chop twice and cook. Portion, plate, and add sprinkles if ordered." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Doughnuts", "Adds doughnuts as a main dish", "Love me a good doughnut!"))
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook),
            GetGDO<Process>(ProcessReferences.Knead),
            GetGDO<Process>(ProcessReferences.Chop)
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.Plate),
            GetGDO<Item>(ItemReferences.Flour),
            GetGDO<Item>(ItemReferences.Egg),
            GetGDO<Item>(ItemReferences.Sugar),
            GetGDO<Item>(MilkItem),
        };
        public override List<string> StartingNameSet => new()
        {
            "Donut Stop Me Now",
            "Glazy Day",
            "A Sprinkle in Time",
            "Sprinkled with Love",
            "Hole-y Moley!",
            "Jam Packed!"
        };

        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, PlatedDonut>(),
                DynamicMenuType = DynamicMenuType.Static,
                Phase = MenuPhase.Main,
                Weight = 1
            }
        };

        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => new()
        {
            new()
            {
                Ingredient = GetCastedGDO<Item, PlainDonut>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedDonut>()
            }
        };

        public override void OnRegister(Dish gdo)
        {
            IconPrefab.ApplyMaterialToChildCafe("Sprinkles", "Clothing Pink", "Blueberry", "Plastic - White");
            IconPrefab.ApplyMaterialToChildCafe("Icing", "Chocolate");
            IconPrefab.ApplyMaterialToChildCafe("Donut", "Cooked Pastry");
            IconPrefab.ApplyMaterialToChildCafe("Plate", "Plate", "Plate - Ring");
        }
    }
}
