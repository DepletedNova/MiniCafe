namespace MiniCafe.Desserts
{
    public class Cannoli : CustomItem
    {
        public override string UniqueNameID => "cannoli";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Cannoli");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.Small;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("roll", "Cooked Pastry");
            Prefab.ApplyMaterialToChildCafe("filling", "Coffee Cup");
            Prefab.ApplyMaterialToChildren("chocolate", "Chocolate");
        }
    }
}
