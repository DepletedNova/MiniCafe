using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using MiniCafe.Items;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.MaterialUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Coffee.Large
{
    internal class LargeLatte : CustomItemGroup
    {
        public override string UniqueNameID => "large_latte";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Large Latte");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int RewardOverride => 3;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Text = "LLa",
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
                    GetGDO<Item>(FrothedMilk)
                },
                Max = 2,
                Min = 2,
                IsMandatory = true
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("latte", "Light Coffee Cup", "Coffee - Latte");
            Prefab.ApplyMaterialToChild("plate", "Coffee Cup");
        }
    }
}
