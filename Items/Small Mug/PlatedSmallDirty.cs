namespace MiniCafe.Items
{
    public class PlatedSmallDirty : CustomItem
    {
        public override string UniqueNameID => "plated_small_dirty";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Small Dirty");
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int SplitCount => 1;
        public override float SplitSpeed => 5f;
        public override Item SplitSubItem => GetCastedGDO<Item, SmallMugDirty>();
        public override List<Item> SplitDepletedItems => new() { GetGDO<Item>(ItemReferences.Plate) };
        public override bool IsIndisposable => true;

        public override void OnRegister(GameDataObject gdo)
        {
            SmallMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChild("dirty_mug", "Plate - Dirty Food");
            Prefab.ApplyMaterialToChild("Plate", "Plate", "Plate - Ring");
        }
    }
}
