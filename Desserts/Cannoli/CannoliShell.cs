using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Desserts
{
    public class CannoliShell : CustomItem
    {
        public override string UniqueNameID => "cannoli_shell";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Cannoli Shell");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemValue ItemValue => ItemValue.Small;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("roll", "Cooked Pastry");
        }
    }
}
