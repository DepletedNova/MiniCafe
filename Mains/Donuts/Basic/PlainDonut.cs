using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class PlainDonut : CustomItem
    {
        public override string UniqueNameID => "plain_donut";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plain Donut");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("donut", "Bread - Inside Cooked");
        }
    }
}
