namespace MiniCafe.Dishes
{
    public class CappuccinoDish : CustomDish
    {
        public override string UniqueNameID => "cappuccino_dish";

        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override int MinimumFranchiseTier => 0;
        public override bool IsSpecificFranchiseTier => false;
        public override float SelectionBias => 0;
        public override List<Unlock> HardcodedRequirements => new()
        {
            GetCastedGDO<Unlock, EspressoDish>()
        };

        public override DishType Type => DishType.Main;
        public override bool DestroyAfterModUninstall => false;
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, BigCappuccino>(),
                Phase = MenuPhase.Main,
                Weight = 2,
                DynamicMenuIngredient = null,
                DynamicMenuType = DynamicMenuType.Static
            },
            new()
            {
                Item = GetCastedGDO<Item, SmallCappuccino>(),
                Phase = MenuPhase.Main,
                Weight = 2,
                DynamicMenuIngredient = null,
                DynamicMenuType = DynamicMenuType.Static
            }
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.FillCoffee),
            GetCastedGDO<Process, SteamProcess>()
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take a large or small mug and fill with coffee. After it is completed it is ready to be served as is!" }
        };
        public override LocalisationObject<UnlockInfo> Info => CreateUnlockLocalisation(
                (Locale.English, "Cappuccino", "Adds Cappuccino", "That's a weird main course!")
            );
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, BigMug>(),
            GetGDO<Item>(References.GetIngredient("Milk"))
        };

        public override void AttachDependentProperties(GameData gameData, GameDataObject gdo)
        {
            base.AttachDependentProperties(gameData, gdo);
            gdo.name = "Cappuccino Dish";

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
    }
}
