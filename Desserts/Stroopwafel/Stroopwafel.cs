namespace MiniCafe.Sides
{
    public class Stroopwafel : CustomItem
    {
        public override string UniqueNameID => "stroopwafel";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Stroopwafel");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("wafel", "Bread - Cooked", "Stroop");
            Prefab.ApplyMaterialToChildCafe("plate", "Plate", "Plate - Ring");
        }
    }
}
