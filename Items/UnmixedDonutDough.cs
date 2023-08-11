using IngredientLib.Ingredient.Items;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using MiniCafe.Extras;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.MaterialUtils;
using static MiniCafe.Helper;

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
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 3.5f,
                Result = GetCastedGDO<Item, SconePlatter>()
            }
        };
        public override List<ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, SweetDough>(),
                Text = "SD"
            },
            new()
            {
                Item = GetGDO<Item>(MilkItem),
                Text = "M"
            }
        };
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, SweetDough>(),
                    GetGDO<Item>(MilkItem)
                },
                Max = 2,
                Min = 2,
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal - Brass");
            Prefab.ApplyMaterialToChild("Dough", "Raw Pastry");
            Prefab.ApplyMaterialToChild("Milk", "Plastic - White");

            Prefab.GetComponent<View>().Setup(gdo);
        }

        public class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    GameObject = gameObject.GetChild("Dough"),
                    Item = GetCastedGDO<Item, SweetDough>(),
                },
                new()
                {
                    GameObject = gameObject.GetChild("Milk"),
                    Item = GetGDO<Item>(MilkItem)
                },
            };
        }
    }
}
