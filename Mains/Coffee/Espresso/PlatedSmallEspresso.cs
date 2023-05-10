namespace MiniCafe.Mains.Coffee
{
    internal class PlatedSmallEspresso : CustomItemGroup<PlatedSmallEspresso.View>
    {
        public override string UniqueNameID => "plated_small_espresso";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Small Espresso");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override Item DirtiesTo => GetCastedGDO<Item, SmallMugDirty>();
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.MediumLarge;
        public override bool CanContainSide => true;

        public override List<ItemGroupView.ColourBlindLabel> Labels => ApplyPlatedLabel(new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, SmallEspresso>(),
                    Text = "SEs"
                }
            });

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SmallEspresso>()
                },
                IsMandatory = true,
                Max = 1,
                Min = 1,
            },
            ExtrasSet
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyGenericPlated();
            Prefab.GetComponent<View>().Setup(gdo);

            SmallMug.ApplyMugMaterials(Prefab.GetChild("Mug"));
            Prefab.ApplyMaterialToChildCafe("Filling", "Coffee - Black");
            Prefab.ApplyMaterialToChildren("Bean", "Wood - Log");
        }

        internal class View : PlatedItemGroupView
        {

        }

    }
}
