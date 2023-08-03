using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using MiniCafe.Appliances;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Items
{
    public class Sprinkles : CustomItem
    {
        public override string UniqueNameID => "sprinkles";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Sprinkles");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override bool IsIndisposable => true;
        public override Appliance DedicatedProvider => GetCastedGDO<Appliance, SprinklesProvider>();

        public override Item SplitSubItem => GetCastedGDO<Item, SprinklesIngredient>();
        public override bool PreventExplicitSplit => true;
        public override bool AllowSplitMerging => true;
        public override int SplitCount => 999;

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Can", "Clothing Pink", "Plastic - White", "Hob Black", "Plastic - Blue");
        }
    }
}
