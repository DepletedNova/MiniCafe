namespace MiniCafe.Mains.Coffee
{
    internal class PlatedBigIced : CustomItemGroup<PlatedBigIced.View>
    {
        public override string UniqueNameID => "plated_big_iced";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Big Iced");
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
                    Item = GetCastedGDO<Item, Ice>(),
                    Text = "BIc"
                }
            });

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, BigEspresso>(),
                    GetCastedGDO<Item, Ice>()
                },
                IsMandatory = true,
                Max = 2,
                Min = 2,
            },
            ExtrasSet
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyGenericPlated();
            Prefab.GetComponent<View>().Setup(gdo);

            BigMug.ApplyMugMaterials(Prefab.GetChild("Mug"));
            Prefab.ApplyMaterialToChildCafe("Filling", "Coffee - Black");
            Prefab.ApplyMaterialToChildren("Ice", "Ice");
        }

        internal class View : PlatedItemGroupView
        {

        }

    }
}
