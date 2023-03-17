namespace MiniCafe.Mains
{
    public class SmallCappuccino : CustomItemGroup
    {
        public override string UniqueNameID => "small_cappuccino";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Cappuccino");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override string ColourBlindTag => "SCa";
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SteamedMilk>(),
                    GetCastedGDO<Item, SmallEspresso>()
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
            SmallMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChildCafe("fill", "Coffee Blend", "Coffee Foam");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }
    }
}
