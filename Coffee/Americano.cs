using KitchenData;

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
            Prefab.ApplyMaterialToChildCafe("coffee", "Americano", "Coffee Foam", "Coffee Glass");
        }
    }
}
