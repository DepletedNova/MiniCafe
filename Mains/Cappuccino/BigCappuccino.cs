namespace MiniCafe.Mains
{
    public class BigCappuccino : CustomItemGroup
    {
        public override string UniqueNameID => "big_cappuccino";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Cappuccino");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override string ColourBlindTag => "BCa";
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SteamedMilk>(),
                    GetCastedGDO<Item, BigEspresso>()
                },
                Min = 2,
                Max = 2,
                IsMandatory = true,
                OrderingOnly = false,
                RequiresUnlock = false,
            },
        };

        public override void OnRegister(GameDataObject gdo)
        {
            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChild("fill", "Coffee Blend", "Coffee Foam");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }
    }
}
