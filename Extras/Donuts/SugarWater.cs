namespace MiniCafe.Extras
{
    internal class SugarWater : CustomItemGroup
    {
        public override string UniqueNameID => "uncooked_sugar_water";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Sugar Water");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 4.5f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Result = GetCastedGDO<Item, CookedSugarWater>()
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
                    GetGDO<Item>(ItemReferences.Water),
                    GetGDO<Item>(ItemReferences.Sugar)
                }
            },
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Pot", "Metal", "Metal Dark", "Metal Dark");
            Prefab.ApplyMaterialToChildCafe("Water", "Water");
            Prefab.ApplyMaterialToChildCafe("Sugar", "Sugar");
        }
    }
}
