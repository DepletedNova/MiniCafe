using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class BurntLongJohns : CustomItem
    {
        public override string UniqueNameID => "burnt_long_johns";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Burnt Long Johns");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Tray", "Metal");
            Prefab.ApplyMaterialToChildren("donut", "Burned");
        }
    }
}
