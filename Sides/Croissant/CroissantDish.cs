﻿using IngredientLib.Ingredient.Items;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Extras
{
    public class CroissantDish : CustomDish
    {
        public override string UniqueNameID => "croissant_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Croissant");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Croissant");
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override DishType Type => DishType.Side;

        public override bool RequiredNoDishItem => true;

        public override HashSet<Process> RequiredProcesses => new()
        {
            GetGDO<Process>(ProcessReferences.Cook),
            GetGDO<Process>(ProcessReferences.Knead)
        };
        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take flour, add water or knead, add a slice of butter, knead, cook, and then add to any main if ordered!" }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Croissant", "Adds croissants as a side", "Buttery goodness!"))
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.Flour),
            GetCastedGDO<Item, ButterBlock>(),
        };
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, Croissant>(),
                Phase = MenuPhase.Side,
                Weight = 1f
            }
        };
    }
}
