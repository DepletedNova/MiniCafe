namespace MiniCafe.Dishes
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
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Big Espresso");
        public override DishType Type => DishType.Base;
        public override bool IsAvailableAsLobbyOption => true;
        public override bool DestroyAfterModUninstall => false;
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<ItemGroup, PlatedBigMug>(),
                Phase = MenuPhase.Main,
                Weight = 1,
                DynamicMenuIngredient = null,
                DynamicMenuType = DynamicMenuType.Static
            },
            new()
            {
                Item = GetCastedGDO<ItemGroup, PlatedSmallMug>(),
                Phase = MenuPhase.Main,
                Weight = 1,
                DynamicMenuIngredient = null,
                DynamicMenuType = DynamicMenuType.Static
            }
        };
        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => new()
        {
            new()
            {
                Ingredient = GetCastedGDO<Item, BigEspresso>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigMug>()
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, SmallEspresso>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallMug>()
            }
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetCastedGDO<Process, SteamProcess>(),
            GetGDO<Process>(ProcessReferences.FillCoffee)
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take cup, fill with coffee, add to plate, and then serve." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Espresso", "Adds espresso as a main dish", "That's a weird main course!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, SmallMug>(),
            GetGDO<Item>(ItemReferences.Plate)
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

        public override void AttachDependentProperties(GameData gameData, GameDataObject gdo)
        {
            base.AttachDependentProperties(gameData, gdo);
            gdo.name = "Espresso Dish";
        }

        public override void OnRegister(GameDataObject gdo)
        {
            var bigCoffee = IconPrefab.GetChild("Big Coffee");
            BigMug.ApplyMugMaterials(bigCoffee.GetChild("mug"));
            bigCoffee.ApplyMaterialToChild("fill", "Coffee - Black");

            var smallCoffee = IconPrefab.GetChild("Small Coffee");
            SmallMug.ApplyMugMaterials(smallCoffee.GetChild("mug"));
            smallCoffee.ApplyMaterialToChild("fill", "Coffee Blend", "Coffee Foam");
        }
    }
}
