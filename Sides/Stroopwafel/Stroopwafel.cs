namespace MiniCafe.Sides
{
    public class Stroopwafel : CustomItemGroup
    {
        public override string UniqueNameID => "stroopwafel";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Stroopwafel");
        public override GameObject SidePrefab => Main.Bundle.LoadAsset<GameObject>("Stroopwafel");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override bool IsMergeableSide => true;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, Stroop>(),
                Text = "S"
            },
            new()
            {
                Item = GetCastedGDO<Item, Wafel>(),
                Text = "W"
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, Stroop>(),
                    GetCastedGDO<Item, Wafel>()
                },
                Max = 2,
                Min = 2
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChildCafe("wafel", "Cooked Pastry", "Bread - Cooked", "Stroop");
        }
    }
}
