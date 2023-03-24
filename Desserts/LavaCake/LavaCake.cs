namespace MiniCafe.Desserts
{
    internal class LavaCake : CustomItem
    {
        public override string UniqueNameID => "lava_cake";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Lava Cake");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int MaxOrderSharers => 2;
        public override ItemValue ItemValue => ItemValue.Medium;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("cake", "Lava Cake Light", "Lava Cake Dark");
            Prefab.ApplyMaterialToChildCafe("plate", "Plate", "Plate - Ring");
        }
    }
}
