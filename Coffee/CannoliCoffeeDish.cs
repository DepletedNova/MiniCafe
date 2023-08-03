using IngredientLib.Ingredient.Items;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using MiniCafe.Desserts;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Coffee
{
    public class CannoliCoffeeDish : CustomDish
    {
        public override string UniqueNameID => "cannoli_coffee_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Cannoli");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Cannoli");
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
        public override int MinimumFranchiseTier => 0;
        public override bool IsSpecificFranchiseTier => false;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override float SelectionBias => 0;
        public override List<Unlock> HardcodedRequirements => new() { GetGDO<Unlock>(CoffeeshopMode) };
        public override DishType Type => DishType.Extra;

        public override bool RequiredNoDishItem => true;

        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook),
            GetGDO<Process>(ProcessReferences.Knead)
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Filling: Chop up chocolate & cheese, add those with sugar and knead. \n" +
                "Shells: Add water to flour or knead flour and then add oil. Knead this again and then cook. \n" +
                "Combine both, portion, and then serve." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Cannoli", "Adds cannoli as a baked good", "Can o' li!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetCastedGDO<Item, Chocolate>(),
            //GetCastedGDO<Item, WhippingCream>(),
            GetGDO<Item>(ItemReferences.Sugar),
            GetGDO<Item>(ItemReferences.Cheese),
            GetGDO<Item>(ItemReferences.Flour),
            GetGDO<Item>(ItemReferences.Oil),
            GetGDO<Item>(ItemReferences.Water)
        };
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, Cannoli>(),
                Phase = MenuPhase.Dessert,
                Weight = 1f
            }
        };
    }
}
