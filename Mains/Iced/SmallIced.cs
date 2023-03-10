namespace MiniCafe.Mains
{
    public class SmallIced : CustomItemGroup
    {
        public override string UniqueNameID => "small_iced_coffee";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Ice Espresso");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override string ColourBlindTag => "SIc";
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

        public override void OnRegister(GameDataObject gdo)
        {
            SmallMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChild("fill", "Coffee - Black");
            Prefab.ApplyMaterialToChildren("Ice", "Ice");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }
    }
}
