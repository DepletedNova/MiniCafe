using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using MiniCafe.Appliances;
using System.Collections.Generic;
using UnityEngine;

namespace MiniCafe.Sides
{
    internal class EmptyWafelTray : CustomItem
    {
        public override string UniqueNameID => "nova.mc_wafel_tray";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Empty Wafel Tray");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, WafelTraySource>();

        public override bool IsIndisposable => true;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Tray", "Metal Black - Shiny");
        }
    }
}
