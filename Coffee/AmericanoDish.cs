using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using MiniCafe.Items;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static MiniCafe.Helper;
using IngredientLib.Ingredient.Items;
using MiniCafe.Processes;

namespace MiniCafe.Coffee
{
    public class AmericanoDish : CustomDish
    {
        public override string UniqueNameID => "americano_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Small American");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Small American");
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
        public override int MinimumFranchiseTier => 0;
        public override bool IsSpecificFranchiseTier => false;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override float SelectionBias => 0;
        public override List<Unlock> HardcodedRequirements => new() { GetGDO<Unlock>(CoffeeBaseDish) };
        public override DishType Type => DishType.Extra;

        public override bool RequiredNoDishItem => true;

        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Prepare black coffee, place on machine to create americano." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Americano", "Adds americano as a coffee variant", ""))
        };
        
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.CoffeeCup),
        };
        public override HashSet<Process> RequiredProcesses => new()
        {
            GetCastedGDO<Process, BoilWaterProcess>()
        };
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, Americano>(),
                Phase = MenuPhase.Dessert,
                Weight = 1f
            }
        };
    }
}
