namespace MiniCafe.Mains.Tea
{
    internal class PlatedSmallEarlGrey : CustomItemGroup<PlatedSmallEarlGrey.View>
    {
        public override string UniqueNameID => "plated_small_earl_grey";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Small Earl Grey");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override Item DirtiesTo => GetCastedGDO<Item, SmallMugDirty>();
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
                    GetCastedGDO<Item, SmallMug>(),
                    GetCastedGDO<Item, EarlGreySteeped>()
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
                Max = 1
            },
            ExtrasSet
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyGenericPlated();
            Prefab.GetComponent<View>().Setup(gdo);

            SmallMug.ApplyMugMaterials(Prefab.GetChild("Mug"));
            Prefab.ApplyMaterialToChildCafe("Filling", "Earl Grey Tea");
            Prefab.ApplyMaterialToChildren("Decoration", "Plastic - Dark Green");

            Prefab.ApplyMaterialToChildCafe("Lemon", "Lemon", "Lemon Inner", "White Fruit");
            Prefab.ApplyMaterialToChildCafe("Honey", "Door Glass", "Honey", "Wood 1");
            Prefab.ApplyMaterialToChildCafe("Napkin", "Clothing Soft Pink");
        }

        internal class View : PlatedItemGroupView
        {
            protected override List<ColourBlindLabel> labels => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, EarlGreySteeped>(),
                    Text = "SEG"
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
