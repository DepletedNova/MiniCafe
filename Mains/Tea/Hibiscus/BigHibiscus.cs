namespace MiniCafe.Mains.Tea
{
    internal class BigHibiscus : CustomItemGroup
    {
        public override string UniqueNameID => "big_hibiscus";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Hibiscus");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override bool AutoCollapsing => true;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, BigHibiscus>(),
                Text = "BHi"
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, BigMug>(),
                    GetCastedGDO<Item, HibiscusSteeped>()
                },
                IsMandatory = true,
                Min = 2,
                Max = 2,
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChildCafe("fill", "Hibiscus Tea");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");

            Prefab.ApplyMaterialToChildCafe("Lemon", "Lemon", "Lemon Inner", "White Fruit");
            Prefab.ApplyMaterialToChildCafe("Honey", "Honey");
        }
    }
}
