using KitchenData;
using KitchenLib.Customs;
using KitchenLib.References;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.MaterialUtils;
using static MiniCafe.Helper;

namespace MiniCafe.Desserts
{
    internal class LavaCake : CustomItem
    {
        public override string UniqueNameID => "lava_cake";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Lava Cake");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int MaxOrderSharers => 1;
        public override ItemValue ItemValue => ItemValue.Small;
        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Duration = 4.5f,
                IsBad = true,
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Result = GetGDO<Item>(ItemReferences.BurnedFood)
            }
        };

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("cake", "Lava Cake Light", "Lava Cake Dark");
            Prefab.ApplyMaterialToChild("plate", "Plate", "Plate - Ring");
        }
    }
}
