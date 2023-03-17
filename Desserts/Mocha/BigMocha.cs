namespace MiniCafe.Desserts
{
    internal class BigMocha : CustomItemGroup<BigMocha.View>
    {
        public override string UniqueNameID => "big_mocha";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Mocha");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override Item DirtiesTo => GetCastedGDO<Item, BigMugDirty>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.MediumLarge;
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
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, WhippedCream>()
                },
                Max = 1,
                Min = 1,
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.GetComponent<View>().Setup(gdo);

            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChildCafe("fill", "Coffee Blend", "Chocolate");

            var cream = Prefab.GetChild("cream");
            cream.ApplyMaterialCafe("Coffee Cup");
            cream.ApplyMaterialToChildCafe("chocolate", "Chocolate");

            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }

        internal class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, WhippedCream>(),
                    GameObject = gameObject.GetChild("cream")
                }
            };
        }
    }
}
