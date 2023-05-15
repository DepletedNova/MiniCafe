using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class PlatedLongJohn : CustomItemGroup<Donut.View>
    {
        public override string UniqueNameID => "plated_long_john";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Long John");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.Medium;

        public override bool CanContainSide => true;
        public override Item DisposesTo => GetGDO<Item>(ItemReferences.Plate);
        public override Item DirtiesTo => GetGDO<Item>(ItemReferences.PlateDirty);

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
                Max = 2,
                Min = 2,
                IsMandatory = true,
                Items = new()
                {
                    GetCastedGDO<Item, PlainLongJohn>(),
                    GetGDO<Item>(ItemReferences.Plate)
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
                RequiresUnlock = true,
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

            Prefab.ApplyMaterialToChildCafe("Plate", "Plate", "Plate - Ring");
            Prefab.ApplyMaterialToChildCafe("Donut", "Bread - Inside Cooked");
            Prefab.ApplyMaterialToChildCafe("Chocolate", "Chocolate");
            Prefab.ApplyMaterialToChildCafe("Caramel", "Caramel");
            Prefab.ApplyMaterialToChildCafe("Glazed", "Bread - Inside");
            Prefab.ApplyMaterialToChildCafe("Sprinkles", "Clothing Pink", "Blueberry", "Plastic - White");
        }
    }
}
