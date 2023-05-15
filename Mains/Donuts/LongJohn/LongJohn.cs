using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class LongJohn : CustomItemGroup<Donut.View>
    {
        public override string UniqueNameID => "long_john";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Long John");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, Caramel>(),
                Text = "Ca"
            },
            new()
            {
                Item = GetCastedGDO<Item, ChocolateGlazeIngredient>(),
                Text = "Ch"
            },
            new()
            {
                Item = GetCastedGDO<Item, GlazeIngredient>(),
                Text = "Gl"
            },
            new()
            {
                Item = GetCastedGDO<Item, SprinklesIngredient>(),
                Text = "Sp"
            },
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Max = 1,
                Min = 1,
                IsMandatory = true,
                Items = new()
                {
                    GetCastedGDO<Item, PlainLongJohn>()
                }
            },
            new()
            {
                Max = 1,
                Min = 0,
                Items = new()
                {
                    GetCastedGDO<Item, SprinklesIngredient>()
                }
            },
            new()
            {
                Max = 1,
                Min = 0,
                Items = new()
                {
                    GetCastedGDO<Item, Caramel>(),
                    GetCastedGDO<Item, ChocolateGlazeIngredient>(),
                    GetCastedGDO<Item, GlazeIngredient>(),
                }
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.TryAddComponent<Donut.View>().Setup(gdo);

            Prefab.ApplyMaterialToChildCafe("Donut", "Bread - Inside Cooked");
            Prefab.ApplyMaterialToChildCafe("Chocolate", "Chocolate");
            Prefab.ApplyMaterialToChildCafe("Caramel", "Caramel");
            Prefab.ApplyMaterialToChildCafe("Glazed", "Bread - Inside");
            Prefab.ApplyMaterialToChildCafe("Sprinkles", "Clothing Pink", "Blueberry", "Plastic - White");
        }
    }
}
