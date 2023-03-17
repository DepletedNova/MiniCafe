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

        public static ItemGroup.ItemSet ExtrasSet => new()
        {
            Items = new()
            {
                GetCastedGDO<Item, Teaspoon>(),
                GetCastedGDO<Item, Croissant>(),
                GetCastedGDO<Item, Scone>()
            },
            IsMandatory = true,
            RequiresUnlock = true,
            Max = 1,
            Min = 1,
        };

        internal interface IWontRegister { }

        public abstract class PlatedItemGroupView : ItemGroupView
        {
            protected virtual List<ComponentGroup> groups => new();
            protected virtual List<ColourBlindLabel> labels => new();

            public GameObject LabelGameObject;

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

            private List<ColourBlindLabel> defaultLabels => new()
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

            public virtual void Setup(GameDataObject gdo)
            {
                List<ComponentGroup> compGroups = new();
                foreach (var group in groups)
                    compGroups.Add(group);
                foreach (var group in defaultGroups)
                    compGroups.Add(group);

                List<ColourBlindLabel> compLabels = new();
                foreach (var label in labels)
                    compLabels.Add(label);
                foreach (var label in defaultLabels)
                    compLabels.Add(label);

                ComponentGroups = compGroups;
                ComponentLabels = compLabels;
                LabelGameObject = ColorblindUtils.cloneColourBlindObjectAndAddToItem(gdo as Item);
                ColorblindUtils.setColourBlindLabelObjectOnItemGroupView(this, LabelGameObject);
                LabelGameObject.transform.Find("Title").localPosition += Vector3.up * 0.15f;
            }
        }

        // Views
        public abstract class AccessedItemGroupView : ItemGroupView
        {
            protected abstract List<ComponentGroup> groups { get; }
            protected virtual List<ColourBlindLabel> labels => new();

            public GameObject LabelGameObject;

            public virtual void Setup(GameDataObject gdo)
            {
                ComponentGroups = groups;

                if (labels.Count > 0)
                {
                    ComponentLabels = labels;
                    LabelGameObject = ColorblindUtils.cloneColourBlindObjectAndAddToItem(gdo as Item);
                    ColorblindUtils.setColourBlindLabelObjectOnItemGroupView(this, LabelGameObject);
                }
            }
        }

        // Generic
        internal static void ApplyGenericPlated(this GameObject prefab)
        {
            prefab.ApplyMaterialToChildCafe("Plate", "Plate", "Plate - Ring");
            var steam = prefab.GetChild("Steam");
            if (steam != null)
                steam.ApplyVisualEffect("Steam");

            var extras = prefab.GetChild("Sides");
            extras.ApplyMaterialToChildCafe("Spoon", "Metal");
            extras.ApplyMaterialToChildCafe("Croissant", "Croissant");
            extras.ApplyMaterialToChildCafe("Scone", "Bread - Inside Cooked", "Chocolate");
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
