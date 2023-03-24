namespace MiniCafe.Desserts
{
    internal class LavaCakeMix : CustomItemGroup<LavaCakeMix.View>
    {
        public override string UniqueNameID => "lava_cake_mix";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Lava Cake Mix");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 5.5f,
                Result = GetCastedGDO<Item, LavaCake>()
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Max = 1,
                Min = 1,
                IsMandatory = true,
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.EggCracked)
                }
            },
            new()
            {
                Max = 2,
                Min = 2,
                Items = new()
                {
                    GetCastedGDO<Item, ChocolateShavings>(),
                    GetCastedGDO<Item, Butter>()
                }
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.GetComponent<View>().Setup(gdo);

            Prefab.ApplyMaterialToChildCafe("Bowl", "Metal Dark");
            Prefab.ApplyMaterialToChildCafe("Whites", "Egg - White");
            Prefab.ApplyMaterialToChildCafe("Yolk", "Egg - Yolk");
            Prefab.ApplyMaterialToChildCafe("Butter", "Butter");
            Prefab.ApplyMaterialToChildCafe("Chocolate", "Chocolate");
        }

        internal class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, ChocolateShavings>(),
                    GameObject = gameObject.GetChild("Chocolate")
                },
                new()
                {
                    Item = GetCastedGDO<Item, Butter>(),
                    GameObject = gameObject.GetChild("Butter")
                }
            };
        }
    }
}
