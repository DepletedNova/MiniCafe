﻿namespace MiniCafe.Desserts
{
    public class CookedCannoliTray : CustomItem
    {
        public override string UniqueNameID => "cooked_cannoli_tray";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Cooked Cannoli Tray");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 6.5f,
                Result = GetCastedGDO<Item, BurntCannoliTray>(),
                IsBad = true
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Tray", "Metal");
            Prefab.ApplyMaterialToChildren("Cannoli", "Cooked Pastry");
        }
    }
}