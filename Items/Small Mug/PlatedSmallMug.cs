using static KitchenData.Dish;

namespace MiniCafe.Items
{
    public class PlatedSmallMug : CustomItemGroup<PlatedSmallMug.View>
    {
        public override string UniqueNameID => "plated_small_mug";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Small");
        public override Item DisposesTo => GetCastedGDO<Item, PlatedSmallDirty>();
        public override Item DirtiesTo => GetCastedGDO<Item, PlatedSmallDirty>();
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemValue ItemValue => ItemValue.Medium;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override bool CanContainSide => true;
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Plate)
                },
                IsMandatory = true,
                Max = 1,
                Min = 1,
            },
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SmallEspresso>(),
                    GetCastedGDO<Item, SmallAmericano>(),
                    GetCastedGDO<Item, SmallCappuccino>(),
                    GetCastedGDO<Item, SmallMocha>(),
                },
                RequiresUnlock = true,
                Max = 1,
                Min = 1,
            },
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, Croissant>(),
                },
                RequiresUnlock = true,
                Max = 1,
                Min = 0,
            }
        };

        public override void OnRegister(GameDataObject gdo)
        {
            var mug = Prefab.GetChild("mug");

            SmallMug.ApplyMugMaterials(mug);
            Prefab.ApplyMaterialToChild("Plate", "Plate", "Plate - Ring");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");

            Prefab.ApplyMaterialToChild("espresso", "Coffee - Black");

            Prefab.ApplyMaterialToChild("americano", "Americano", "Coffee - Black");

            Prefab.ApplyMaterialToChild("cappuccino", "Coffee Blend", "Coffee Foam");

            Prefab.ApplyMaterialToChild("mocha", "Coffee Blend", "Chocolate");

            Prefab.ApplyMaterialToChild("whipped", "Coffee Cup");
            Prefab.GetChild("whipped").ApplyMaterialToChild("chocolate", "Chocolate");

            Prefab.ApplyMaterialToChild("croissant", "Croissant");

            var view = Prefab.GetComponent<View>();
            view.Setup(gdo);
            view.LabelGameObject.transform.SetParent(mug.transform, false);
        }

        public class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    GameObject = gameObject.GetChild("espresso"),
                    Item = GetCastedGDO<Item, SmallEspresso>()
                },
                new()
                {
                    GameObject = gameObject.GetChild("americano"),
                    Item = GetCastedGDO<Item, SmallAmericano>()
                },
                new()
                {
                    GameObject = gameObject.GetChild("cappuccino"),
                    Item = GetCastedGDO<Item, SmallCappuccino>()
                },
                new()
                {
                    GameObject = gameObject.GetChild("mocha"),
                    Item = GetCastedGDO<Item, SmallMocha>()
                },
                new()
                {
                    GameObject = gameObject.GetChild("whipped"),
                    Item = GetCastedGDO<Item, WhippedCream>()
                },
                new()
                {
                    GameObject = gameObject.GetChild("croissant"),
                    Item = GetCastedGDO<Item, Croissant>()
                },
            };

            protected override List<ColourBlindLabel> labels => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, SmallEspresso>(),
                    Text = "SEs"
                },
                new()
                {
                    Item = GetCastedGDO<Item, SmallAmericano>(),
                    Text = "SAm"
                },
                new()
                {
                    Item = GetCastedGDO<Item, SmallCappuccino>(),
                    Text = "SCa"
                },
                new()
                {
                    Item = GetCastedGDO<Item, SmallMocha>(),
                    Text = "SMo"
                },
                new()
                {
                    Item = GetCastedGDO<Item, WhippedCream>(),
                    Text = "SMoW"
                }
            };
        }

    }
}
