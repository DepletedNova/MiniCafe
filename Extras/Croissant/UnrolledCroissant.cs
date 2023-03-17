namespace MiniCafe.Extras
{
    public class UnrolledCroissant : CustomItemGroup
    {
        public override string UniqueNameID => "unrolled_croissant";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Unwrapped Croissant");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Knead),
                Duration = 0.75f,
                Result = GetCastedGDO<Item, UncookedCroissant>()
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                IsMandatory = true,
                Max = 2,
                Min = 2,
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Dough),
                    GetCastedGDO<Item, Butter>(),
                }
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Dough", "Raw Pastry");
            Prefab.ApplyMaterialToChildCafe("Butter", "Butter");
        }
    }
}
