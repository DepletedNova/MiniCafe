namespace MiniCafe.Mains.Tea
{
    internal class SmallEarlGrey : CustomItemGroup
    {
        public override string UniqueNameID => "small_earl_grey";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Earl Grey");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, EarlGreySteeped>(),
                Text = "SEG"
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SmallMug>(),
                    GetCastedGDO<Item, EarlGreySteeped>()
                },
                IsMandatory = true,
                Min = 2,
                Max = 2,
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            SmallMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChildCafe("fill", "Earl Grey Tea");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");

            Prefab.ApplyMaterialToChildCafe("Lemon", "Lemon", "Lemon Inner", "White Fruit");
            Prefab.ApplyMaterialToChildCafe("Honey", "Honey");
        }
    }
}
