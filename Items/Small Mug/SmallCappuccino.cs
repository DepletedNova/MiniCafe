namespace MiniCafe.Items
{
    public class SmallCappuccino : CustomItemGroup
    {
        public override string UniqueNameID => "small_cappuccino";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Cappuccino");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.Small;
        public override Item DirtiesTo => GetCastedGDO<Item, SmallMugDirty>();
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
                IsMandatory = false,
                OrderingOnly = false,
                RequiresUnlock = false,
            },
        };

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Small Cappuccino";

            SmallMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChild("fill", "Coffee Blend", "Coffee Foam");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }
    }
}
