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
    internal class StroopwafelTray : CustomItemGroup
    {
        public override string UniqueNameID => "stroopwafel_tray";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Stroopwafel Tray");
        public override ItemStorage ItemStorageFlags => ItemStorage.Small;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override int SplitCount => 5;
        public override Item SplitSubItem => GetCastedGDO<Item, Stroopwafel>();
        public override List<Item> SplitDepletedItems => new() { GetCastedGDO<Item, Stroopwafel>() };

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, Stroop>(),
                Text = "SW"
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, WafelTray>(),
                    GetCastedGDO<Item, Stroop>()
                },
                Max = 2,
                Min = 2,
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.ApplyMaterialToChild("Tray", "Metal");

            List<GameObject> objects = new();
            for (int i = 0; i < Prefab.GetChildCount(); i++)
            {
                var child = Prefab.GetChild(i);
                if (!child.name.Contains("Cookie"))
                    continue;

                child.ApplyMaterial("Bread - Cooked", "Stroop");

                if (objects.Count < gdo.SplitCount)
                    objects.Add(child);
            }

            ReflectionUtils.GetField<ObjectsSplittableView>("Objects").SetValue(Prefab.AddComponent<ObjectsSplittableView>(), objects);
        }
    }
}
