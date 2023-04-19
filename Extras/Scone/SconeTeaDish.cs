﻿namespace MiniCafe.Extras
{
    public class SconeTeaDish : CustomDish
    {
        public override string UniqueNameID => "scone_tea_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Scone");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Scone");
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
        public override int MinimumFranchiseTier => 0;
        public override bool IsSpecificFranchiseTier => false;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.LargeDecrease;
        public override float SelectionBias => 0;
        public override List<Unlock> HardcodedRequirements => new() { GetCastedGDO<Unlock, EarlGreyDish>() };
        public override List<Unlock> HardcodedBlockers => new() { GetCastedGDO<Unlock, SconeCoffeeDish>() };
        public override DishType Type => DishType.Extra;
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook),
            GetGDO<Process>(ProcessReferences.Knead)
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Crack an egg and then add flour. Knead this once and then add whipping cream. Knead once more and then cook. Portion and serve with any valid main." +
                "This is interchangeable with any extra/spoon." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Scones", "Adds scones as an extra", "Yummers!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.Flour),
            GetGDO<Item>(ItemReferences.Egg),
            GetCastedGDO<Item, WhippingCream>(),
        };
        public override HashSet<Dish.IngredientUnlock> IngredientsUnlocks => ExtraHelper.GetUnlocks(GetCastedGDO<Item, Scone>());
    }
}