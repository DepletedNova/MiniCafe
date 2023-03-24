namespace MiniCafe.Mains.Tea
{
    internal class BigEarlGrey : CustomItemGroup<BigEarlGrey.View>
    {
        public override string UniqueNameID => "big_earl_grey";
        public override GameObject Prefab => Main.Bundle.LoadAsset<GameObject>("Big Earl Grey");
        public override Item DisposesTo => GetCastedGDO<Item, BigMug>();
        public override ItemStorage ItemStorageFlags => ItemStorage.StackableFood;
        public override ItemCategory ItemCategory => ItemCategory.Generic;

        public override List<ItemGroup.ItemSet> Sets => new()
        {
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, BigMug>(),
                    GetCastedGDO<Item, EarlGreySteeped>()
                },
                IsMandatory = true,
                Min = 2,
                Max = 2,
            },
            new()
            {
                Items = new()
                {
                    GetCastedGDO<Item, HoneyIngredient>(),
                    GetCastedGDO<Item, LemonSlice>()
                },
                Min = 0,
                Max = 2
            }
        };

        public override void OnRegister(ItemGroup gdo)
        {
            Prefab.GetComponent<View>().Setup(gdo);

            BigMug.ApplyMugMaterials(Prefab.GetChild("mug"));
            Prefab.ApplyMaterialToChildCafe("fill", "Earl Grey Tea");
            Prefab.GetChild("Steam").ApplyVisualEffect("Steam");

            Prefab.ApplyMaterialToChildCafe("Lemon", "Lemon", "Lemon Inner", "White Fruit");
            Prefab.ApplyMaterialToChildCafe("Honey", "Honey");
        }

        internal class View : AccessedItemGroupView
        {
            protected override List<ComponentGroup> groups => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, LemonSlice>(),
                    GameObject = gameObject.GetChild("Lemon")
                },
                new()
                {
                    Item = GetCastedGDO<Item, HoneyIngredient>(),
                    GameObject = gameObject.GetChild("Honey"),
                }
            };

            protected override List<ColourBlindLabel> labels => new()
            {
                new()
                {
                    Item = GetCastedGDO<Item, EarlGreySteeped>(),
                    Text = "BEG"
                },
                new()
                {
                    Item = GetCastedGDO<Item, LemonSlice>(),
                    Text = "L"
                },
                new()
                {
                    Item = GetCastedGDO<Item, HoneyIngredient>(),
                    Text = "H"
                }
            };
        }
    }
}
