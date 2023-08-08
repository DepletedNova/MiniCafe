using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using static KitchenLib.Utils.GDOUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Bakery
{
    public class CakeFlavourCherry : CustomDish
    {
        public override string UniqueNameID => "Cake Flavour - Cherry";
        public override Unlock.RewardLevel ExpReward => Unlock.RewardLevel.Medium;
        public override DishCustomerChange CustomerMultiplier => DishCustomerChange.SmallDecrease;
        public override DishType Type => DishType.Dessert;
        public override CardType CardType => CardType.Default;
        public override bool IsUnlockable => true;
        public override bool RequiredNoDishItem => true;

        public override Dictionary<Locale, string> Recipe => new()
        {
            { Locale.English, "Take and use cherries in a pastry recipe" }
        };
        public override List<(Locale, UnlockInfo)> InfoList => new()
        {
            (Locale.English, LocalisationUtils.CreateUnlockInfo("Pastry Flavour - Cherry", "Adds cherry as a pastry flavour", ""))
        };

        public override List<Unlock> HardcodedRequirements => new()
        {
            GetGDO<Unlock>(1113735761)
        };
        public override List<Dish.MenuItem> ResultingMenuItems => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, CherryFlavour>(),
                Weight = 1,
                Phase = MenuPhase.Dessert
            }
        };
        public override HashSet<Item> MinimumIngredients => new()
        {
            GetGDO<Item>(ItemReferences.Cherry)
        };

        public override void OnRegister(Dish gdo)
        {
            gdo.HideInfoPanel = true;
        }
    }
}
