namespace MiniCafe.Mains
{
    public class BigIced : CustomItemGroup
    {
        public override string UniqueNameID => "big_iced_coffee";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Ice Espresso");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override string ColourBlindTag => "BIc";
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, BigEspresso>(),
                    GetCastedGDO<Item, Ice>()

                },
                Min = 2,
                Max = 2,
            },
        };

        public override void OnRegister(ItemGroup gdo)
        {
            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChildCafe("fill", "Coffee - Black");
            Prefab.ApplyMaterialToChildren("Ice", "Ice");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }
    }
}
