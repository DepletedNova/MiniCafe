namespace MiniCafe.Mains.Tea
{
    public class EarlGreyDish : CustomDish
    {
        public override string UniqueNameID => "earl_grey_dish";
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Small;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.None;
        public override int MinimumFranchiseTier => 0;
        public override bool IsSpecificFranchiseTier => false;
        public override float SelectionBias => 0;

        public override Item RequiredDishItem => GetCastedGDO<Item, SmallMug>();
        public override bool RequiredNoDishItem => true;

        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Tea Icon");
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Plated Small Earl Grey");
        public override DishType Type => DishType.Base;
        public override bool IsAvailableAsLobbyOption => true;
        public override bool DestroyAfterModUninstall => false;
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook),
            GetCastedGDO<Process, RequiresMugProcess>(),
            GetCastedGDO<Process, SteepProcess>(),
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take a kettle, fill with water, and put it to a boil. Add the earl grey and then let it steep on a counter. Portion with a mug and serve with a spoon." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Tea", "Adds earl grey tea as a main dish", "Calming!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, Teaspoon>(),
            GetCastedGDO<Item, Kettle>(),
            GetCastedGDO<Item, EarlGrey>(),
            GetGDO<Item>(ItemReferences.Water)
        };
        public override List<string> StartingNameSet => new()
        {
            "Tea Crew",
            "Old Tea Hut",
            "Hot Sips",
            "The Morning Dew",
            "Tea Time Folly",
            "Tea-Hee",
            "The Tipsy Teapot",
            "Tea-riffic!",
            "Tea?",
            "Lorem Sipsum",
            "Lord of the Mugs",
            "The Matcha Mistress",
            "Lady of Leaf",
            "Tea Mater"
        };
        //public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => GetUnlocks(GetCastedGDO<Item, Teaspoon>());
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, PlatedBigEarlGrey>(),
                DynamicMenuType = DynamicMenuType.Static,
                Phase = MenuPhase.Main,
                Weight = 1
            },
            new()
            {
                Item = GetCastedGDO<Item, PlatedSmallEarlGrey>(),
                DynamicMenuType = DynamicMenuType.Static,
                Phase = MenuPhase.Main,
                Weight = 1
            }
        };

        public override void OnRegister(Dish gdo)
        {
            IconPrefab.ApplyMaterialToChildCafe("Plate", "Marble", "Plate - Ring");
            SmallMug.ApplyMugMaterials(IconPrefab.GetChild("Mug"));
            IconPrefab.ApplyMaterialToChildren("sage", "Sage");
            IconPrefab.ApplyMaterialToChildCafe("Fill", "Sage Tea");
            IconPrefab.ApplyMaterialToChildCafe("Extra", "Bread - Inside Cooked", "Chocolate");
        }
    }
}
