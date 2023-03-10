namespace MiniCafe.Items
{
    public class BigMocha : CustomItemGroup
    {
        public override string UniqueNameID => "big_mocha";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Mocha");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override string ColourBlindTag => "BMo";
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, BigCappuccino>(),
                    GetCastedGDO<Item, ChocolateSauce>()
                },
                Min = 2,
                Max = 2,
                IsMandatory = true,
            },
        };

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Big Mocha";

            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChild("fill", "Coffee Blend", "Chocolate");
            
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }

    }
}
