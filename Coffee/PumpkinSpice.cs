using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using MiniCafe.Items;
using System.Collections.Generic;
using UnityEngine;

namespace MiniCafe.Coffee
{
    internal class PumpkinSpice : CustomItemGroup
    {
        public override string UniqueNameID => "pumpkin_spice";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Pumpkin Spice");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int RewardOverride => 3;
        public override Factor EatingTime => 3f;

        public override List<ColourBlindLabel> Labels => new()
        {
            new()
            {
                Text = "SPS",
                Item = GetCastedGDO<Item, PumpkinSpiceIngredient>()
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Latte),
                    GetCastedGDO<Item, PumpkinSpiceIngredient>()
                },
                Max = 2,
                Min = 2,
                IsMandatory = true
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("coffee", "Coffee Cup", "Pumpkin Spice", "Pumpkin");
        }
    }
}
