namespace MiniCafe.Mains.Tea
{
    internal class BigEarlGrey : CustomItemGroup
    {
        public override string UniqueNameID => "big_earl_grey";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Earl Grey");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override bool AutoCollapsing => true;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, BigEarlGrey>(),
                Text = "BEG"
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, BigMug>(),
                    GetCastedGDO<Item, EarlGreySteeped>()
                },
                IsMandatory = true,
                Min = 2,
                Max = 2,
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChildCafe("fill", "Earl Grey Tea");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");

            Prefab.ApplyMaterialToChildCafe("Lemon", "Lemon", "Lemon Inner", "White Fruit");
            Prefab.ApplyMaterialToChildCafe("Honey", "Honey");
        }
    }
}
