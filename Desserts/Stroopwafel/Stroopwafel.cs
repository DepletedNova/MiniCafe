using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Sides
{
    public class Stroopwafel : CustomItem
    {
        public override string UniqueNameID => "stroopwafel";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Stroopwafel");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("wafel", "Bread - Cooked", "Stroop");
            Prefab.ApplyMaterialToChild("plate", "Plate", "Plate - Ring");
        }
    }
}
