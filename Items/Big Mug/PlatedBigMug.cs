using static KitchenData.Dish;

namespace MiniCafe.Items
{
    public class PlatedBigMug : CustomItemGroup<PlatedBigMug.View>
    {
        public override string UniqueNameID => "plated_big_mug";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Big");
        public override Item DisposesTo => GetCastedGDO<Item, PlatedBigDirty>();
        public override Item DirtiesTo => GetCastedGDO<Item, PlatedBigDirty>();
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
                    GetCastedGDO<Item, BigEspresso>(),
                    GetCastedGDO<Item, BigAmericano>(),
                    GetCastedGDO<Item, BigCappuccino>(),
                    GetCastedGDO<Item, BigMocha>(),
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
            var steam = Prefab.GetChild("Steam");

            Prefab.ApplyMaterialToChild("Plate", "Plate", "Plate - Ring");
            steam.ApplyVisualEffect("Steam");

            Prefab.ApplyMaterialToChild("espresso", "Coffee - Black");
            BigMug.ApplyMugMaterials(Prefab.GetChild("espresso").GetChild("mug"));

            Prefab.ApplyMaterialToChild("americano", "Americano", "Coffee - Black");
            BigMug.ApplyMugMaterials(Prefab.GetChild("americano").GetChild("mug"));

            Prefab.ApplyMaterialToChild("cappuccino", "Coffee Blend", "Coffee Foam");
            BigMug.ApplyMugMaterials(Prefab.GetChild("cappuccino").GetChild("mug"));

            Prefab.ApplyMaterialToChild("mocha", "Coffee Blend", "Chocolate");
            BigMug.ApplyMugMaterials(Prefab.GetChild("mocha").GetChild("mug"));

            Prefab.ApplyMaterialToChild("whipped", "Coffee Cup");
            Prefab.GetChild("whipped").ApplyMaterialToChild("chocolate", "Chocolate");
            BigMug.ApplyMugMaterials(Prefab.GetChild("whipped").GetChild("mug"));

            Prefab.ApplyMaterialToChild("croissant", "Croissant");

            var view = Prefab.GetComponent<View>();
            view.Setup(gdo);
            view.LabelGameObject.transform.SetParent(steam.transform, false);
            view.LabelGameObject.transform.localScale = new Vector3(2.5f, 1, 2.5f);
        }

        public class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    GameObject = gameObject.GetChild("espresso"),
                    Item = GetCastedGDO<Item, BigEspresso>()
                },
                new()
                {
                    GameObject = gameObject.GetChild("americano"),
                    Item = GetCastedGDO<Item, BigAmericano>()
                },
                new()
                {
                    GameObject = gameObject.GetChild("cappuccino"),
                    Item = GetCastedGDO<Item, BigCappuccino>()
                },
                new()
                {
                    GameObject = gameObject.GetChild("mocha"),
                    Item = GetCastedGDO<Item, BigMocha>()
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
                    Item = GetCastedGDO<Item, BigEspresso>(),
                    Text = "BEs"
                },
                new()
                {
                    Item = GetCastedGDO<Item, BigAmericano>(),
                    Text = "BAm"
                },
                new()
                {
                    Item = GetCastedGDO<Item, BigCappuccino>(),
                    Text = "BCa"
                },
                new()
                {
                    Item = GetCastedGDO<Item, BigMocha>(),
                    Text = "BMo"
                },
                new()
                {
                    Item = GetCastedGDO<Item, WhippedCream>(),
                    Text = "BMoW"
                }
            };
        }

    }
}
