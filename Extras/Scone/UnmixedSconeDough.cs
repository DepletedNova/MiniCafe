namespace MiniCafe.Extras
{
    public class UnmixedSconeDough : CustomItemGroup
    {
        public override string UniqueNameID => "unmixed_scone";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Unmixed Scone Dough");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Knead),
                Duration = 1.3f,
                Result = GetCastedGDO<Item, UncookedScones>()
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Max = 2,
                Min = 2,
                Items = new()
                {
                    GetCastedGDO<Item, EggDough>(),
                    GetCastedGDO<Item, WhippingCreamIngredient>()
                }
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Bowl", "Metal Dark");
            Prefab.ApplyMaterialToChildCafe("Dough", "Egg Dough");
            Prefab.ApplyMaterialToChildCafe("Whipping", "Coffee Cup");
        }
    }
}
