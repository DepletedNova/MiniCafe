namespace MiniCafe.Desserts
{
    internal class UncookedDonut : CustomItemGroup
    {
        public override string UniqueNameID => "uncooked_donut";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Uncooked Donut");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 0.65f,
                Result = GetCastedGDO<Item, CookedDonut>()
            }
        };
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Dough),
                    GetCastedGDO<Item, Butter>(),
                },
                Max = 2,
                Min = 2,
                IsMandatory = true
            },
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, MilkIngredient>()
                },
                Max = 1,
                Min = 1,
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Donut", "Raw Pastry");
        }
    }
}
