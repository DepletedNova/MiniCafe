namespace MiniCafe.Items
{
    public class BigWhipped : CustomItemGroup
    {
        public override string UniqueNameID => "big_whipped";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Whipped");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override string ColourBlindTag => "BMoW";
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, BigMocha>(),
                    GetCastedGDO<Item, WhippedCream>()
                },
                Min = 2,
                Max = 2,
                IsMandatory = true,
            },
        };

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Big Whipped";

            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChild("cream", "Coffee Cup");
            Prefab.ApplyMaterialToChild("chocolate", "Chocolate");
            
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }

    }
}
