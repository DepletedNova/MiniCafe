using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;

namespace MiniCafe.Desserts
{
    internal class CannoliDoughPot : CustomItemGroup<ItemGroupView>
    {
        public override string UniqueNameID => "Cannoli Dough Pot";
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override Item DisposesTo => GetGDO<Item>(ItemReferences.Pot);

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetGDO<Item>(1719428613),
                },
                IsMandatory = true,
                Max = 1,
                Min = 1,
            },
            new()
            {
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.PizzaCrust)
                },
                Max = 1,
                Min = 1
            }
        };

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Result = GetCastedGDO<Item, CannoliShellPot>(),
                Duration = 3.5f,
                Process = GetGDO<Process>(ProcessReferences.Cook)
            }
        };

        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Cannoli Shells");
        public override void SetupPrefab(GameObject prefab)
        {
            prefab.ApplyMaterialToChild("Pot/Base", "Metal");
            prefab.ApplyMaterialToChild("Pot/Handle", "Metal Dark");
            prefab.ApplyMaterialToChild("Oil", "Frying Oil");

            prefab.GetChild("Shells").ApplyMaterialToChildren("", "Raw Pastry");
            prefab.GetChild("Burned").ApplyMaterialToChildren("", "Burned - Light");

            prefab.TryAddComponent<ItemGroupView>().ComponentGroups = new()
            {
                new()
                {
                    GameObject = prefab.GetChild("Shells"),
                    Item = GetCastedGDO<Item, CannoliDoughPot>()
                },
                new()
                {
                    GameObject = prefab.GetChild("Burned"),
                    Item = GetCastedGDO<Item, BurnedCannoliPot>()
                }
            };
        }
    }
}
