namespace MiniCafe.Items
{
    internal class LemonSlice : CustomItem
    {
        public override string UniqueNameID => "lemon_slice";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Lemon Slice");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("slice", "Lemon", "Lemon Inner");
        }
    }
}
