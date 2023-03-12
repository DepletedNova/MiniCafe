namespace MiniCafe.Desserts
{
    public class SmallMocha : CustomItemGroup<SmallMocha.View>
    {
        public override string UniqueNameID => "small_mocha";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Mocha");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.MediumLarge;
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SmallCappuccino>(),
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
                Min = 0,
                RequiresUnlock = true
            }
        };

        public override void OnRegister(GameDataObject gdo)
        {
            Prefab.GetComponent<View>().Setup(gdo);

            SmallMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChild("fill", "Coffee Blend", "Chocolate");

            var cream = Prefab.GetChild("cream");
            cream.ApplyMaterial("Coffee Cup");
            cream.ApplyMaterialToChild("chocolate", "Chocolate");

            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }

        public class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, WhippedCream>(),
                    GameObject = gameObject.GetChild("cream")
                }
            };
            protected override List<ColourBlindLabel> labels => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, SmallMocha>(),
                    Text = "SMo"
                },
                new()
                {
                    Item = GetCastedGDO<Item, WhippedCream>(),
                    Text = "W"
                }
            };
        }
    }
}
