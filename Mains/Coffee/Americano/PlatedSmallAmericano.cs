namespace MiniCafe.Mains.Coffee
{
    internal class PlatedSmallAmericano : CustomItemGroup
    {
        public override string UniqueNameID => "plated_small_americano";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Small Americano");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override Item DirtiesTo => GetCastedGDO<Item, SmallMugDirty>();
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.MediumLarge;
        public override bool CanContainSide => true;

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
                    GetCastedGDO<Item, Teaspoon>(),
                },
                IsMandatory = true,
                Max = 2,
                Min = 2,
            },
            new()
            {
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Water)
                },
                Max = 1,
                Min = 1,
            },
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyGenericPlated();

            SmallMug.ApplyMugMaterials(Prefab.GetChild("Mug"));
            Prefab.ApplyMaterialToChildCafe("Filling", "Coffee - Black", "Americano");
            Prefab.ApplyMaterialToChildCafe("Straw", "Plastic - Red", "Plastic - Red");
        }
    }
}
