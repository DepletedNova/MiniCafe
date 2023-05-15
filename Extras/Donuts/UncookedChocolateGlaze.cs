namespace MiniCafe.Extras
{
    internal class UncookedChocolateGlaze : CustomItemGroup<UncookedChocolateGlaze.View>
    {
        public override string UniqueNameID => "uncooked_chocolate_glaze";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Uncooked Chocolate Glaze");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Item DisposesTo => GetGDO<Item>(ItemReferences.Pot);

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 8.5f,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Result = GetCastedGDO<Item, ChocolateGlaze>()
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Max = 2,
                Min = 2,
                IsMandatory = true,
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Pot),
                    GetCastedGDO<Item, MilkIngredient>()
                }
            },
            new()
            {
                Max = 2,
                Min = 2,
                Items = new()
                {
                    GetCastedGDO<Item, ChoppedChocolate>(),
                    GetGDO<Item>(ItemReferences.Sugar)
                }
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.GetComponent<View>().Setup(gdo);

            Prefab.ApplyMaterialToChildCafe("Pot", "Metal");
            Prefab.ApplyMaterialToChildCafe("Handles", "Metal Dark");
            Prefab.ApplyMaterialToChildCafe("Milk", "Plastic - White");
            Prefab.ApplyMaterialToChildCafe("Chocolate", "Chocolate", "Chocolate", "Chocolate");
            Prefab.ApplyMaterialToChildCafe("Sugar", "Sugar");
        }

        internal class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, ChoppedChocolate>(),
                    GameObject = gameObject.GetChild("Chocolate")
                },
                new()
                {
                    Item = GetGDO<Item>(ItemReferences.Sugar),
                    GameObject = gameObject.GetChild("Sugar")
                }
            };
        }
    }
}
