namespace MiniCafe.Sides
{
    internal class UncookedWafelTray : CustomItem
    {
        public override string UniqueNameID => "uncooked_wafel_tray";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Wafel Tray");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 4.5f,
                Result = GetCastedGDO<Item, WafelTray>()
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Tray", "Metal");
            Prefab.ApplyMaterialToChildCafe("Fill", "Raw Pastry");
        }
    }
}
