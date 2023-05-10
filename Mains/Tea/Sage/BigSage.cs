namespace MiniCafe.Mains.Tea
{
    internal class BigSage : CustomItemGroup
    {
        public override string UniqueNameID => "big_sage";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Sage");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, SageSteeped>(),
                Text = "BSa"
            }
        };

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
                Min = 2,
                Max = 2,
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChildCafe("fill", "Sage Tea");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
            Prefab.ApplyMaterialToChildren("sage", "Sage");

            Prefab.ApplyMaterialToChildCafe("Lemon", "Lemon", "Lemon Inner", "White Fruit");
            Prefab.ApplyMaterialToChildCafe("Honey", "Honey");
        }
    }
}
