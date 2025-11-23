using IngredientLib.Ingredient.Items;
using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.GDOUtils;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Sides
{
    internal class BurntWafelTray : CustomItem
    {
        public override string UniqueNameID => "stroopwafel_tray";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Burnt Wafel Tray");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override Item DisposesTo => GetCastedGDO<Item, EmptyWafelTray>();

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Tray", "Metal Black - Shiny");
            Prefab.ApplyMaterialToChildren("Cookie", "Burned");
        }
    }
}
