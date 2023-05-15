using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class UnbakedJelly : CustomItem
    {
        public override string UniqueNameID => "unbaked_jelly";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Unbaked Jelly");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Chop),
                Duration = 0.6f,
                Result = GetCastedGDO<Item, UnbakedDonuts>()
            },
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 4f,
                Result = GetCastedGDO<Item, BakedJelly>()
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Tray", "Metal");
            Prefab.ApplyMaterialToChildren("donut", "Raw Pastry");
        }
    }
}
