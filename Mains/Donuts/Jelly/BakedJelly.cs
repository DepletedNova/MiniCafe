using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class BakedJelly : CustomItem
    {
        public override string UniqueNameID => "baked_jelly";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Baked Jelly");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override int SplitCount => 2;
        public override Item SplitSubItem => GetCastedGDO<Item, PlainJelly>();
        public override List<Item> SplitDepletedItems => new() { GetCastedGDO<Item, PlainJelly>() };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Tray", "Metal");

            gdo.AddObjectsSplittableView(Prefab, "donut", "Bread - Inside Cooked");
        }
    }
}
