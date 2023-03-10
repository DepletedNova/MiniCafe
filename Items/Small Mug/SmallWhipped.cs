namespace MiniCafe.Items
{
    public class SmallWhipped : CustomItemGroup
    {
        public override string UniqueNameID => "small_whipped";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Whipped");
        public override Item DisposesTo => GetCastedGDO<Item, SmallMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override string ColourBlindTag => "SMoW";
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SmallMocha>(),
                    GetCastedGDO<Item, WhippedCream>()
                },
                Min = 2,
                Max = 2,
                IsMandatory = true,
            },
        };

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Small Whipped";

            SmallMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChild("cream", "Coffee Cup");
            Prefab.ApplyMaterialToChild("chocolate", "Chocolate");

            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");
        }

    }
}
