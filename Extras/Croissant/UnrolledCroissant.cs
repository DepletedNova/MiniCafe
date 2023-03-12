namespace MiniCafe.Extras
{
    public class UnrolledCroissant : CustomItemGroup
    {
        public override string UniqueNameID => "unrolled_croissant";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Unwrapped Croissant");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int SplitCount => 1;
        public override float SplitSpeed => 0.8f;
        public override Item SplitSubItem => GetCastedGDO<Item, UncookedCroissant>();
        public override List<Item> SplitDepletedItems => new() { GetCastedGDO<Item, UncookedCroissant>() };

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

        public override void OnRegister(GameDataObject gdo)
        {
            Prefab.ApplyMaterialToChild("Dough", "Raw Pastry");
            Prefab.ApplyMaterialToChild("Butter", "Butter");
        }
    }
}
