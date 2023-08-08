using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.MaterialUtils;
using static KitchenLib.Utils.GDOUtils;

namespace MiniCafe.Desserts
{
    public class Cannoli : CustomItemGroup
    {
        public override string UniqueNameID => "cannoli";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Cannoli");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.Small;

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, CannoliFilling>()
                },
                Min = 1,
                Max = 1,
                IsMandatory = true,
            },
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, CannoliShell>()
                },
                Min = 1,
                Max = 1,
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("roll", "Cooked Pastry");
            Prefab.ApplyMaterialToChild("filling", "Coffee Cup");
        }
    }
}
