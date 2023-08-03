using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using UnityEngine;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Items
{
    public class LemonSlice : CustomItem
    {
        public override string UniqueNameID => "lemon_slice";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Lemon Slice");
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("slice", "Lemon", "Lemon Inner");
        }
    }
}
