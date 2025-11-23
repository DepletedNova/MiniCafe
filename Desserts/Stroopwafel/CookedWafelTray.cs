using Kitchen;
using KitchenData;
using KitchenLib.Customs;
using KitchenLib.Utils;
using MiniCafe.Desserts;
using System.Collections.Generic;
using UnityEngine;
using static KitchenLib.Utils.MaterialUtils;

namespace MiniCafe.Sides
{
    internal class CookedWafelTray : CustomItem
    {
        public override string UniqueNameID => "wafel_tray";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Wafel Tray");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override int SplitCount => 4;
        public override Item SplitSubItem => GetCastedGDO<Item, Wafel>();
        public override List<Item> SplitDepletedItems => new() { GetCastedGDO<Item, EmptyWafelTray>() };

        public override Item DisposesTo => GetCastedGDO<Item, EmptyWafelTray>();

        public override void OnRegister(Item gdo)
        {
            Prefab.ApplyMaterialToChild("Tray", "Metal Black - Shiny");
            Prefab.ApplyMaterialToChildren("Cookie", "Bread - Cooked");

            List<GameObject> objects = new();
            for (int i = 0; i < Prefab.GetChildCount(); i++)
            {
                var child = Prefab.GetChild(i);
                if (!child.name.Contains("Cookie"))
                    continue;

                if (objects.Count < gdo.SplitCount)
                    objects.Add(child);
            }

            ReflectionUtils.GetField<ObjectsSplittableView>("Objects").SetValue(Prefab.AddComponent<ObjectsSplittableView>(), objects);
        }
    }
}
