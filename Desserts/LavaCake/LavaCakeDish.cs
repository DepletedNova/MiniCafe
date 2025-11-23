using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace MiniCafe.Desserts
{
    internal class LavaCakeDish : CustomDish
    {
        public override string UniqueNameID => "lava_cake_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Lava Cake");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Lava Cake");
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
        public override List<Dish.MenuItem> ResultingMenuItems => new();
        public override HashSet<Process> RequiredProcesses => new();
        public override HashSet<Item> MinimumIngredients => new();
        public override Dictionary<Locale, string> Recipe => new();
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationBuilder.NewBuilder<UnlockInfo>().SetName("Lava Cake").SetDescription("Outdated"))
        };
    }
}
