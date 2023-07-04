namespace MiniCafe.Desserts
{
    public class UncookedCannoliTray : CustomItem
    {
        public override string UniqueNameID => "uncooked_cannoli_tray";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Uncooked Cannoli Tray");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 2.5f,
                Result = GetCastedGDO<Item, CookedCannoliTray>()
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Tray", "Metal");
            Prefab.ApplyMaterialToChildren("Cannoli", "Raw Pastry");
        }
    }
}
