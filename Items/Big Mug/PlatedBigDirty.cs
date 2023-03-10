namespace MiniCafe.Items
{
    public class PlatedBigDirty : CustomItem
    {
        public override string UniqueNameID => "plated_big_dirty";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Big Dirty");
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int SplitCount => 1;
        public override float SplitSpeed => 5f;
        public override Item SplitSubItem => GetCastedGDO<Item, BigMugDirty>();
        public override List<Item> SplitDepletedItems => new() { GetGDO<Item>(ItemReferences.Plate) };
        public override bool IsIndisposable => true;

        public override void OnRegister(GameDataObject gdo)
        {
            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChild("dirty_mug", "Plate - Dirty Food");
            Prefab.ApplyMaterialToChild("Plate", "Plate", "Plate - Ring");
        }
    }
}
