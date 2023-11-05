using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace MiniCafe.Coffee.Large
{
    internal class LargeAffogato : CustomItemGroup
    {
        public override string UniqueNameID => "large_affogato";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Large Affogato");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int RewardOverride => 3;
        public override Factor EatingTime => 4f;

        public override List<ColourBlindLabel> Labels => new()
        {
            new()
            {
                Text = "LAf",
                Item = GetGDO<Item>(ItemReferences.IceCreamVanilla),
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, LargeCoffee>(),
                    GetGDO<Item>(ItemReferences.IceCreamVanilla)
                },
                Max = 2,
                Min = 2,
                IsMandatory = true
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("cup", "Light Coffee Cup");
            Prefab.ApplyMaterialToChild("fill", "Coffee - Black");
            Prefab.ApplyMaterialToChild("spoon", "Metal");
            Prefab.ApplyMaterialToChild("icecream", "Vanilla");
        }
    }
}
