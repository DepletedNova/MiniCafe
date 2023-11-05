using IngredientLib.Ingredient.Items;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using MiniCafe.Items;
using System.Collections.Generic;
using UnityEngine;

namespace MiniCafe.Coffee.Large
{
    internal class LargePumpkinSpice : CustomItemGroup
    {
        public override string UniqueNameID => "large_pumpkin_spice";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Large Pumpkin Spice");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int RewardOverride => 4;
        public override Factor EatingTime => 4f;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Text = "LAm",
                Item = GetCastedGDO<Item, PumpkinSpiceIngredient>(),
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, LargeLatte>(),
                    GetCastedGDO<Item, PumpkinSpiceIngredient>()
                },
                Max = 2,
                Min = 2,
                IsMandatory = true
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("coffee", "Light Coffee Cup", "Pumpkin Spice", "Plastic - Black", "Pumpkin");
            Prefab.ApplyMaterialToChild("straw", "Pumpkin", "Coffee Cup");
        }
    }
}
