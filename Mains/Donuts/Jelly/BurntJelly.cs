using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class BurntJelly : CustomItem
    {
        public override string UniqueNameID => "burnt_jelly";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Burnt Jelly");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Tray", "Metal");
            Prefab.ApplyMaterialToChildren("donut", "Burned");
        }
    }
}
