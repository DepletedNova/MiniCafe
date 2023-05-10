namespace MiniCafe.Mains.Coffee
{
    internal class PlatedBigEspresso : CustomItemGroup<PlatedBigEspresso.View>
    {
        public override string UniqueNameID => "plated_big_espresso";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Big Espresso");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override Item DirtiesTo => GetCastedGDO<Item, BigMugDirty>();
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.MediumLarge;
        public override bool CanContainSide => true;

        public override List<ItemGroupView.ColourBlindLabel> Labels => ApplyPlatedLabel(new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, BigEspresso>(),
                    Text = "BEs"
                }
            });

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, BigEspresso>()
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

            BigMug.ApplyMugMaterials(Prefab.GetChild("Mug"));
            Prefab.ApplyMaterialToChildCafe("Filling", "Coffee - Black");
            Prefab.ApplyMaterialToChildren("Bean", "Wood - Log");
        }

        internal class View : PlatedItemGroupView
        {

        }

    }
}
