using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class BakedJelly : CustomItem
    {
        public override string UniqueNameID => "baked_jelly";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Baked Jelly");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Result = GetCastedGDO<Item, BurntJelly>(),
                Duration = 10,
                IsBad = true,
                Process = GetGDO<Process>(ProcessReferences.Cook)
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Tray", "Metal");

            gdo.AddObjectsSplittableView(Prefab, "donut", "Bread - Inside Cooked");
        }
    }
}
