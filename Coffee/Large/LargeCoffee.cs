using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;

namespace MiniCafe.Coffee.Large
{
    internal class LargeCoffee : CustomItem
    {
        public override string UniqueNameID => "large_coffee";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Large Coffee");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int RewardOverride => 2;
        public override Factor EatingTime => 4f;

        public override string ColourBlindTag => "LBl";

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("cup", "Light Coffee Cup");
            Prefab.ApplyMaterialToChild("fill", "Coffee - Black");
        }
    }
}
