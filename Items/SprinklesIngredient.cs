using KitchenData;
using KitchenLib.Customs;
using UnityEngine;

namespace MiniCafe.Items
{
    public class SprinklesIngredient : CustomItem
    {
        public override string UniqueNameID => "sprinkles_ingredient";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Sprinkles");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
    }
}
