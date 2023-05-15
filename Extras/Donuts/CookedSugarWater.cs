namespace MiniCafe.Extras
{
    internal class CookedSugarWater : CustomItem
    {
        public override string UniqueNameID => "sugar_water";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Cooked Sugar Water");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Pot", "Metal", "Metal Dark", "Metal Dark");
            Prefab.ApplyMaterialToChildCafe("Water", "Soup - Watery");
        }
    }
}
