using UnityEngine.VFX;
using static Kitchen.ItemGroupView;

namespace MiniCafe
{
    internal static class Helper
    {
        private const string ModName = "Mini Cafe";

        // GDO
        public static T GetGDO<T>(int id) where T : GameDataObject
        {
            return GetExistingGDO(id) as T;
        }

        public static List<ColourBlindLabel> ApplyPlatedLabel(List<ColourBlindLabel> addedLabels)
        {
            List<ColourBlindLabel> labels = new();
            foreach (var label in addedLabels)
                labels.Add(label);
            foreach (var label in defaultLabels)
                labels.Add(label);

            return labels;
        }

        private static List<ColourBlindLabel> defaultLabels => new()
        {
            new()
            {
                Item = GetCastedGDO<Item, Croissant>(),
                Text = "Cr"
            },
            new()
            {
                Item = GetCastedGDO<Item, Scone>(),
                Text = "Sc"
            },
        };

        internal interface IWontRegister { }

        public abstract class PlatedItemGroupView : ItemGroupView
        {
            protected virtual List<ComponentGroup> groups => new();

            private List<ComponentGroup> defaultGroups => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, Teaspoon>(),
                    GameObject = gameObject.GetChild("Sides/Spoon")
                },
                new()
                {
                    Item = GetCastedGDO<Item, Croissant>(),
                    GameObject = gameObject.GetChild("Sides/Croissant")
                },
                new()
                {
                    Item = GetCastedGDO<Item, Scone>(),
                    GameObject = gameObject.GetChild("Sides/Scone")
                },
            };

            public virtual void Setup(GameDataObject gdo)
            {
                List<ComponentGroup> compGroups = new();
                foreach (var group in groups)
                    compGroups.Add(group);
                foreach (var group in defaultGroups)
                    compGroups.Add(group);
            }
        }

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
        internal static void ApplyGenericPlated(this GameObject prefab)
        {
            string size = prefab.name.ToLower().Contains("big") ? "Sack - Blue" : "Coffee Blend";
            prefab.ApplyMaterialToChildCafe("Plate", "Plate", size);
            var steam = prefab.GetChild("Steam");
            if (steam != null)
                steam.ApplyVisualEffect("Steam");

            AttachPlateExtras(prefab);
        }

        private static GameObject ExtrasPrefab;
        internal static void AttachPlateExtras(this GameObject prefab)
        {
            if (ExtrasPrefab == null)
            {
                ExtrasPrefab = Main.Bundle.LoadAsset<GameObject>("Sides");
                ExtrasPrefab.ApplyMaterialToChildCafe("Spoon", "Metal");
                ExtrasPrefab.ApplyMaterialToChildCafe("Croissant", "Croissant");
                ExtrasPrefab.ApplyMaterialToChildCafe("Scone", "Bread - Inside Cooked", "Chocolate");
            }
            var extras = Object.Instantiate(ExtrasPrefab);
            var transform = extras.transform;
            transform.SetParent(prefab.transform, false);
            extras.name = "Sides";
        }

        private static Dictionary<string, VisualEffectAsset> VisualEffects = new();
        internal static VisualEffect ApplyVisualEffect(this GameObject gameObject, string effectName)
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

        // Materials
        public static void ApplyMaterial<T>(this GameObject gameObject, Material[] materials) where T : Renderer
        {
            var comp = gameObject.GetComponent<T>();
            if (comp == null)
                return;

            comp.materials = materials;
        }
        public static void ApplyMaterial(this GameObject gameObject, Material[] materials)
        {
            ApplyMaterial<MeshRenderer>(gameObject, materials);
        }
        public static void ApplyMaterialCafe(this GameObject gameObject, params string[] materials)
        {
            ApplyMaterial<MeshRenderer>(gameObject, GetMaterialArray(materials));
        }

        public static void ApplyMaterialToChildren<T>(this GameObject gameObject, string nameMatch, Material[] materials) where T : Renderer
        {
            for (int i = 0; i < gameObject.GetChildCount(); i++)
            {
                GameObject child = gameObject.GetChild(i);
                if (!child.name.ToLower().Contains(nameMatch.ToLower()))
                    continue;
                child.ApplyMaterial<T>(materials);
            }
        }
        public static void ApplyMaterialToChildren(this GameObject gameObject, string nameMatch, Material[] materials)
        {
            ApplyMaterialToChildren<MeshRenderer>(gameObject, nameMatch, materials);
        }
        public static void ApplyMaterialToChildren(this GameObject gameObject, string nameMatch, params string[] materials)
        {
            ApplyMaterialToChildren<MeshRenderer>(gameObject, nameMatch, GetMaterialArray(materials));
        }

        public static void ApplyMaterialToChild<T>(this GameObject gameObject, string childName, Material[] materials) where T : Renderer
        {
            gameObject.GetChild(childName).ApplyMaterial<T>(materials);
        }
        public static void ApplyMaterialToChildCafe(this GameObject gameObject, string childName, Material[] materials)
        {
            gameObject.GetChild(childName).ApplyMaterial(materials);
        }
        public static void ApplyMaterialToChildCafe(this GameObject gameObject, string childName, params string[] materials)
        {
            gameObject.GetChild(childName).ApplyMaterial(GetMaterialArray(materials));
        }

        public static Material[] GetMaterialArray(params string[] materials)
        {
            List<Material> materialList = new List<Material>();
            foreach (string matName in materials)
            {
                string formatted = $"{ModName} - \"{matName}\"";
                string formatted2 = $"IngredientLib - \"{matName}\"";
                if (CustomMaterials.CustomMaterialsIndex.ContainsKey(formatted))
                {
                    materialList.Add(CustomMaterials.CustomMaterialsIndex[formatted]);
                }
                else if (CustomMaterials.CustomMaterialsIndex.ContainsKey(formatted2))
                {
                    materialList.Add(CustomMaterials.CustomMaterialsIndex[formatted2]);
                }
                else
                {
                    materialList.Add(MaterialUtils.GetExistingMaterial(matName));
                }
            }
            return materialList.ToArray();
        }

        // Material Generation
        public static Material CreateFlat(string name, Color color, float shininess = 0, float overlayScale = 10)
        {
            Material mat = new Material(Shader.Find("Simple Flat"));
            mat.name = $"{ModName} - \"{name}\"";
            mat.SetColor("_Color0", color);
            mat.SetFloat("_Shininess", shininess);
            mat.SetFloat("_OverlayScale", overlayScale);
            return mat;
        }
        public static Material CreateFlat(string name, int color, float shininess = 0, float overlayScale = 10)
        {
            return CreateFlat(name, ColorFromHex(color), shininess, overlayScale);
        }

        public static Material CreateTransparent(string name, Color color)
        {
            Material mat = new Material(Shader.Find("Simple Transparent"));
            mat.name = $"{ModName} - \"{name}\"";
            mat.SetColor("_Color", color);
            return mat;
        }
        public static Material CreateTransparent(string name, int color, float opacity)
        {
            Color col = ColorFromHex(color);
            col.a = opacity;
            return CreateTransparent(name, col);
        }

        // Color Utility
        public static Color ColorFromHex(int hex)
        {
            return new Color(((hex & 0xFF0000) >> 16) / 255.0f, ((hex & 0xFF00) >> 8) / 255.0f, (hex & 0xFF) / 255.0f);
        }
    }
}
