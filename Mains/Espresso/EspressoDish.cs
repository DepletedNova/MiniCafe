namespace MiniCafe.Mains
{
    public class EspressoDish : CustomDish
    {
        public override string UniqueNameID => "espresso_dish";
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Small;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.None;
        public override int MinimumFranchiseTier => 0;
        public override bool IsSpecificFranchiseTier => false;
        public override float SelectionBias => 0;

        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Coffee Icon");
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Plated Big Espresso");
        public override DishType Type => DishType.Base;
        public override bool IsAvailableAsLobbyOption => true;
        public override bool DestroyAfterModUninstall => false;
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetCastedGDO<Process, CuplessFillCupProcess>(),
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take mug, fill with coffee, add spoon or any other valid extra, and then serve. Interact with the mug container to swap between mug types." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Espresso", "Adds espresso as a main dish", "That's a weird main course!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, SmallMug>(),
            GetCastedGDO<Item, Teaspoon>()
        };
        public override List<string> StartingNameSet => new()
        {
            "Deja Brew",
            "The Big Bean",
            "Mocha Madness",
            "Queen of Beans",
            "Java the Hutt",
            "Espress Yourself",
            "The Polar Espresso",
            "Rise and Grind",
            "Hugs with Mugs",
            "The Rancid Spoon"
        };
        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => new()
        {
            // Espresso
            new()
            {
                Ingredient = GetCastedGDO<Item, Teaspoon>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigEspresso>()
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, Teaspoon>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallEspresso>()
            },
            // Americano
            new()
            {
                Ingredient = GetCastedGDO<Item, Teaspoon>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigAmericano>()
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, Teaspoon>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallAmericano>()
            },
            // Iced
            new()
            {
                Ingredient = GetCastedGDO<Item, Teaspoon>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigIced>()
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, Teaspoon>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallIced>()
            },
            // Cappuccino
            new()
            {
                Ingredient = GetCastedGDO<Item, Teaspoon>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigCappuccino>()
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, Teaspoon>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallCappuccino>()
            },
        };
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, PlatedBigEspresso>(),
                DynamicMenuType = DynamicMenuType.Static,
                Phase = MenuPhase.Main,
                Weight = 1
            },
            new()
            {
                Item = GetCastedGDO<Item, PlatedSmallEspresso>(),
                DynamicMenuType = DynamicMenuType.Static,
                Phase = MenuPhase.Main,
                Weight = 1
            }
        };

        public override void OnRegister(Dish gdo)
        {
            IconPrefab.ApplyMaterialToChildCafe("Croissant", "Croissant");
            IconPrefab.ApplyMaterialToChildCafe("Fill", "Americano", "Coffee - Black");
            IconPrefab.ApplyMaterialToChildCafe("Plate", "Marble", "Plate - Ring");
            BigMug.ApplyMugMaterials(IconPrefab.GetChild("Mug"));
        }
    }
}
