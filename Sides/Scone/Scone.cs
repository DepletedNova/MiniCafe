using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Extras
{
    public class Scone : CustomItem
    {
        public override string UniqueNameID => "scone";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Scone");
        public override GameObject SidePrefab => Main.Bundle.LoadAsset<GameObject>("Scone");
        public override bool IsMergeableSide => true;
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override int RewardOverride => 2;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Scone", "Bread - Inside Cooked", "Chocolate");
        }
    }
}
