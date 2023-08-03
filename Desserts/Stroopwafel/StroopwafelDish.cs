using IngredientLib.Ingredient.Items;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Sides
{
    internal class StroopwafelDish : CustomDish
    {
        public override string UniqueNameID => "stroopwafel_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Stroopwafel");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Stroopwafel");
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
        public override int MinimumFranchiseTier => 0;
        public override bool IsSpecificFranchiseTier => false;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override float SelectionBias => 0;

        public override bool RequiredNoDishItem => true;

        public override DishType Type => DishType.Dessert;
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, Stroopwafel>(),
                Phase = MenuPhase.Dessert,
                Weight = 1f
            }
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook),
            GetGDO<Process>(ProcessReferences.Knead),
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, Cinnamon>(),
            GetGDO<Item>(ItemReferences.Flour),
            GetGDO<Item>(ItemReferences.Egg),
            GetGDO<Item>(MilkItem),
            GetGDO<Item>(ItemReferences.Sugar),
            GetCastedGDO<Item, Syrup>()
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Add flour, a cracked egg, milk, and cinnamon and then cook. Add sugar and syrup together and cook this and add to the wafel tray. Portion and serve." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Stroopwafels", "Adds stroopwafels as a dessert", "A tasty Dutch treat!"))
        };
    }
}
