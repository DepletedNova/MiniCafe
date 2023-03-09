namespace MiniCafe.Items
{
    public class BigMugDirty : CustomItem
    {
        public override string UniqueNameID => "big_dirty_mug";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Dirty Big Mug");
        public override bool IsIndisposable => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 1.5f,
                Process = GetGDO<Process>(ProcessReferences.Clean),
                Result = GetCastedGDO<Item, BigMug>()
            }
        };

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Big Dirty Mug";

            // Materials
            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChild("dirty_mug", "Plate - Dirty Food");
        }
    }
}
