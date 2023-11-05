using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using MiniCafe.Processes;
using System.Collections.Generic;
using UnityEngine;

namespace MiniCafe.Coffee
{
    public class PumpkinSpiceDish : CustomDish
    {
        public override string UniqueNameID => "pumpkin_spice_dish";
        public override GameObject DisplayPrefab => Main.Bundle.LoadAsset<GameObject>("Small Pumpkin Spice");
        public override GameObject IconPrefab => Main.Bundle.LoadAsset<GameObject>("Small Pumpkin Spice");
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override bool IsUnlockable => true;
        public override UnlockGroup UnlockGroup => UnlockGroup.Dish;
        public override CardType CardType => CardType.Default;
        public override int MinimumFranchiseTier => 0;
        public override bool IsSpecificFranchiseTier => false;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override float SelectionBias => 0;
        public override List<Unlock> HardcodedRequirements => new() { GetGDO<Unlock>(LatteDish) };
        public override DishType Type => DishType.Extra;

        public override bool RequiredNoDishItem => true;

        public override int Difficulty => 3;

        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Prepare latte. Dispose pumpkin seeds and chop up pumpkin before adding sugar. Add to latte." }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Pumpkin Spice", "Adds pumpkin spice latte as a coffee variant", ""))
        };
        
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.Pumpkin),
            GetGDO<Item>(ItemReferences.Sugar),
        };
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, PumpkinSpice>(),
                Phase = MenuPhase.Dessert,
                Weight = 1f
            }
        };
    }
}
