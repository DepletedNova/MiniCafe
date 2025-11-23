using IngredientLib.Ingredient.Items;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Desserts
{
    internal class CannoliDish : CustomDish
    {
        public override string UniqueNameID => "cannoli_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Cannoli");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Cannoli");
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => false;
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
                Item = GetCastedGDO<Item, Cannoli>(),
                Phase = MenuPhase.Dessert,
                Weight = 1f
            }
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook),
            GetGDO<Process>(ProcessReferences.Knead)
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.Sugar),
            GetGDO<Item>(ItemReferences.Cheese),
            GetGDO<Item>(ItemReferences.Flour),
            GetGDO<Item>(ItemReferences.Oil),
            GetGDO<Item>(ItemReferences.Pot),
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take a pot and fill it with oil. Take flour, knead it, add oil and then add to pot before cooking. " +
                "Add sugar and once-chopped cheese together to create the filling and add such to portions from the pot" }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Cannoli", "Adds Cannoli as a dessert", ""))
        };
    }
}
