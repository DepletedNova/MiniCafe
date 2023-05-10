namespace MiniCafe.Desserts
{
    internal class Donut : CustomItemGroup<Donut.View>
    {
        public override string UniqueNameID => "donut";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Donut");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.Medium;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, Ganache>(),
                Text = "Do"
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
                Items = new()
                {
                    GetCastedGDO<Item, CookedDonut>(),
                    GetCastedGDO<Item, Ganache>(),
                },
                Max = 2,
                Min = 2,
                IsMandatory = true
            },
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SprinklesIngredient>()
                },
                Max = 1,
                Min = 0,
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.GetComponent<View>().Setup(gdo);

            Prefab.ApplyMaterialToChildCafe("Donut", "Bread - Inside Cooked");
            Prefab.ApplyMaterialToChildCafe("Chocolate", "Chocolate");
            Prefab.ApplyMaterialToChildCafe("Sprinkles", "Clothing Pink", "Blueberry", "Plastic - White");
        }

        public class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, Ganache>(),
                    GameObject = gameObject.GetChild("Chocolate")
                },
                new()
                {
                    Item = GetCastedGDO<Item, SprinklesIngredient>(),
                    GameObject = gameObject.GetChild("Sprinkles")
                }
            };
        }
    }
}
