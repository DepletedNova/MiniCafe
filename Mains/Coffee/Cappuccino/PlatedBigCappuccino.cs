namespace MiniCafe.Mains.Coffee
{
    internal class PlatedBigCappuccino : CustomItemGroup
    {
        public override string UniqueNameID => "plated_big_cappuccino";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Big Cappuccino");
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
                    GetCastedGDO<Item, SteamedMilk>()
                },
                Max = 1,
                Min = 1,
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyGenericPlated();

            BigMug.ApplyMugMaterials(Prefab.GetChild("Mug"));
            Prefab.ApplyMaterialToChildCafe("Filling", "Coffee Blend", "Coffee Foam");
            Prefab.ApplyMaterialToChildCafe("Napkin", "Cloth - Blue");
        }
    }
}
