namespace MiniCafe.Desserts
{
    public class CannoliFilling : CustomItem
    {
        public override string UniqueNameID => "cannoli_filling";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Cannoli Filling");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Bowl", "Metal");
            Prefab.ApplyMaterialToChildCafe("Fill", "Coffee Cup");
            Prefab.ApplyMaterialToChildren("Chocolate", "Chocolate");
        }
    }
}
