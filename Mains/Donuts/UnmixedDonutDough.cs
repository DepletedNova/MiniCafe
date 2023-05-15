using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class UnmixedDonutDough : CustomItemGroup<UnmixedDonutDough.View>
    {
        public override string UniqueNameID => "unmixed_donut_dough";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Unmixed Donut Dough");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Knead),
                Duration = 1.3f,
                Result = GetCastedGDO<Item, UnbakedJelly>()
            }
        };
        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetGDO<Item>(ItemReferences.Sugar),
                Text = "Su"
            },
            new()
            {
                Item = GetGDO<Item>(ItemReferences.Flour),
                Text = "Fl"
            },
            new()
            {
                Item = GetCastedGDO<Item, MilkIngredient>(),
                Text = "Mi"
            },
        };
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Flour),
                    GetGDO<Item>(ItemReferences.Sugar),
                    GetCastedGDO<Item, MilkIngredient>()
                },
                Max = 3,
                Min = 3,
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChildCafe("Bowl", "Metal - Brass");
            Prefab.ApplyMaterialToChildCafe("Sugar", "Sugar");
            Prefab.ApplyMaterialToChildCafe("Flour", "Flour");
            Prefab.ApplyMaterialToChildCafe("Milk", "Plastic - White");

            Prefab.GetComponent<View>().Setup(gdo);
        }

        public class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    GameObject = gameObject.GetChild("Sugar"),
                    Item = GetGDO<Item>(ItemReferences.Sugar)
                },
                new()
                {
                    GameObject = gameObject.GetChild("Flour"),
                    Item = GetGDO<Item>(ItemReferences.Flour)
                },
                new()
                {
                    GameObject = gameObject.GetChild("Milk"),
                    Item = GetCastedGDO<Item, MilkIngredient>()
                },
            };
        }
    }
}
