namespace MiniCafe.Mains.Tea
{
    public class Sage : CustomItem
    {
        public override string UniqueNameID => "sage";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Sage");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, SageProvider>();

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Result = GetCastedGDO<Item, SageSteeped>(),
                Process = GetCastedGDO<Process, SteepProcess>(),
                RequiresWrapper = true
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("sage", "Sage");
        }
    }
}
