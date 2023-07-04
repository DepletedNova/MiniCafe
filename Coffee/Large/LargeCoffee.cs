using KitchenData;

namespace MiniCafe.Coffee.Large
{
    internal class LargeCoffee : CustomItem
    {
        public override string UniqueNameID => "large_coffee";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Large Coffee");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int RewardOverride => 2;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("cup", "Light Coffee Cup");
            Prefab.ApplyMaterialToChildCafe("fill", "Coffee - Black");
        }
    }
}
