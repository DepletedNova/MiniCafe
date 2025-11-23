using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;

namespace MiniCafe.Desserts
{
    public class Wafel : CustomItem
    {
        public override string UniqueNameID => "nova.mc_wafel";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Wafel");
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("wafel", "Bread - Cooked");
            Prefab.ApplyMaterialToChild("plate", "Plate", "Plate - Ring");
        }
    }
}
