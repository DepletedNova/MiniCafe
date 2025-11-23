using IngredientLib.Ingredient.Items;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using MiniCafe.Desserts;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.MaterialUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Sides
{
    internal class UncookedWafelTray : CustomItemGroup
    {
        public override string UniqueNameID => "uncooked_wafel_tray";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Dough Tray");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 6f,
                Result = GetCastedGDO<Item, CookedWafelTray>()
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetGDO<Item>(ItemReferences.Dough),
                    GetCastedGDO<Item, Cinnamon>(),
                    GetCastedGDO<Item, EmptyWafelTray>()
                },
                IsMandatory = true,
                Max = 3,
                Min = 3
            }
        };

        public override Item DisposesTo => GetCastedGDO<Item, EmptyWafelTray>();

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("Tray", "Metal Black - Shiny");
            Prefab.ApplyMaterialToChildren("Ball", "Cinnamon");
        }
    }
}
