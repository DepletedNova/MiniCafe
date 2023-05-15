namespace MiniCafe.Mains.Coffee
{
    internal class PlatedSmallEspresso : CustomItemGroup
    {
        public override string UniqueNameID => "plated_small_espresso";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Small Espresso");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override Item DirtiesTo => GetCastedGDO<Item, SmallMugDirty>();
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.MediumLarge;
        public override bool CanContainSide => true;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, SmallEspresso>(),
                    Text = "SEs"
                }
            };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SmallEspresso>(),
                    GetCastedGDO<Item, Teaspoon>()
                },
                IsMandatory = true,
                Max = 2,
                Min = 2,
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyGenericPlated();

            SmallMug.ApplyMugMaterials(Prefab.GetChild("Mug"));
            Prefab.ApplyMaterialToChildCafe("Filling", "Coffee - Black");
            Prefab.ApplyMaterialToChildren("Bean", "Wood - Log");
        }
    }
}
