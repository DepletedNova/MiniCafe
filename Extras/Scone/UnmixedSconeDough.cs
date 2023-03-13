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
                Duration = 1.8f,
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

        public override void OnRegister(GameDataObject gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal Dark");
            Prefab.ApplyMaterialToChild("Dough", "Egg Dough");
            Prefab.ApplyMaterialToChild("Whipping", "Coffee Cup");
        }
    }
}
