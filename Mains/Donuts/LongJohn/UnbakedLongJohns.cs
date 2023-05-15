using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class UnbakedLongJohns : CustomItem
    {
        public override string UniqueNameID => "unbaked_long_johns";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Unbaked Long Johns");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 4f,
                Result = GetCastedGDO<Item, BakedLongJohns>()
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Tray", "Metal");
            Prefab.ApplyMaterialToChildren("donut", "Raw Pastry");
        }
    }
}
