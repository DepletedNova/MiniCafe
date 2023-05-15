namespace MiniCafe.Mains.Tea
{
    internal class SmallSage : CustomItemGroup
    {
        public override string UniqueNameID => "small_sage";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Matcha");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override bool AutoCollapsing => true;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, SmallSage>(),
                Text = "SMa"
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SmallMug>(),
                    GetCastedGDO<Item, SageSteeped>()
                },
                IsMandatory = true,
                Min = 2,
                Max = 2,
            },
        };

        public override void OnRegister(ItemGroup gdo)
        {
            SmallMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChildCafe("fill", "Sage Tea");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");

            Prefab.ApplyMaterialToChildCafe("Lemon", "Lemon", "Lemon Inner", "White Fruit");
            Prefab.ApplyMaterialToChildCafe("Honey", "Honey");
        }
    }
}
