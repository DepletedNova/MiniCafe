using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using MiniCafe.Appliances;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Extras
{
    public class Teaspoon : CustomItem
    {
        public override string UniqueNameID => "teaspoon";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Teaspoon");
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, TeaspoonDispenser>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("spoon", "Metal");
        }
    }
}
