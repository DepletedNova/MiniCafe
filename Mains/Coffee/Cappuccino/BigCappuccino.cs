namespace MiniCafe.Mains.Coffee
{
    public class BigCappuccino : CustomItemGroup
    {
        public override string UniqueNameID => "big_cappuccino";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Cappuccino");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, SteamedMilk>(),
                Text = "BCa"
            }
        };
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

        public override void OnRegister(ItemGroup gdo)
        {
            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChildCafe("fill", "Coffee Blend", "Coffee Foam");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }
    }
}
