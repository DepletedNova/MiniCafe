namespace MiniCafe.Desserts
{
    internal class LavaCakeMix : CustomItemGroup
    {
        public override string UniqueNameID => "lava_cake_mix";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Lava Cake Mix");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 2f,
                Result = GetCastedGDO<Item, LavaCake>()
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Max = 2,
                Min = 2,
                Items = new()
                {
                    GetCastedGDO<Item, ChocolateShavings>(),
                    GetCastedGDO<Item, ThinBatter>(),
                }
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Bowl", "Metal Dark");
            Prefab.ApplyMaterialToChildCafe("Fill", "Flour", "Raw Pastry");
            Prefab.ApplyMaterialToChildren("Chocolate", "Chocolate");
        }
    }
}
