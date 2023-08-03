using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Desserts
{
    public class Cannoli : CustomItem
    {
        public override string UniqueNameID => "cannoli";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Cannoli");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.Small;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("roll", "Cooked Pastry");
            Prefab.ApplyMaterialToChild("filling", "Coffee Cup");
            Prefab.ApplyMaterialToChildren("chocolate", "Chocolate");
        }
    }
}
