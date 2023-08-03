using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.MaterialUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Extras
{
    public class UncookedScones : CustomItem, IWontRegister
    {
        public override string UniqueNameID => "uncooked_scones";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Uncooked Scones");
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 3.0f,
                Result = GetCastedGDO<Item, SconePlatter>()
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChildren("Scone", "Raw Pastry");
        }
    }
}
