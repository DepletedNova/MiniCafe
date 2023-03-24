namespace MiniCafe.Mains.Tea
{
    internal class PlatedBigSage : CustomItemGroup<PlatedBigSage.View>
    {
        public override string UniqueNameID => "plated_big_sage";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Big Sage");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override Item DirtiesTo => GetCastedGDO<Item, BigMugDirty>();
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.MediumLarge;
        public override bool CanContainSide => true;

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, BigMug>(),
                    GetCastedGDO<Item, SageSteeped>()
                },
                IsMandatory = true,
                Max = 2,
                Min = 2,
            },
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, HoneyIngredient>(),
                    GetCastedGDO<Item, LemonSlice>()
                },
                RequiresUnlock = true,
                Min = 0,
                Max = 2
            },
            ExtrasSet
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyGenericPlated();
            Prefab.GetComponent<View>().Setup(gdo);

            BigMug.ApplyMugMaterials(Prefab.GetChild("Mug"));
            Prefab.ApplyMaterialToChildCafe("Filling", "Sage Tea");
            Prefab.ApplyMaterialToChildren("sage", "Sage");

            Prefab.ApplyMaterialToChildCafe("Lemon", "Lemon", "Lemon Inner", "White Fruit");
            Prefab.ApplyMaterialToChildCafe("Honey", "Honey");
        }

        internal class View : PlatedItemGroupView
        {
            protected override List<ColourBlindLabel> labels => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, SageSteeped>(),
                    Text = "BSa"
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
                },
            };

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
                    GameObject = gameObject.GetChild("Honey")
                },
            };
        }

    }
}
