namespace MiniCafe.Mains.Tea
{
    internal class SmallSage : CustomItemGroup<SmallSage.View>
    {
        public override string UniqueNameID => "small_sage";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Sage");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SmallMug>(),
                    GetCastedGDO<Item, SageSteeped>()
                },
                IsMandatory = true,
                Min = 2,
                Max = 2,
            },
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, HoneyIngredient>(),
                    GetCastedGDO<Item, LemonSlice>()
                },
                Min = 0,
                Max = 2
            },
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.GetComponent<View>().Setup(gdo);

            SmallMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChildCafe("fill", "Sage Tea");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
            Prefab.ApplyMaterialToChildren("sage", "Sage");

            Prefab.ApplyMaterialToChildCafe("Lemon", "Lemon", "Lemon Inner", "White Fruit");
            Prefab.ApplyMaterialToChildCafe("Honey", "Honey");
        }

        internal class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, LemonSlice>(),
                    GameObject = gameObject.GetChild("Lemon")
                },
                new()
                {
                    Item = GetCastedGDO<Item, HoneyIngredient>(),
                    GameObject = gameObject.GetChild("Honey"),
                }
            };

            protected override List<ColourBlindLabel> labels => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, SageSteeped>(),
                    Text = "SSa"
                },
                new()
                {
                    Item = GetCastedGDO<Item, LemonSlice>(),
                    Text = "L"
                },
                new()
                {
                    Item = GetCastedGDO<Item, HoneyIngredient>(),
                    Text = "H"
                }
            };
        }
    }
}
