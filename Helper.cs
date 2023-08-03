using Kitchen;
using KitchenData;
using KitchenLib.Utils;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using static KitchenLib.Utils.GDOUtils;

namespace MiniCafe
{
    public static class Helper
    {
        // GDO
        public static T GetGDO<T>(int id) where T : GameDataObject => GetExistingGDO(id) as T;

        public interface IWontRegister { }

        // Views
        public abstract class AccessedItemGroupView : ItemGroupView
        {
            protected abstract List<ComponentGroup> groups { get; }
            protected virtual List<ColourBlindLabel> labels => new();

            public virtual void Setup(Item gdo)
            {
                ComponentGroups = groups;
            }
        }

        // Generic
        public static void ApplyGenericPlated(this GameObject prefab)
        {
            string size = prefab.name.ToLower().Contains("big") ? "Sack - Blue" : "Coffee Blend";
            prefab.ApplyMaterialToChild("Plate", "Plate", size);
            var steam = prefab.GetChild("Steam");
            if (steam != null)
                steam.ApplyVisualEffect("Steam");

            prefab.ApplyMaterialToChild("Spoon", "Metal");
        }

        public static void AddObjectsSplittableView(this Item gdo, GameObject childSelect, string name, params string[] materials)
        {
            List<GameObject> objects = new();
            for (int i = 0; i < childSelect.GetChildCount(); i++)
            {
                var child = childSelect.GetChild(i);
                if (!child.name.ToLower().Contains(name))
                    continue;

                child.ApplyMaterial(materials);

                if (objects.Count < gdo.SplitCount)
                    objects.Add(child);
            }
            objects.Reverse();
            ReflectionUtils.GetField<ObjectsSplittableView>("Objects").SetValue(gdo.Prefab.AddComponent<ObjectsSplittableView>(), objects);
        }

        public static void AddPositionSplittableView(this GameObject prefab, List<GameObject> objects, Vector3 emptyPos, Vector3 fullPos)
        {
            var view = prefab.TryAddComponent<PositionSplittableView>();
            ReflectionUtils.GetField<PositionSplittableView>("Objects").SetValue(view, objects);
            ReflectionUtils.GetField<PositionSplittableView>("EmptyPosition").SetValue(view, emptyPos);
            ReflectionUtils.GetField<PositionSplittableView>("FullPosition").SetValue(view, fullPos);
        }

        private static Dictionary<string, VisualEffectAsset> VisualEffects = new();
        public static VisualEffect ApplyVisualEffect(this GameObject gameObject, string effectName)
        {
            if (VisualEffects.IsNullOrEmpty())
            {
                foreach (VisualEffectAsset asset in Resources.FindObjectsOfTypeAll<VisualEffectAsset>())
                {
                    if (!VisualEffects.ContainsKey(asset.name))
                    {
                        VisualEffects.Add(asset.name, asset);
                    }
                }
            }
            var comp = gameObject.TryAddComponent<VisualEffect>();
            comp.visualEffectAsset = VisualEffects.TryGetValue(effectName, out var effect) ? effect : null;
            return comp;
        }
    }
}
