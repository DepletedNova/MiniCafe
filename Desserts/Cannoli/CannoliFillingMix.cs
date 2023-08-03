using IngredientLib.Ingredient.Items;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.MaterialUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Desserts
{
    internal class CannoliFillingMix : CustomItemGroup<CannoliFillingMix.View>
    {
        public override string UniqueNameID => "cannoli_filling_mix";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Cannoli Filling Mix");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Knead),
                Duration = 1.3f,
                Result = GetCastedGDO<Item, CannoliFilling>()
            }
        };

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, WhippingCreamIngredient>(),
                Text = "Wh"
            },
            new()
            {
                Item = GetGDO<Item>(ItemReferences.Sugar),
                Text = "S"
            },
            new()
            {
                Item = GetCastedGDO<Item, ChoppedChocolate>(),
                Text = "Cho"
            },
            new()
            {
                Item = GetGDO<Item>(ItemReferences.CheeseGrated),
                Text = "Che"
            },
        };
        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Sugar),
                    //GetCastedGDO<Item, WhippingCreamIngredient>(),
                    GetCastedGDO<Item, ChoppedChocolate>(),
                    GetGDO<Item>(ItemReferences.CheeseGrated)
                },
                Max = 3,
                Min = 3,
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal");
            Prefab.ApplyMaterialToChild("Sugar", "Sugar");
            Prefab.ApplyMaterialToChild("Whipping", "Coffee Cup");
            Prefab.ApplyMaterialToChild("Cheese", "Cheese - Default");
            Prefab.GetChild("Chocolate").ApplyMaterialToChildren("Chocolate", "Chocolate");

            Prefab.GetComponent<View>().Setup(gdo);
        }

        public class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    GameObject = gameObject.GetChild("Whipping"),
                    Item = GetCastedGDO<Item, WhippingCreamIngredient>()
                },
                new()
                {
                    GameObject = gameObject.GetChild("Sugar"),
                    Item = GetGDO<Item>(ItemReferences.Sugar)
                },
                new()
                {
                    GameObject = gameObject.GetChild("Chocolate"),
                    Item = GetCastedGDO<Item, ChoppedChocolate>()
                },
                new()
                {
                    GameObject = gameObject.GetChild("Cheese"),
                    Item = GetGDO<Item>(ItemReferences.CheeseGrated)
                },
            };
        }
    }
}
