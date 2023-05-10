namespace MiniCafe.Sides
{
    public class Wafel : CustomItem
    {
        public override string UniqueNameID => "wafel";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Wafel");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override string ColourBlindTag => "Wa";

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("wafel", "Cooked Pastry", "Bread - Cooked");
        }
    }
}
