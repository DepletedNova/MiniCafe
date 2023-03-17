namespace MiniCafe.Mains
{
    internal class PlatedBigCappuccino : CustomItemGroup<PlatedBigCappuccino.View>
    {
        public override string UniqueNameID => "plated_big_cappuccino";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Big Cappuccino");
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
                    GetCastedGDO<Item, BigEspresso>(),
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
            BigMug.ApplyMugMaterials(Prefab.GetChild("Mug"));
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
                    Text = "BCa"
                }
            };
        }

    }
}
