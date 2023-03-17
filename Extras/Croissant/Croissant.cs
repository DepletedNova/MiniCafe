namespace MiniCafe.Extras
{
    public class Croissant : CustomItem
    {
        public override string UniqueNameID => "croissant";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Croissant");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override string ColourBlindTag => "Cr";
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                IsBad = true,
                Duration = 6.5f,
                Result = GetGDO<Item>(ItemReferences.BurnedFood)
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildCafe("roll", "Croissant");
        }
    }
}
