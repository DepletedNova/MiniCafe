using MiniCafe.Sides;

namespace MiniCafe.Mains
{
    internal class PlatedDonut : CustomItemGroup<PlatedDonut.View>
    {
        public override string UniqueNameID => "plated_donut";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Plated Donut");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.Medium;

        public override bool CanContainSide => true;
        public override Item DisposesTo => GetGDO<Item>(ItemReferences.Plate);
        public override Item DirtiesTo => GetGDO<Item>(ItemReferences.PlateDirty);

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, PlainDonut>(),
                Text = "Do"
            },
            new()
            {
                Item = GetCastedGDO<Item, PlainLongJohn>(),
                Text = "LJ"
            },
            new()
            {
                Item = GetCastedGDO<Item, PlainJelly>(),
                Text = "Fi"
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
                Text = "G"
            },
            new()
            {
                Item = GetCastedGDO<Item, SprinklesIngredient>(),
                Text = "S"
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
                    GetGDO<Item>(ItemReferences.Plate)
                }
            },
            new()
            {
                Max = 1,
                Min = 1,
                IsMandatory = true,
                RequiresUnlock = true,
                Items = new()
                {
                    GetCastedGDO<Item, PlainDonut>(),
                    GetCastedGDO<Item, PlainLongJohn>(),
                    GetCastedGDO<Item, PlainJelly>()
                }
            },
            new()
            {
                Max = 1,
                Min = 0,
                RequiresUnlock = true,
                Items = new()
                {
                    GetCastedGDO<Item, SprinklesIngredient>()
                }
            },
            new()
            {
                Max = 1,
                Min = 0,
                RequiresUnlock = true,
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

            Prefab.ApplyMaterialToChildCafe("Plate", "Plate", "Plate - Ring");
            Prefab.ApplyMaterialToChildren("Donut", "Bread - Inside Cooked");

            for (int i = 0; i < Prefab.GetChildCount(); i++)
            {
                var child = Prefab.GetChild(i);
                if (!child.name.Contains("Donut"))
                    continue;

                child.ApplyMaterial("Bread - Inside Cooked");
                child.ApplyMaterialToChildCafe("Chocolate", "Chocolate");
                child.ApplyMaterialToChildCafe("Caramel", "Caramel");
                child.ApplyMaterialToChildCafe("Glazed", "Bread - Inside");
                child.ApplyMaterialToChildCafe("Sprinkles", "Clothing Pink", "Blueberry", "Plastic - White");
            }

            Prefab.ApplyMaterialToChild("Donut J/Creme", "Plastic - Light Yellow");
        }

        public class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, PlainDonut>(),
                    GameObject = gameObject.GetChild("Donut O")
                },
                new()
                {
                    Item = GetCastedGDO<Item, PlainJelly>(),
                    GameObject = gameObject.GetChild("Donut J")
                },
                new()
                {
                    Item = GetCastedGDO<Item, PlainLongJohn>(),
                    GameObject = gameObject.GetChild("Donut L")
                },
                new()
                {
                    Item = GetCastedGDO<Item, SprinklesIngredient>(),
                    Objects = GetDescendantsOfName("Sprinkles"),
                    DrawAll = true
                },
                new()
                {
                    Item = GetCastedGDO<Item, Caramel>(),
                    Objects = GetDescendantsOfName("Caramel"),
                    DrawAll = true,
                },
                new()
                {
                    Item = GetCastedGDO<Item, ChocolateGlazeIngredient>(),
                    Objects = GetDescendantsOfName("Chocolate"),
                    DrawAll = true,
                },
                new()
                {
                    Item = GetCastedGDO<Item, GlazeIngredient>(),
                    Objects = GetDescendantsOfName("Glazed"),
                    DrawAll = true,
                },
            };

            private List<GameObject> GetDescendantsOfName(string name)
            {
                List<GameObject> objects = new();
                for (int i = 0; i < gameObject.GetChildCount(); i++)
                {
                    var donut = gameObject.GetChild(i);
                    if (!donut.name.Contains("Donut"))
                        continue;

                    for (int i2 = 0; i2 < donut.GetChildCount(); i2++)
                    {
                        var topping = donut.GetChild(i2);
                        if (topping.name.Contains(name))
                            objects.Add(topping);
                    }
                }
                return objects;
            }
        }
    }
}
