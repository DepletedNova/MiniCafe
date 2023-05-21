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
            { Locale.English, "Add milk, flour, and sugar together and then knead. Chop this once and then cook. Portion, plate, and then serve. Add sprinkles if ordered." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Doughnuts", "Adds doughnuts as a main dish", "Love me a good doughnut!"))
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook)
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.Plate),
            GetGDO<Item>(ItemReferences.Flour),
            GetGDO<Item>(ItemReferences.Sugar),
            GetCastedGDO<Item, Milk>(),
            GetCastedGDO<Item, Sprinkles>(),
        };
        public override List<string> StartingNameSet => new()
        {
            "Donut Stop Me Now",
            "Glazy Day",
            "A Sprinkle in Time",
            "Sprinkled with Love",
            "Hole-y Moley!",
            "The Downfall of Humanity",
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

        public override void OnRegister(Dish gdo)
        {
            IconPrefab.ApplyMaterialToChildCafe("Sprinkles", "Clothing Pink", "Blueberry", "Plastic - White");
            IconPrefab.ApplyMaterialToChildCafe("Icing", "Chocolate");
            IconPrefab.ApplyMaterialToChildCafe("Donut", "Cooked Pastry");
            IconPrefab.ApplyMaterialToChildCafe("Plate", "Plate", "Plate - Ring");
        }
    }
}
