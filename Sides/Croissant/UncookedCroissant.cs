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
    public class UncookedCroissant : CustomItem
    {
        public override string UniqueNameID => "uncooked_croissant";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Uncooked Croissant");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 1.3f,
                Result = GetCastedGDO<Item, Croissant>()
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("roll", "Raw Pastry");
        }
    }
}
