namespace MiniCafe.Mains.Coffee
{
    internal class PlatedBigAmericano : CustomItemGroup
    {
        public override string UniqueNameID => "plated_big_americano";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Big Americano");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override Item DirtiesTo => GetCastedGDO<Item, BigMugDirty>();
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.MediumLarge;
        public override bool CanContainSide => true;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
            {
                new()
                {
                    Item = GetGDO<Item>(ItemReferences.Water),
                    Text = "BAm"
                }
            };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, BigEspresso>(),
                    GetCastedGDO<Item, Teaspoon>()
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

            BigMug.ApplyMugMaterials(Prefab.GetChild("Mug"));
            Prefab.ApplyMaterialToChildCafe("Filling", "Coffee - Black", "Americano");
            Prefab.ApplyMaterialToChildCafe("Straw", "Plastic - Red", "Plastic - Red");
        }
    }
}
