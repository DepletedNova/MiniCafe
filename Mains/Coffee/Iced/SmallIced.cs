namespace MiniCafe.Mains.Coffee
{
    public class SmallIced : CustomItemGroup
    {
        public override string UniqueNameID => "small_iced_coffee";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Ice Espresso");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, Ice>(),
                Text = "SIc"
            }
        };
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SmallEspresso>(),
                    GetCastedGDO<Item, Ice>()

                },
                Min = 2,
                Max = 2,
            },
        };

        public override void OnRegister(ItemGroup gdo)
        {
            SmallMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChildCafe("fill", "Coffee - Black");
            Prefab.ApplyMaterialToChildren("Ice", "Ice");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }
    }
}
