namespace MiniCafe.Mains.Tea
{
    public class GroundMatcha : CustomItem
    {
        public override string UniqueNameID => "ground_matcha";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Ground Matcha");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

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
            Prefab.ApplyMaterialToChildCafe("mortar", "Stone - Black");
            Prefab.ApplyMaterialToChildCafe("pestle", "Stone - Black");
            Prefab.ApplyMaterialToChildCafe("fill", "Sage");
        }
    }
}
