using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Sides
{
    public class Stroop : CustomItem
    {
        public override string UniqueNameID => "stroop";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Stroop");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override string ColourBlindTag => "St";

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Bowl", "Metal");
            Prefab.ApplyMaterialToChild("Fill", "Stroop");
        }
    }
}
