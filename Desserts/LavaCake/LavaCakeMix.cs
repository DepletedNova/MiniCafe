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
                Duration = 3f,
                Result = GetCastedGDO<Item, LavaCake>()
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Max = 3,
                Min = 3,
                Items = new()
                {
                    GetCastedGDO<Item, ChocolateShavings>(),
                    GetCastedGDO<Item, Butter>(),
                    GetGDO<Item>(ItemReferences.EggCracked)
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
                },
                new()
                {
                    Item = GetGDO<Item>(ItemReferences.EggCracked),
                    Objects = new()
                    {
                        gameObject.GetChild("Whites"),
                        gameObject.GetChild("Yolk")
                    },
                    DrawAll = true
                }
            };
        }
    }
}
