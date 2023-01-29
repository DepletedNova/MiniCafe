namespace MiniCafe.Items
{
    public class SmallMugDirty : CustomItem
    {
        public override string UniqueNameID => "small_dirty_mug";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Small Mug");
        public override bool IsIndisposable => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 1.5f,
                Process = GetGDO<Process>(ProcessReferences.Clean),
                Result = GetCastedGDO<Item, SmallMug>()
            }
        };

        public override void OnRegister(GameDataObject gdo)
        {
            gdo.name = "Small Dirty Mug";
        }
    }
}
