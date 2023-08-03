using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Sides
{
    internal class WafelTray : CustomItem
    {
        public override string UniqueNameID => "wafel_tray";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Wafel Tray");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Tray", "Metal");
            Prefab.ApplyMaterialToChildren("Cookie", "Bread - Cooked");
        }
    }
}
