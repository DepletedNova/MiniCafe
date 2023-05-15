using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class Jelly : CustomItemGroup<Jelly.View>
    {
        public override string UniqueNameID => "jelly";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Jelly");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, CremeIngredient>(),
                Text = "Cr"
            },
            new()
            {
                Item = GetCastedGDO<Item, Ganache>(),
                Text = "Ga"
            },
            new()
            {
                Item = GetCastedGDO<Item, Caramel>(),
                Text = "Ca"
            },
            new()
            {
                Item = GetCastedGDO<Item, ChocolateGlazeIngredient>(),
                Text = "Ch"
            },
            new()
            {
                Item = GetCastedGDO<Item, GlazeIngredient>(),
                Text = "Gl"
            },
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
                    GetCastedGDO<Item, PlainJelly>()
                }
            },
            new()
            {
                Max = 1,
                Min = 1,
                RequiresUnlock = true,
                Items = new()
                {
                    GetCastedGDO<Item, Ganache>(),
                    GetCastedGDO<Item, CremeIngredient>()
                }
            },
            new()
            {
                Max = 1,
                Min = 0,
                Items = new()
                {
                    GetCastedGDO<Item, Caramel>(),
                    GetCastedGDO<Item, ChocolateGlazeIngredient>(),
                    GetCastedGDO<Item, GlazeIngredient>(),
                }
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.TryAddComponent<View>().Setup(gdo);

            Prefab.ApplyMaterialToChildCafe("Donut", "Bread - Inside Cooked");
            Prefab.ApplyMaterialToChildCafe("Chocolate", "Chocolate");
            Prefab.ApplyMaterialToChildCafe("Caramel", "Caramel");
            Prefab.ApplyMaterialToChildCafe("Glazed", "Bread - Inside");
            Prefab.ApplyMaterialToChildCafe("Creme", "Plastic");
            Prefab.ApplyMaterialToChildCafe("Ganache", "Chocolate Light");
        }

        public class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, CremeIngredient>(),
                    GameObject = gameObject.GetChild("Creme")
                },
                new()
                {
                    Item = GetCastedGDO<Item, Ganache>(),
                    GameObject = gameObject.GetChild("Ganache")
                },
                new()
                {
                    Item = GetCastedGDO<Item, Caramel>(),
                    GameObject = gameObject.GetChild("Caramel")
                },
                new()
                {
                    Item = GetCastedGDO<Item, ChocolateGlazeIngredient>(),
                    GameObject = gameObject.GetChild("Chocolate")
                },
                new()
                {
                    Item = GetCastedGDO<Item, GlazeIngredient>(),
                    GameObject = gameObject.GetChild("Glazed")
                },
            };
        }
    }
}
