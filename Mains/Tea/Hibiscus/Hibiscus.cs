namespace MiniCafe.Mains.Tea
{
    public class Hibiscus : CustomItem
    {
        public override string UniqueNameID => "hibiscus";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Hibiscus");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, HibiscusProvider>();

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Result = GetCastedGDO<Item, HibiscusSteeped>(),
                Process = GetCastedGDO<Process, SteepProcess>(),
                RequiresWrapper = true
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("scoop", "Metal - Brass");
            Prefab.ApplyMaterialToChildren("main", "Hibiscus");
            Prefab.ApplyMaterialToChildCafe("extra", "Hibiscus Extra");
        }
    }
}
