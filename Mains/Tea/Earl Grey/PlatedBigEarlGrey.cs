namespace MiniCafe.Mains.Tea
{
    internal class PlatedBigEarlGrey : CustomItemGroup<PlatedBigEarlGrey.View>
    {
        public override string UniqueNameID => "plated_big_earl_grey";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Big Earl Grey");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override Item DirtiesTo => GetCastedGDO<Item, BigMugDirty>();
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.MediumLarge;
        public override bool CanContainSide => true;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, Teaspoon>(),
                    Text = "BEG"
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

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, BigEarlGrey>(),
                    GetCastedGDO<Item, Teaspoon>()
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
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyGenericPlated();
            Prefab.TryAddComponent<View>().Setup(gdo);

            BigMug.ApplyMugMaterials(Prefab.GetChild("Mug"));
            Prefab.ApplyMaterialToChildCafe("Filling", "Earl Grey Tea");
            Prefab.ApplyMaterialToChildren("Decoration", "Plastic - Dark Green");

            Prefab.ApplyMaterialToChildCafe("Lemon", "Lemon", "Lemon Inner", "White Fruit");
            Prefab.ApplyMaterialToChildCafe("Honey", "Door Glass", "Honey", "Wood 1");
            Prefab.ApplyMaterialToChildCafe("Napkin", "Clothing Soft Pink");
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
                    GameObject = gameObject.GetChild("Honey")
                },
            };
        }

    }
}
