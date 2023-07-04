using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class BurntDonuts : CustomItem
    {
        public override string UniqueNameID => "burnt_donuts";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Burnt Donuts");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Tray", "Metal");
            Prefab.ApplyMaterialToChildren("donut", "Burned");
        }
    }
}
