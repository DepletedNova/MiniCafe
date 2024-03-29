﻿using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace MiniCafe.Coffee.Large
{
    internal class LargeIced : CustomItemGroup
    {
        public override string UniqueNameID => "large_iced_coffee";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Large Iced");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int RewardOverride => 3;
        public override Factor EatingTime => 4f;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Text = "LIc",
                Item = GetGDO<Item>(IceItem),
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, LargeCoffee>(),
                    GetGDO<Item>(IceItem)
                },
                Max = 2,
                Min = 2,
                IsMandatory = true
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("iced", "Coffee - Ice", "Coffee Glass");
            Prefab.ApplyMaterialToChild("ice", "Ice");
            Prefab.ApplyMaterialToChild("straw", "Plastic - Red", "Coffee Cup");
        }
    }
}
