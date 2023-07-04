namespace MiniCafe.Desserts
{
    public class BurntCannoliTray : CustomItem
    {
        public override string UniqueNameID => "burnt_cannoli_tray";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Burnt Cannoli Tray");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Tray", "Metal");
            Prefab.ApplyMaterialToChildren("Cannoli", "Burned");
        }
    }
}
