namespace MiniCafe.Items
{
    internal class KettleRaw : CustomItemGroup<KettleRaw.View>
    {
        public override string UniqueNameID => "kettle_raw";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Kettle Tea Raw");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override Item DisposesTo => GetCastedGDO<Item, Kettle>();

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Result = GetCastedGDO<Item, Kettle>(),
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 8,
                IsBad = true,
            },
            new()
            {
                Result = GetCastedGDO<Item, KettleSteeped>(),
                Process = GetCastedGDO<Process, SteepProcess>(),
                Duration = 8
            }
        };

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, KettleBoiled>()
                },
                IsMandatory = true,
                Min = 1,
                Max = 1,
            },
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, Sage>(),
                    GetCastedGDO<Item, EarlGrey>(),
                    GetCastedGDO<Item, Hibiscus>()
                },
                Min = 1,
                Max = 1
            }
        };

        public override List<IItemProperty> Properties => new()
        {
            new CPreventItemMerge { Condition = MergeCondition.OnlyAsWrapped }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            var view = Prefab.GetComponent<View>();
            view.Setup(gdo);
            view.LabelGameObject.transform.localPosition += Vector3.up * 0.65f;

            Prefab.ApplyMaterialToChild("pot", "Plastic", "Metal Dark", "Metal", "Hob Black");
            Prefab.ApplyMaterialToChild("lid", "Plastic", "Metal");
            Prefab.ApplyMaterialToChild("water", "Soup - Watery");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");

            // Sage
            Prefab.GetChild("Sage").ApplyMaterialToChildren("sage", "Sage");

            // Earl Grey
            var eg = Prefab.GetChild("Earl Grey");
            eg.ApplyMaterialToChildCafe("earl", "Earl Grey");
            eg.ApplyMaterialToChildCafe("grey", "Earl Grey Extra");

            // Earl Grey
            var hi = Prefab.GetChild("Hibiscus");
            hi.ApplyMaterialToChildCafe("petal1", "Hibiscus");
            hi.ApplyMaterialToChildCafe("petal2", "Hibiscus Extra");
        }

        public class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, Sage>(),
                    GameObject = gameObject.GetChild("Sage"),
                },
                new()
                {
                    Item = GetCastedGDO<Item, EarlGrey>(),
                    GameObject = gameObject.GetChild("Earl Grey"),
                },
                new()
                {
                    Item = GetCastedGDO<Item, Hibiscus>(),
                    GameObject = gameObject.GetChild("Hibiscus"),
                },
            };
            protected override List<ColourBlindLabel> labels => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, Sage>(),
                    Text = "Sa"
                },
                new()
                {
                    Item = GetCastedGDO<Item, EarlGrey>(),
                    Text = "EG"
                },
                new()
                {
                    Item = GetCastedGDO<Item, Hibiscus>(),
                    Text = "Hi"
                },
            };
        }
    }
}
