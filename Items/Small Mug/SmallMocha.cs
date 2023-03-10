namespace MiniCafe.Items
{
    public class SmallMocha : CustomItemGroup
    {
        public override string UniqueNameID => "small_mocha";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Mocha");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override string ColourBlindTag => "SMo";
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SmallCappuccino>(),
                    GetCastedGDO<Item, ChocolateSauce>()
                },
                Min = 2,
                Max = 2,
                IsMandatory = true,
            },
        };

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Small Mocha";

            SmallMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChild("fill", "Coffee Blend", "Chocolate");

            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }

    }
}
