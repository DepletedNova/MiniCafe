using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using System.Collections.Generic;
using UnityEngine;

namespace MiniCafe.Desserts
{
    internal class LavaCakeMix : CustomItemGroup<ItemGroupView>
    {
        public override string UniqueNameID => "lava_cake_mix";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Lava Cake Mix");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<Item.ItemProcess> Processes => new();

        public override List<ItemGroup.ItemSet> Sets => new();
    }
}
