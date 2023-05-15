namespace MiniCafe.Extras
{
    internal class UnmixedCreme : CustomItemGroup
    {
        public override string UniqueNameID => "unmixed_creme";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Unmixed Creme");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 1.3f,
                Process = GetGDO<Process>(ProcessReferences.Knead),
                Result = GetCastedGDO<Item, Creme>()
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
                    GetCastedGDO<Item, Butter>(),
                    GetCastedGDO<Item, CookedSugarWater>()
                }
            },
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Pot", "Metal", "Metal Dark", "Metal Dark");
            Prefab.ApplyMaterialToChildCafe("Water", "Soup - Watery");
            Prefab.ApplyMaterialToChildCafe("Butter", "Butter");
        }
    }
}
