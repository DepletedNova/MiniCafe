namespace MiniCafe.Mains.Tea
{
    public class GroundHibiscus : CustomItem
    {
        public override string UniqueNameID => "ground_hibiscus";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Ground Hibiscus");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

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
            Prefab.ApplyMaterialToChildCafe("mortar", "Stone - Black");
            Prefab.ApplyMaterialToChildCafe("pestle", "Stone - Black");
            Prefab.ApplyMaterialToChildCafe("fill", "Hibiscus Tea");
        }
    }
}
