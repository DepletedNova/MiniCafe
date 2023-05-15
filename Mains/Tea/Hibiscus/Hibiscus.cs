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
                Result = GetCastedGDO<Item, GroundHibiscus>(),
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Duration = 0.5f
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("flower", "AppleRed", "AppleRed");
        }
    }
}
