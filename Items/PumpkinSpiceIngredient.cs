using KitchenData;
using KitchenLib.Customs;
using UnityEngine;

namespace MiniCafe.Items
{
    public class PumpkinSpiceIngredient : CustomItem
    {
        public override string UniqueNameID => "pumpkin_spice_ingredient_split";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Pumpkin Spice");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;
    }
}
