namespace MiniCafe.Mains
{
    internal class PlatedSmallCappuccino : CustomItemGroup<PlatedSmallCappuccino.View>
    {
        public override string UniqueNameID => "plated_small_cappuccino";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Small Cappuccino");
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
                    GetCastedGDO<Item, SmallEspresso>(),
                    GetCastedGDO<Item, SteamedMilk>()
                },
                IsMandatory = true,
                Max = 2,
                Min = 2,
            },
            ExtrasSet
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.GetComponent<View>().Setup(gdo);

            Prefab.ApplyGenericPlated();
            SmallMug.ApplyMugMaterials(Prefab.GetChild("Mug"));
            Prefab.ApplyMaterialToChildCafe("Filling", "Coffee Blend", "Coffee Foam");
            Prefab.ApplyMaterialToChildCafe("Napkin", "Cloth - Blue");
        }

        internal class View : PlatedItemGroupView
        {
            protected override List<ColourBlindLabel> labels => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, SteamedMilk>(),
                    Text = "SCa"
                }
            };
        }

    }
}
