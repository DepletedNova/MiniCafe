using KitchenLib.References;

namespace MiniCafe.Dishes
{
    public class CroissantDish : CustomDish
    {
        public override string UniqueNameID => "croissant_dish";

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
        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => new()
        {
            new()
            {
                Ingredient = GetCastedGDO<Item, Croissant>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedBigMug>()
            },
            new()
            {
                Ingredient = GetCastedGDO<Item, Croissant>(),
                MenuItem = GetCastedGDO<ItemGroup, PlatedSmallMug>()
            }
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook)
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take flour, add water or knead, add a slice of butter, knead and portion, cook, and then add with any coffee main on plate." }
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

        public override void AttachDependentProperties(GameData gameData, GameDataObject gdo)
        {
            base.AttachDependentProperties(gameData, gdo);
            gdo.name = "Croissant Dish";
        }
    }
}
