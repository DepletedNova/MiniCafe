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
