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
            GetCastedGDO<Item, Syrup>(),
            GetCastedGDO<Item, EmptyWafelTray>(),
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.Default, "Knead flour into dough and add cinnamon. Place in the dedicated wafel tray and cook. After portioning, add syrup before serving!" },
            { Locale.English, "Knead flour into dough and add cinnamon. Place in the dedicated wafel tray and cook. After portioning, add syrup before serving!" },
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.Default, LocalisationBuilder.NewBuilder<UnlockInfo>().SetName("Stroopwafels").SetDescription("Adds stroopwafels as a desert").SetFlavourText("A tasty Dutch treat!")),
            (Locale.English, LocalisationBuilder.NewBuilder<UnlockInfo>().SetName("Stroopwafels").SetDescription("Adds stroopwafels as a desert").SetFlavourText("A tasty Dutch treat!"))
        };
    }
}
