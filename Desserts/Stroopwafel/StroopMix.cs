namespace MiniCafe.Sides
{
    public class StroopMix : CustomItemGroup
    {
        public override string UniqueNameID => "stroop_mix";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Stroop Mix");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 2f,
                Result = GetCastedGDO<Item, Stroop>()
            }
        };
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Sugar),
                    GetCastedGDO<Item, SyrupIngredient>(),
                },
                Max = 2,
                Min = 2
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Bowl", "Metal");
            Prefab.ApplyMaterialToChildCafe("Syrup", "Sugar");
            Prefab.ApplyMaterialToChildCafe("Sugar", "Cooked Batter");
        }
    }
}
