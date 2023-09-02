using Kitchen;
using KitchenData;
using IngredientLib.Ingredient.Items;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using MiniCafe.Items;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.MaterialUtils;
using static MiniCafe.Helper;
using IngredientLib.Ingredient.Items;

namespace MiniCafe.Coffee
{
    internal class Americano : CustomItemGroup
    {
        public override string UniqueNameID => "americano";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small American");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int RewardOverride => 2;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Text = "SAm",
                Item = GetCastedGDO<Item, BoiledWater>(),
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.CoffeeCupCoffee),
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
