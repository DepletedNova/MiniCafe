namespace MiniCafe.Mains.Coffee
{
    public class SmallAmericano : CustomItemGroup
    {
        public override string UniqueNameID => "small_americano";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Americano");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetGDO<Item>(ItemReferences.Water),
                Text = "SAm"
            }
        };
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SmallEspresso>(),
                    GetGDO<Item>(ItemReferences.Water)

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
            Prefab.ApplyMaterialToChildCafe("fill", "Americano", "Coffee - Black");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
            Prefab.ApplyMaterialToChildCafe("Straw", "Plastic - Red", "Plastic - Red");
        }
    }
}
