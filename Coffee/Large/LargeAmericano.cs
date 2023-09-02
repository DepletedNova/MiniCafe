using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using MiniCafe.Items;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.MaterialUtils;
using IngredientLib.Ingredient.Items;

namespace MiniCafe.Coffee.Large
{
    internal class LargeAmericano : CustomItemGroup
    {
        public override string UniqueNameID => "large_americano";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Large American");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int RewardOverride => 3;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Text = "LAm",
                Item = GetCastedGDO<Item, BoiledWater>(),
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, LargeCoffee>(),
                    GetCastedGDO<Item, BoiledWater>()
                },
                Max = 2,
                Min = 2,
                IsMandatory = true
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("coffee", "Americano", "Coffee Foam", "Coffee Glass");
        }
    }
}
