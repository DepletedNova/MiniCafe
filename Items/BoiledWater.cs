using KitchenData;
using KitchenLib.Customs;
using MiniCafe.Appliances;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;

namespace MiniCafe.Items
{
    public class BoiledWater : CustomItem
    {
        public override string UniqueNameID => "boiled_water";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Boiler");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, Boiler>();
    }
}
