using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class PlainLongJohn : CustomItem
    {
        public override string UniqueNameID => "plain_long_john";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plain Long John");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("donut", "Bread - Inside Cooked");
        }
    }
}
