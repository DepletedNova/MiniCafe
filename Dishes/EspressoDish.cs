namespace MiniCafe.Dishes
{
    public class EspressoDish : CustomDish
    {
        public override string UniqueNameID => "espresso_dish";

        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Small;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.LargeIncrease;
        public override int MinimumFranchiseTier => 0;
        public override bool IsSpecificFranchiseTier => false;
        public override float SelectionBias => 0;

        public override List<Unlock> HardcodedBlockers => new()
        {
            GetGDO<Unlock>(DishReferences.CoffeeDessert)
        };

        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Coffee Icon");
        public override DishType Type => DishType.Base;
        public override bool IsAvailableAsLobbyOption => true;
        public override bool DestroyAfterModUninstall => false;
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, BigEspresso>(),
                Phase = MenuPhase.Main,
                Weight = 1,
                DynamicMenuIngredient = null,
                DynamicMenuType = DynamicMenuType.Static
            },
            new()
            {
                Item = GetCastedGDO<Item, SmallEspresso>(),
                Phase = MenuPhase.Main,
                Weight = 1,
                DynamicMenuIngredient = null,
                DynamicMenuType = DynamicMenuType.Static
            }
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.FillCoffee)
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take a large or small mug and fill with coffee. After it is completed it is ready to be served as is!" }
        };
        public override LocalisationObject<UnlockInfo> Info => CreateUnlockLocalisation(
                (Locale.English, "Espresso", "Adds Espresso as a Main", "That's a weird main course!")
            );
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, BigMug>()
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

            (gdo as Dish).UnlocksMenuItems = new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, BigEspresso>(),
                    Phase = MenuPhase.Main,
                    Weight = 1,
                    DynamicMenuIngredient = null,
                    DynamicMenuType = DynamicMenuType.Static
                },
                new()
                {
                    Item = GetCastedGDO<Item, SmallEspresso>(),
                    Phase = MenuPhase.Main,
                    Weight = 1,
                    DynamicMenuIngredient = null,
                    DynamicMenuType = DynamicMenuType.Static
                }
            };
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
