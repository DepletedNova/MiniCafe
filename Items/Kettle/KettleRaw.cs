namespace MiniCafe.Items
{
    internal class KettleRaw : CustomItemGroup<KettleRaw.View>
    {
        public static int KettleID { get; private set; }
        public override void AttachDependentProperties(GameData gameData, GameDataObject gdo)
        {
            base.AttachDependentProperties(gameData, gdo);
            KettleID = gdo.ID;
        }

        public override string UniqueNameID => "kettle_raw";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Kettle Tea Raw");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override Item DisposesTo => GetCastedGDO<Item, Kettle>();

        public override List<ItemGroupView.ColourBlindLabel> Labels => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, Sage>(),
                    Text = "Ma"
                },
                new()
                {
                    Item = GetCastedGDO<Item, EarlGrey>(),
                    Text = "EG"
                },
                new()
                {
                    Item = GetCastedGDO<Item, GroundHibiscus>(),
                    Text = "Hi"
                },
            };

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(SteepProcess),
                Duration = 4.5f,
                Result = GetCastedGDO<Item, KettleSteeped>(),
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
                    GetCastedGDO<Item, GroundMatcha>(),
                    GetCastedGDO<Item, EarlGrey>(),
                    GetCastedGDO<Item, GroundHibiscus>()
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

            gdo.AutomaticItemProcess = new()
            {
                Process = GetGDO<Process>(SteepProcess),
                Duration = 4.5f,
                Result = GetCastedGDO<Item, KettleSteeped>(),
            };

            Prefab.ApplyMaterialToChild("pot", "Plastic", "Metal Dark", "Metal", "Hob Black");
            Prefab.ApplyMaterialToChild("lid", "Plastic", "Metal");
            Prefab.ApplyMaterialToChild("water", "Soup - Watery");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");

            // Matcha
            Prefab.GetChild("Matcha").ApplyMaterialToChildren("fill", "Sage");

            // Earl Grey
            var eg = Prefab.GetChild("Earl Grey");
            eg.ApplyMaterialToChildCafe("earl", "Earl Grey");
            eg.ApplyMaterialToChildCafe("grey", "Plastic - Blue");

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
                    Item = GetCastedGDO<Item, GroundMatcha>(),
                    GameObject = gameObject.GetChild("Matcha"),
                },
                new()
                {
                    Item = GetCastedGDO<Item, EarlGrey>(),
                    GameObject = gameObject.GetChild("Earl Grey"),
                },
                new()
                {
                    Item = GetCastedGDO<Item, GroundHibiscus>(),
                    GameObject = gameObject.GetChild("Hibiscus"),
                },
            };
        }
    }
}
