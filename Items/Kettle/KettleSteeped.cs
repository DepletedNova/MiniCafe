namespace MiniCafe.Items
{
    internal class KettleSteeped : CustomItemGroup<KettleSteeped.View>
    {
        public static int KettleID { get; private set; }
        public override void AttachDependentProperties(GameData gameData, GameDataObject gdo)
        {
            base.AttachDependentProperties(gameData, gdo);
            KettleID = gdo.ID;
        }

        public override string UniqueNameID => "kettle_steeped";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Kettle Tea");
        public override ItemCategory ItemCategory => ItemCategory.Generic;
        public override ItemStorage ItemStorageFlags => ItemStorage.None;
        public override Item DisposesTo => GetCastedGDO<Item, Kettle>();
        public override bool ApplyProcessesToComponents => true;

        public override void OnRegister(ItemGroup gdo)
        {
            var view = Prefab.GetComponent<View>();
            view.Setup(gdo);
            view.LabelGameObject.transform.localPosition += Vector3.up * 0.15f;

            Prefab.ApplyMaterialToChild("pot", "Plastic", "Metal Dark", "Metal", "Hob Black");
            Prefab.ApplyMaterialToChild("lid", "Plastic", "Metal");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");

            // Sage
            var sage = Prefab.GetChild("Sage");
            sage.ApplyMaterialCafe("Sage Tea");
            sage.ApplyMaterialToChildren("sage", "Sage");

            // Earl Grey
            Prefab.ApplyMaterialToChildCafe("Earl Grey", "Earl Grey Tea");

            // Hibiscus
            Prefab.ApplyMaterialToChildCafe("Hibiscus", "Hibiscus Teapot");
        }

        public override int SplitCount => 6;
        public override float SplitSpeed => 1;
        public override bool PreventExplicitSplit => true;
        public override bool AllowSplitMerging => true;
        public override bool SplitByComponents => true;
        public override Item SplitByComponentsHolder => GetCastedGDO<Item, KettleBoiled>();
        public override Item RefuseSplitWith => GetCastedGDO<Item, KettleBoiled>();

        public override List<Item.ItemProcess> Processes => new()
        {
            new()
            {
                Process = GetGDO<Process>(ProcessReferences.Cook),
                Duration = 12f,
                IsBad = true,
                Result = GetCastedGDO<Item, Kettle>()
            }
        };

        public override List<IItemProperty> Properties => new()
        {
            new CComponentSplitDepleted()
            {
                DepletionItem = GetCustomGameDataObject<Kettle>().ID
            }
        };

        public class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, SageSteeped>(),
                    GameObject = gameObject.GetChild("Sage"),
                },
                new()
                {
                    Item = GetCastedGDO<Item, EarlGreySteeped>(),
                    GameObject = gameObject.GetChild("Earl Grey"),
                },
                new()
                {
                    Item = GetCastedGDO<Item, HibiscusSteeped>(),
                    GameObject = gameObject.GetChild("Hibiscus"),
                },
            };

            protected override List<ColourBlindLabel> labels => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, KettleSteeped>(),
                    Text = "St"
                },
                new()
                {
                    Item = GetCastedGDO<Item, SageSteeped>(),
                    Text = "Sa"
                },
                new()
                {
                    Item = GetCastedGDO<Item, EarlGreySteeped>(),
                    Text = "EG"
                },
                new()
                {
                    Item = GetCastedGDO<Item, HibiscusSteeped>(),
                    Text = "Hi"
                },
            };
        }
    }
}
