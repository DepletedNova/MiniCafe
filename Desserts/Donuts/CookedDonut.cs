namespace MiniCafe.Desserts
{
    internal class CookedDonut : CustomItem
    {
        public override string UniqueNameID => "cooked_donut";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Cooked Donut");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
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
            Prefab.ApplyMaterialToChildCafe("Donut", "Bread - Inside Cooked");
        }
    }
}
